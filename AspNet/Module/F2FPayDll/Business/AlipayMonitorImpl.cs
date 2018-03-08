using Aop.Api.Response;
using Com.Alipay.Domain;
using Aop.Api;
using Aop.Api.Request;
using System.Text;
using System.Threading;
using Com.Alipay.Model;
using Com.Alipay.Business;


namespace Com.Alipay
{
    /// <summary>
    /// AlipayTradePayImpl 的摘要说明
    /// </summary>
    public class AlipayMonitorImpl : IAlipayMonitor
    {

        IAopClient client = null;

        public AlipayMonitorImpl(string monitorUrl, string appId, string merchant_private_key, string format, string version,
     string sign_type, string alipay_public_key, string charset)
        {
            client = new DefaultAopClient(monitorUrl, appId, merchant_private_key, format, version,
           sign_type, alipay_public_key, charset);

        }


        #region 接口方法

        public AlipayF2FMonitorResult mcloudMonitor(AlipayMonitorContentBuilder build)
        {
            AlipayF2FMonitorResult result = new AlipayF2FMonitorResult();
            try
            {
                MonitorHeartbeatSynRequest monitorRequest = new MonitorHeartbeatSynRequest();
                monitorRequest.BizContent = build.BuildJson();
                result.response = client.Execute(monitorRequest);
                return result;
            }
            catch
            {
                result.response = null;
                return result;
            }
        }


        #endregion


        #region 内部方法

        #endregion
    }
}