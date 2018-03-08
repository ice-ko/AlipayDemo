using Com.Alipay.Domain;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Com.Alipay.Model;
using Components.Tools;
using Com.Alipay.Business;
using Com.Alipay;
using PayWeb.Utils;
using System.Threading;
using System.Drawing.Imaging;
using System.Collections.Specialized;

namespace PayWeb.Controllers
{
    /// <summary>
    /// 二维码支付
    /// </summary>
    public class ScanPayController : Controller
    {
        IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(Config.gatewayUrl, Config.app_id, Config.private_key, Config.version,
                           Config.sign_type, Config.alipay_public_key, Config.charset);
        // GET: ScanPay
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 生成支付二维码
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ScanCodeGen(string orderName, string orderAmount, string outTradeNo)
        {

            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(orderName, orderAmount, outTradeNo);

            //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
            //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。

            //string notify_url = "http://10.5.21.14/Pay/Notify";  //商户接收异步通知的地址
            AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder);

            //以下返回结果的处理供参考。
            //payResponse.QrCode即二维码对于的链接
            //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。
            var bitmap = new Bitmap(Path.Combine(Server.MapPath("~/"), "Images/alipay.png"));
            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:
                    bitmap.Dispose();
                    bitmap = RenderQrCode(precreateResult.response.QrCode);
                    //轮询订单结果
                    //根据业务需要，选择是否新起线程进行轮询
                    ParameterizedThreadStart parStart = new ParameterizedThreadStart(LoopQuery);
                    Thread myThread = new Thread(parStart);
                    object o = precreateResult.response.OutTradeNo;
                    myThread.Start(o);
                    break;
                case ResultEnum.FAILED:
                    Console.WriteLine("生成二维码失败：" + precreateResult.response.Body);
                    break;

                case ResultEnum.UNKNOWN:
                    Console.WriteLine("生成二维码失败：" + (precreateResult.response == null ? "配置或网络异常，请检查后重试" : "系统异常，请更新外部订单后重新发起请求"));
                    break;
            }
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            byte[] bytes = ms.GetBuffer();
            return File(bytes, "image/png");
        }
        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单编号</param>
        /// <returns>请求结果集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(string orderName, string orderAmount, string outTradeNo)
        {
            //线上联调时，请输入真实的外部订单号。
            if (string.IsNullOrEmpty(outTradeNo))
            {
                outTradeNo = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            }

            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号
            builder.seller_id = Config.UID;
            //订单编号
            builder.out_trade_no = outTradeNo;
            //订单总金额
            builder.total_amount = orderAmount;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = orderName;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";

            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "goods id";
            goods.goods_name = "goods name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;

        }
        /// <summary>
        /// 渲染二维码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Bitmap RenderQrCode(string str)
        {
            QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        Bitmap bp = qrCode.GetGraphic(20, Color.Black, Color.White,
                            new Bitmap(Path.Combine(Server.MapPath("~/"), "Images/alipay.png")), 15);
                        return bp;
                    }
                }
            }

        }
        /// <summary>
        /// 轮询支付结果
        /// </summary>
        /// <param name="o">订单号</param>
        public void LoopQuery(object o)
        {
            AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();
            int count = 100;
            int interval = 10000;
            string outTradeNo = o.ToString();

            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(interval);
                queryResult = serviceClient.tradeQuery(outTradeNo);
                if (queryResult?.Status == ResultEnum.SUCCESS)
                {
                    DoSuccessProcess(queryResult);
                    return;
                }
            }
            DoFailedProcess(queryResult);
        }

        /// <summary>
        /// 请添加支付成功后的处理
        /// </summary>
        private void DoSuccessProcess(AlipayF2FQueryResult queryResult)
        {
            //支付成功，请更新相应单据
            Console.WriteLine("扫码支付成功：商户订单号 " + queryResult.response.OutTradeNo);

        }

        /// <summary>
        /// 请添加支付失败后的处理
        /// </summary>
        private void DoFailedProcess(AlipayF2FQueryResult queryResult)
        {
            //支付失败，请更新相应单据
            Console.WriteLine("扫码支付失败：商户订单号 " + queryResult.response.OutTradeNo);
        }
        public void Notify()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //Notify aliNotify = new Notify();
                Notify aliNotify = new Notify(Config.charset, Config.sign_type, Config.UID, Config.mapiUrl, Config.alipay_public_key);

                //对异步通知进行验签
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                //对验签结果
                //bool isSign = Aop.Api.Util.AlipaySignature.RSACheckV2(sPara, Config.alipay_public_key ,Config.charset,Config.sign_type,false );

                if (verifyResult && CheckParams()) //验签成功 && 关键业务参数校验成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];


                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    //在支付宝的业务通知中，只有交易通知状态为TRADE_SUCCESS或TRADE_FINISHED时，才是买家付款成功。
                    string trade_status = Request.Form["trade_status"];


                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    Response.Write("success");  //请不要修改或删除

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }
        /// <summary>
        /// 对支付宝异步通知的关键参数进行校验
        /// </summary>
        /// <returns></returns>
        private bool CheckParams()
        {
            bool ret = true;

            //获得商户订单号out_trade_no
            string out_trade_no = Request.Form["out_trade_no"];
            //TODO 商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，

            //获得支付总金额total_amount
            string total_amount = Request.Form["total_amount"];
            //TODO 判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），

            //获得卖家账号seller_email
            string seller_email = Request.Form["seller_email"];
            //TODO 校验通知中的seller_email（或者seller_id) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id / seller_email）

            //获得调用方的appid；
            //如果是非授权模式，appid是商户的appid；如果是授权模式（token调用），appid是系统商的appid
            string app_id = Request.Form["app_id"];
            //TODO 验证app_id是否是调用方的appid；。

            //验证上述四个参数，完全吻合则返回参数校验成功
            return ret;

        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }

}