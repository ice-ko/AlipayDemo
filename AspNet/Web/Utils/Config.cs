using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayWeb.Utils
{
    public class Config
    {
        /// <summary>
        /// 应用ID,您的APPID
        /// </summary>
        public static string app_id = "2016091100487061";
        /// <summary>
        /// 商户UID
        /// </summary>
        public static string UID = "2088102175160555";
        /// <summary>
        /// 支付宝网关
        /// </summary>
        public static string gatewayUrl = "https://openapi.alipaydev.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

        /// <summary>
        /// 商户私钥，您的原始格式RSA私钥
        /// </summary>
        public static string private_key = "MIIEowIBAAKCAQEAoi9XWot1kbOHYDQqpVcGeCnbAgpeXTnl4WrSlMfeChRgO8mZzZn8c9/Ny+fXl7WiSJVW/m62q9L/HKDCF8kWlnfivJFJ0cLR4DMp06CL+zCbOnQNsIhKE1qZpipi4jbcWB4cXEkmRW4beY15z04xEs1q0sP0mRchvQhFnZsRGVngh8s7+cRA0ZE9rF+5aX96ZNWdtC7xrIbUW0R5bBy1IsW/UkyNoeK9TKnOU5KbuAGf8KBCJDhX7WgNkGo6ahHeA/iq35xo043Cx44FKvVIic2q8X2QIFU7d2d0tvJ8L80himBeyYc2BvwkuXDqPIUQRlvyGlU3sYUk586R0UfVkwIDAQABAoIBAD7Ls7fkLXZnm6/9ItH32xJlql2k5BqWk1JvSO/AX1FpyET97uYzjBLVgdpEfy5HbnlKEW/cMDBhNtO/zsbk3jEOZCT84CculpKHXNeK3wg4LrI4QyCp9Hx/5OP4n2bVNQWNSw/UZtQVKOAsjhBdRxTvoj/PDYLE+RQ3ArVeWAWHMGUZtYnOoxL0cNJqRPUyTeXWvy6gAV3e6GT3aVU1m7fiUrYYUz9BCaPJWY5uNW8VgaSOT+pxeaOZxNEfi1YLrERy7H2RgfOR2lQQPVTUItL7Y1diI3BOUNQkNyegbBmSXb901JRk+XW9mwain4+y0GwVtl+9RmArwQgd4Lem1KkCgYEAy8gXa4Ly58/yb+wjj7NE7GFhqSebp/2zWD6apNKccjsQ7/dMdoNPXoo5qXjOoRzb4oufFOSNZEz+SwZIsppVF/3NDPAAPe5iS3Oq/O+TUaF3X3f5vlp2bfbsSj4LIfOJQIwpY+YrGH5KcPOKLgcT8pL4iDel6GUFkJu4H15jVsUCgYEAy76KiP2kF1K3y6ySPuxxThSEN854/zB+NnNytooJIpN3KYYWtKXutsuHLtrJwEKj3wJl/PJVjNzXE5fRUioAKaYDagjuv4fhv843YSmz3iirnxyvKYJ11r+iwDhSAKh94BtlPHjonyqJMtZhMIH4gTdlsGQBQkUU9c42VspYgHcCgYEAlRsOT+Nb+tREDzLYfl/ssY07mTrbARt4K7x7CNj2Efs4FG19rEW+QG2hbWhwwKBOH8CLoQzujBJuCtVJQi38z3YsoCfH9YjEsXKsDTD7OUKNBvTRbz/f4exHzrcBDFGW/vzPmEIlDGHz9Buu4qjZJqf1ZC5bneS1Iq9eFM3KoAUCgYAVVgPN6rnXU2s54f2Cp2IKKi3aZj3Xg22EKDzYDsZ8H3M4Hvmbu1CvRcsHRhQglRnyDZ8+iOYalTnUrG5YK1vPALqUzRCrcABdBKxZ+XMDjcDDLc7OCwJqFbFmbb9woDRmVpgYaBT8LgkmELsmyEWMyo9UlHRpSzCxuQGJvzDbawKBgH09i9AKdIQsKtEs21d1RRpPEvTrjlCfuSQzM4GiB7avJlYl7NRKOwwML6PwFPRhI2cv6vYhqjtP1KeqpjOLfz1eH4hNHZd28YKj3sVCYGnsSWbkgXCsV12lu3Mtmx/tNdgM/u+R2aNmRGtLy5Srs5DdynMjqAYWAQotfI90eRA7";
      
        /// <summary>
        /// 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        /// </summary>
        public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmpiK2CMI34GXqfR8MHktCkOZA880FylREWIrjxDKNCDWlWERlYQzD9U900zCjqG4NVcFLNUdImUS4EqnmOnighQV4Vnq8Jvsr6ZEkuwfYwtaBZU0PXE3oPvXpQI8prjRCa/XXj15mUCkYeIucxOWiYOEsg5LaU00CNo3mb6Ki6Cty2LUPkLzED6jWBQgPhkZ/apgvzyfMXfgcTOgQ91YxeAbK5tBGM+GkasB9j41jF3U+HSvSPqMrmJ3z92D90k5MBU3a1htCcQtNh7PqnUjYEGiUKu3UzLHgQe4voiH+7+hQl8vOVJjYNv7zEVrq96ns/UQNZRjZAKbJed6+hg09wIDAQAB";

        /// <summary>
        /// 签名方式
        /// </summary>
        public static string sign_type = "RSA2";

        /// <summary>
        /// 编码格式
        /// </summary>
        public static string charset = "UTF-8";
        /// <summary>
        /// 版本号，无需修改
        /// </summary>
        public static string version = "1.0";
    }
}