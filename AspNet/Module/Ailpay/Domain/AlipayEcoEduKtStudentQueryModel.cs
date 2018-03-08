using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayEcoEduKtStudentQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoEduKtStudentQueryModel : AopObject
    {
        /// <summary>
        /// Isv pid
        /// </summary>
        [XmlElement("isv_pid")]
        public string IsvPid { get; set; }

        /// <summary>
        /// 学校编号，同步学校返回的编号
        /// </summary>
        [XmlElement("school_no")]
        public string SchoolNo { get; set; }

        /// <summary>
        /// 学校pid
        /// </summary>
        [XmlElement("school_pid")]
        public string SchoolPid { get; set; }

        /// <summary>
        /// 学生编号，发送账单接口返回的学生编号
        /// </summary>
        [XmlElement("student_no")]
        public string StudentNo { get; set; }
    }
}
