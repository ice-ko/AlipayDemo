using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayOpenAppQrcodeCreateModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenAppQrcodeCreateModel : AopObject
    {
        /// <summary>
        /// 对应的二维码描述
        /// </summary>
        [XmlElement("describe")]
        public string Describe { get; set; }

        /// <summary>
        /// 示例："/index.html?name=ali&loc=hz"，/index.html为小程序中能访问到的路径，?name=ali&loc=hz 为自定义的query参数
        /// </summary>
        [XmlElement("url_param")]
        public string UrlParam { get; set; }
    }
}
