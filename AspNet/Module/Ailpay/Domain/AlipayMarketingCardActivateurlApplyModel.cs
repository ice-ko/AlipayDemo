using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayMarketingCardActivateurlApplyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingCardActivateurlApplyModel : AopObject
    {
        /// <summary>
        /// 会员卡开卡表单提交后回调地址。  1.该地址不可带参数，如需回传参数，可设置out_string入参。
        /// </summary>
        [XmlElement("callback")]
        public string Callback { get; set; }

        /// <summary>
        /// 扩展信息，会员领卡完成后将此参数原样带回商户页面。
        /// </summary>
        [XmlElement("out_string")]
        public string OutString { get; set; }

        /// <summary>
        /// 会员卡模板id。使用会员卡模板创建接口(alipay.marketing.card.template.create)返回的结果
        /// </summary>
        [XmlElement("template_id")]
        public string TemplateId { get; set; }
    }
}
