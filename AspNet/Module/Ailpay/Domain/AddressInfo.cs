using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AddressInfo Data Structure.
    /// </summary>
    [Serializable]
    public class AddressInfo : AopObject
    {
        /// <summary>
        /// 商户详细经营地址
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 商户所在城市编码
        /// </summary>
        [XmlElement("city_code")]
        public string CityCode { get; set; }

        /// <summary>
        /// 商户所在区县编码
        /// </summary>
        [XmlElement("district_code")]
        public string DistrictCode { get; set; }

        /// <summary>
        /// 纬度，浮点型,小数点后最多保留6位
        /// </summary>
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 经度，浮点型, 小数点后最多保留6位
        /// </summary>
        [XmlElement("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 商户所在省份编码
        /// </summary>
        [XmlElement("province_code")]
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 地址类型。取值范围：BUSINESS_ADDRESS：经营地址（默认）；REGISTERED_ADDRESS：注册地址
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
