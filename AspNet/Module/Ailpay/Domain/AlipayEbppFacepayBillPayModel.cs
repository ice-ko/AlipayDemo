using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayEbppFacepayBillPayModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEbppFacepayBillPayModel : AopObject
    {
        /// <summary>
        /// 账期
        /// </summary>
        [XmlElement("bill_date")]
        public string BillDate { get; set; }

        /// <summary>
        /// 户号
        /// </summary>
        [XmlElement("bill_key")]
        public string BillKey { get; set; }

        /// <summary>
        /// 业务类型英文名称，固定传JF，表示缴费
        /// </summary>
        [XmlElement("biz_type")]
        public string BizType { get; set; }

        /// <summary>
        /// 出账机构英文名称
        /// </summary>
        [XmlElement("charge_inst")]
        public string ChargeInst { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [XmlElement("extend_field")]
        public string ExtendField { get; set; }

        /// <summary>
        /// 滞纳金额，单位：元
        /// </summary>
        [XmlElement("fine_amount")]
        public string FineAmount { get; set; }

        /// <summary>
        /// 机构唯一ID（要求全局唯一）
        /// </summary>
        [XmlElement("inst_no")]
        public string InstNo { get; set; }

        /// <summary>
        /// ISV交易流水号（要求全局唯一）
        /// </summary>
        [XmlElement("out_order_no")]
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 支付金额（包含滞纳金），单位：元
        /// </summary>
        [XmlElement("pay_amount")]
        public string PayAmount { get; set; }

        /// <summary>
        /// 商户partnerId
        /// </summary>
        [XmlElement("pid")]
        public string Pid { get; set; }

        /// <summary>
        /// 子业务类型英文名称，ELECTRIC-电费，WATER-水费，GAS-燃气费
        /// </summary>
        [XmlElement("sub_biz_type")]
        public string SubBizType { get; set; }

        /// <summary>
        /// 用户支付宝付款码
        /// </summary>
        [XmlElement("user_identity_code")]
        public string UserIdentityCode { get; set; }
    }
}
