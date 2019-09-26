using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XJYDataLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IXJYDataService”。
    [ServiceContract]
    public interface IXJYDataService
    {
        [OperationContract]
        string ServiceTest(string message);       

        [OperationContract]
        RequestResult DownloadFile(CustomerInfo customerInfo);
        [OperationContract]
        RequestResult ImportData2Eas(CustomerInfo customerInfo);
    }

    [DataContract]
    public class RequestResult
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Contents { get; set; }
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string MethodName { get; set; }
        [DataMember]
        public string Descripts { get; set; }
         

    }
    [DataContract]
    public class CustomerInfo
    {
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string ProjectID { get; set; }
        [DataMember]
        public int AuditYear { get; set; }
        [DataMember]
        public DateTime BeginDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string WP_GUID { get; set; }
        [DataMember]
        public string WP_Host { get; set; }
        [DataMember]
        public string WP_PathName { get; set; }

    }

}
