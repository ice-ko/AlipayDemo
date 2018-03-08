using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// Image Data Structure.
    /// </summary>
    [Serializable]
    public class Image : AopObject
    {
        /// <summary>
        /// 图片url
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
