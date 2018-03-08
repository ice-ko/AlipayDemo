using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using PayWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayWeb.Controllers
{
    /// <summary>
    /// 条码支付
    /// </summary>
    public class BarCodeController : Controller
    {
        IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(Config.gatewayUrl, Config.app_id, Config.private_key, Config.version,
                             Config.sign_type, Config.alipay_public_key, Config.charset);
        string result = string.Empty;
        // GET: BarCode
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///  提交支付请求
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <param name="authCode">支付授权码,付款码</param>
        /// <returns></returns>
        public ActionResult ScanCodeGen(string orderName, string orderAmount, string outTradeNo, string authCode)
        {
            AlipayTradePayContentBuilder builder = BuildPayContent(orderName,orderAmount,outTradeNo, authCode);
            string out_trade_no = builder.out_trade_no;

            AlipayF2FPayResult payResult = serviceClient.tradePay(builder);

            switch (payResult.Status)
            {
                case ResultEnum.SUCCESS:
                    DoSuccessProcess(payResult);
                    break;
                case ResultEnum.FAILED:
                    DoFailedProcess(payResult);
                    break;
                case ResultEnum.UNKNOWN:
                    result = "网络异常，请检查网络配置后，更换外部订单号重试";
                    break;
            }
            return Json(result);
        }
        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单编号</param>
        /// <returns>请求结果集</returns>
        private AlipayTradePayContentBuilder BuildPayContent(string orderName, string orderAmount, string outTradeNo, string authCode)
        {
            //线上联调时，请输入真实的外部订单号。
            string out_trade_no = "";
            if (String.IsNullOrEmpty(outTradeNo.Trim()))
            {
                out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            }
            else
            {
                out_trade_no = outTradeNo;
            }

            //扫码枪扫描到的用户手机钱包中的付款条码
            AlipayTradePayContentBuilder builder = new AlipayTradePayContentBuilder();

            //收款账号
            builder.seller_id = Config.UID;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //支付场景，无需修改
            builder.scene = "bar_code";
            //支付授权码,付款码
            builder.auth_code = authCode;
            //订单总金额
            builder.total_amount = orderAmount;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = orderName;
            //自定义超时时间
            builder.timeout_express = "2m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";


            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();

            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "304";
            goods.goods_name = "goods#name";
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
        /// 请添加支付成功后的处理
        /// </summary>
        private void DoSuccessProcess(AlipayF2FPayResult payResult)
        {

            //请添加支付成功后的处理
            System.Console.WriteLine("支付成功");
            result = payResult.response.Body;
        }

        /// <summary>
        /// 请添加支付失败后的处理
        /// </summary>
        private void DoFailedProcess(AlipayF2FPayResult payResult)
        {
            //请添加支付失败后的处理
            System.Console.WriteLine("支付失败");
            result = payResult.response.Body;
        }

    }
}