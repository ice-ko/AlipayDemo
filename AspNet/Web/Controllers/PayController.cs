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
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index()
        {
            return View();
        }
        /// 发起支付请求
        /// </summary>
        /// <param name="tradeno">外部订单号，商户网站订单系统中唯一的订单号</param>
        /// <param name="subject">订单名称</param>
        /// <param name="totalAmout">付款金额</param>
        /// <param name="itemBody">商品描述</param>
        public void PayRequest(string tradeno, string subject, string totalAmout, string itemBody)
        {
            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);

            // 组装业务参数model
            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            // 商品描述
            model.Body = itemBody;
            // 订单名称
            model.Subject = subject;
            // 付款金额
            model.TotalAmount = totalAmout;
            // 外部订单号，商户网站订单系统中唯一的订单号
            model.OutTradeNo = tradeno;
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            // 设置同步回调地址
            request.SetReturnUrl("http://19171zn332.imwork.net:22070/Pay/Callback");
            // 设置异步通知接收地址
            request.SetNotifyUrl("");
            // 将业务model载入到request
            request.SetBizModel(model);
            AlipayTradePagePayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");
                Response.Write(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #region 支付异步回调通知

        /// <summary>
        /// 支付异步回调通知 需配置域名 因为是支付宝主动post请求这个action 所以要通过域名访问或者公网ip
        /// </summary>
        public void Notify()
        {
            /* 实际验证过程建议商户添加以下校验。
            1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
            2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
            3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
            4、验证app_id是否为该商户本身。
            */
            IDictionary<string, string> sArray = GetRequestPost();
            if (sArray.Count != 0)
            {
                bool flag = AlipaySignature.RSACheckV1(sArray, Config.alipay_public_key, Config.charset, Config.sign_type, false);
                if (flag)
                {
                    Console.WriteLine($"异步验证通过，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "同步验证通过";
                }
                else
                {
                    Console.WriteLine($"异步验证失败，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "异步验证失败";
                }
            }
        }

        #endregion

        #region 支付同步回调
        /// <summary>
        /// 支付同步回调
        /// </summary>
        [HttpGet]
        public ActionResult Callback()
        {
            /* 实际验证过程建议商户添加以下校验。
			1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
			2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
			3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
			4、验证app_id是否为该商户本身。
			*/
            IDictionary<string, string> sArray = GetRequestGet();
            if (sArray.Count != 0)
            {
                bool flag = AlipaySignature.RSACheckV1(sArray, Config.alipay_public_key, Config.charset, Config.sign_type, false);
                if (flag)
                {
                    Console.WriteLine($"同步验证通过，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "同步验证通过";
                    var out_trade_no = string.Empty;
                    sArray.TryGetValue("out_trade_no",out out_trade_no);
                    ViewData["PayId"] = out_trade_no;
                }
                else
                {
                    Console.WriteLine($"同步验证失败，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "同步验证失败";
                }
            }
            return View();
        }
        #endregion

        #region 解析请求参数
        private IDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            IDictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
        private IDictionary<string, string> GetRequestPost()
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

        #endregion

        #region 订单查询
        public ActionResult Query() {
            return View();
        }
        public JsonResult QueryInfo(string tradeno, string alipayTradeNo) {
            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);

            AlipayTradeQueryModel model = new AlipayTradeQueryModel();
            // 商户订单号，和交易号不能同时为空
            model.OutTradeNo = tradeno;
            // 支付宝交易号，和商户订单号不能同时为空
            model.TradeNo = alipayTradeNo;

            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.SetBizModel(model);

            AlipayTradeQueryResponse response = null;
            try
            {
                response = client.Execute(request);
                return Json(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region 订单关闭
        public ActionResult OrderClose()
        {
            return View();
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="tradeno">商户订单号</param>
        /// <param name="alipayTradeNo">支付宝交易号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult OrderCloseInfo(string tradeno, string alipayTradeNo)
        {
            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);

            AlipayTradeCloseModel model = new AlipayTradeCloseModel();
            // 商户订单号，和交易号不能同时为空
            model.OutTradeNo = tradeno;
            // 支付宝交易号，和商户订单号不能同时为空
            model.TradeNo = alipayTradeNo;

            AlipayTradeCloseRequest request = new AlipayTradeCloseRequest();
            request.SetBizModel(model);

            AlipayTradeCloseResponse response = null;
            try
            {
                response = client.Execute(request);
              return Json(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region 退款
        public ActionResult Refund() {
            return View();
        }
        /// <summary>
        /// 订单退款
        /// </summary>
        /// <param name="tradeno">商户订单号</param>
        /// <param name="alipayTradeNo">支付宝交易号</param>
        /// <param name="refundAmount">退款金额</param>
        /// <param name="refundReason">退款原因</param>
        /// <param name="refundNo">退款单号</param>
        public ActionResult RefundInfo(string tradeno, string alipayTradeNo, string refundAmount, string refundReason, string refundNo)
        {
            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);

            AlipayTradeRefundModel model = new AlipayTradeRefundModel();
            // 商户订单号，和交易号不能同时为空
            model.OutTradeNo = tradeno;
            // 支付宝交易号，和商户订单号不能同时为空
            model.TradeNo = alipayTradeNo;
            // 退款金额，不能大于订单总金额
            model.RefundAmount = refundAmount;
            // 退款原因
            model.RefundReason = refundReason;
            // 退款单号，同一笔多次退款需要保证唯一，部分退款该参数必填。
            model.OutRequestNo = refundNo;

            AlipayTradeRefundRequest request = new AlipayTradeRefundRequest();
            request.SetBizModel(model);

            AlipayTradeRefundResponse response = null;
            try
            {
                response = client.Execute(request);
               return Json(response.Body);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
        #region 退款查询
        public ActionResult RefundQuery() {
            return View();
        }
        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="tradeno">商户订单号</param>
        /// <param name="alipayTradeNo">支付宝交易号</param>
        /// <param name="refundNo">退款单号</param>
        /// <returns></returns>
        public ActionResult RefundQueryInfo(string tradeno, string alipayTradeNo, string refundNo)
        {
            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);
            if (string.IsNullOrEmpty(refundNo))
            {
                refundNo = tradeno;
            }
            AlipayTradeFastpayRefundQueryModel model = new AlipayTradeFastpayRefundQueryModel();
            model.OutTradeNo = tradeno;
            model.TradeNo = alipayTradeNo;
            model.OutRequestNo = refundNo;

            AlipayTradeFastpayRefundQueryRequest request = new AlipayTradeFastpayRefundQueryRequest();
            request.SetBizModel(model);

            AlipayTradeFastpayRefundQueryResponse response = null;
            try
            {
                response = client.Execute(request);
                return Json(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}