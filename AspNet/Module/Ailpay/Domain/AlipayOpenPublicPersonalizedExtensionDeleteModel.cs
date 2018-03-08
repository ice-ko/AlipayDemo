using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayOpenPublicPersonalizedExtensionDeleteModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenPublicPersonalizedExtensionDeleteModel : AopObject
    {
        /// <summary>
        /// 扩展区套id，接口创建个性化扩展区时会返回
        /// </summary>
        [XmlElement("extension_key")]
        public string ExtensionKey { get; set; }
    }
}
