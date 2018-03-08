using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// QueryLabelRule Data Structure.
    /// </summary>
    [Serializable]
    public class QueryLabelRule : AopObject
    {
        /// <summary>
        /// 标签id
        /// </summary>
        [XmlElement("label_id")]
        public string LabelId { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        [XmlElement("label_name")]
        public string LabelName { get; set; }

        /// <summary>
        /// 标签值，当有多个取值时用英文","分隔，不允许传入下划线"_"、竖线"|"或者空格" "，多个取值时，用户符合其中一个值即可命中该套扩展区
        /// </summary>
        [XmlElement("label_value")]
        public string LabelValue { get; set; }
    }
}
