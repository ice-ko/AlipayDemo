using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayEcoEduKtStudentModifyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoEduKtStudentModifyModel : AopObject
    {
        /// <summary>
        /// 修改后的姓名
        /// </summary>
        [XmlElement("child_name")]
        public string ChildName { get; set; }

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
        /// 区分ISV操作，“D”表示删除，“U”表示更新，区分大小写。  如果为U，则学生名字，学号，省份证至少填写一项
        /// </summary>
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 修改后的学号
        /// </summary>
        [XmlElement("student_code")]
        public string StudentCode { get; set; }

        /// <summary>
        /// 修改后的身份证号码
        /// </summary>
        [XmlElement("student_identify")]
        public string StudentIdentify { get; set; }

        /// <summary>
        /// 学生编号，发送账单接口返回的学生编号
        /// </summary>
        [XmlElement("student_no")]
        public string StudentNo { get; set; }
    }
}
