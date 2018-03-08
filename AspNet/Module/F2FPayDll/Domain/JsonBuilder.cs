using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;


namespace Com.Alipay.Domain
{

    /// <summary>
    /// Class1 的摘要说明
    /// </summary>
    public abstract class JsonBuilder
    {

        // 验证bizContent对象
         public abstract bool Validate();

        // 将bizContent对象转换为json字符串
        public string BuildJson()
         {

            
             JavaScriptSerializer jss = new JavaScriptSerializer();
             try
             {
                 return jss.Serialize(this);
             }
             catch (Exception ex)
             {

                 throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
             }
         }
    }
}