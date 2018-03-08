using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using PayWeb.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayWeb.Controllers
{
    public class AppPayController : Controller
    {
        // GET: AppPay
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// app支付
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        public ActionResult ScanCodeGen(string orderName, string orderAmount, string outTradeNo) {
            IAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", "RSA2", Config.alipay_public_key, "utf-8", false);
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = "我是测试数据";
            model.Subject = orderName;
            model.TotalAmount = orderAmount;
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = "20170216test01";
            model.TimeoutExpress = "30m";
            request.SetBizModel(model);
            request.SetNotifyUrl("http://19171zn332.imwork.net:22070/AppPay/Notify");
            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
           // Response.Write(HttpUtility.HtmlEncode(response.Body));
            //页面输出的response.Body就是orderString 可以直接给客户端请求，无需再做处理。
            return Json(response.Body);
        }
        public void Notify()
        {
           var sPara = GetRequestPost();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                bool flag = AlipaySignature.RSACheckV1(sPara, Config.alipay_public_key, "utf-8", "RSA2", false);

                if (flag) //验签成功 && 关键业务参数校验成功
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
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组 
        /// request回来的信息组成的数组
        public IDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            IDictionary<string, string> sArray = new Dictionary<string, string>();
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