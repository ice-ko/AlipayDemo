using Aop.Api.Response;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Com.Alipay
{
    /// <summary>
    /// IAlipayMonitor 的摘要说明
    /// </summary>
    public interface IAlipayMonitor
    {

        //云监控接口
        AlipayF2FMonitorResult mcloudMonitor(AlipayMonitorContentBuilder builder);
    }

}