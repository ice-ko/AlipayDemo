using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Tools
{
    public class AppSettingHelper
    {
        /// <summary>
        /// 支付宝商户UID
        /// </summary>
        static public string AlipayUid => ConfigurationManager.AppSettings["Alipay:Uid"];
    }
}
