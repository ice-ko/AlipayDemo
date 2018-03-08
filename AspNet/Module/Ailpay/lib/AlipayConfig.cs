using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {

        private static string partner = ConfigurationManager.AppSettings["alipay_partner"];
        private static string seller_email = ConfigurationManager.AppSettings["alipay_seller_email"];
        //private static string key = ConfigurationManager.AppSettings["alipay_key"];
        private static string key = "";
        private static string input_charset = "utf-8";
        //private static string sign_type = "MD5";
        private static string sign_type = "RSA";

        private static string appId = ConfigurationManager.AppSettings["alipay_appid"];

        //商户的私钥
        private static string private_key = ConfigurationManager.AppSettings["alipay_privatekey"];

        //支付宝的公钥，无需修改该值
        private static string public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";


        #region 属性
        /// <summary>
        /// 支付宝分配给开发者的应用ID
        /// </summary>
        public static string AppId
        {
            get { return appId; }
            set { appId = value; }
        }
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Seller_email
        {
            get { return seller_email; }
            set { seller_email = value; }
        }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }
        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }
        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}