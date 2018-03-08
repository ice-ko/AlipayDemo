using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Alipay.Tools
{
    public class AppSettingHelper
    {
        /// <summary>
        /// 支付宝商户UID
        /// </summary>
        static public string AlipayUid => ConfigurationManager.AppSettings["Alipay:Uid"];
        /// <summary>
        /// ip地址
        /// </summary>
        static public string IpAddress => ConfigurationManager.AppSettings["ip"];
    }
}
