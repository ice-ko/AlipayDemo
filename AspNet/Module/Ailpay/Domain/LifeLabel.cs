using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Aop.Api.Domain
{
    /// <summary>
    /// LifeLabel Data Structure.
    /// </summary>
    [Serializable]
    public class LifeLabel : AopObject
    {
        /// <summary>
        /// 标签类目
        /// </summary>
        [XmlElement("category")]
        public string Category { get; set; }

        /// <summary>
        /// 标签值类型
        /// </summary>
        [XmlElement("data_type")]
        public string DataType { get; set; }

        /// <summary>
        /// 标签英文代码
        /// </summary>
        [XmlElement("label_code")]
        public string LabelCode { get; set; }

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
        /// 操作符代码
        /// </summary>
        [XmlElement("operator")]
        public string Operator { get; set; }

        /// <summary>
        /// 文本与取值的映射关系
        /// </summary>
        [XmlArray("options")]
        [XmlArrayItem("option")]
        public List<Option> Options { get; set; }

        /// <summary>
        /// 标签类型，目前分为common（通用标签）、custom（生活号自定义标签）、cloud（云实验室标签）
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
