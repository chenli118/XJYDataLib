using NetDiskLibrary;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XJYDataLib.XJYDataImp;

namespace XJYDataLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“XJYDataService”。
    public class XJYDataService : IXJYDataService
    {
        private string DataFileFolder= Path.Combine(Directory.GetCurrentDirectory(),"XJYData");
        public  RequestResult DownloadFile(CustomerInfo cusInfo)
        {
            
            RequestResult requestResult = new RequestResult();
            requestResult.MethodName = "DownloadFile";
            if (cusInfo == null)
            {
                requestResult.Status = "ERROR";
                requestResult.Contents = "ERROR : 参数不能为空！";
                return requestResult;
            }
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string XdataAccount = config.AppSettings.Settings["XdataAccount"].Value;
            if (!string.IsNullOrEmpty(XdataAccount))
            {
                string logname = XdataAccount.Split('#')[1];
                string logpwd = XdataAccount.Split('#')[0];
                var ndc = NetDiskClient.getInstance(cusInfo.WP_Host, logname, logpwd);
                
                FileInfo fileInfo = new FileInfo(cusInfo.WP_PathName);
                var tempFolder = DataFileFolder+cusInfo.WP_PathName.Replace(fileInfo.Name,""); 
                if (!Directory.Exists(tempFolder)) Directory.CreateDirectory(tempFolder);
                var tempFile = Path.Combine(tempFolder.Replace('/','\\'), fileInfo.Name);
                if (File.Exists(tempFile))
                {
                    File.SetAttributes(tempFile, FileAttributes.Normal);
                    File.Delete(tempFile);                
                }
                 
                string strRet = string.Empty;
                bool bRet= ndc.DownloadFile(cusInfo.WP_GUID, cusInfo.WP_PathName, tempFile,out strRet);
                if (bRet)
                {
                    requestResult.Status = "SUCCESS";
                    requestResult.Contents = "FileName :"+ tempFile;
                    requestResult.Descripts = cusInfo.WP_PathName + ",已完成下载";
                }
                else
                {
                    requestResult.Status = "ERROR";
                    requestResult.Descripts = cusInfo.WP_PathName + ",下载失败！";
                }
            }
            return requestResult;
        }

        public RequestResult ImportData2Eas(CustomerInfo customerInfo)
        {
            RequestResult requestResult = new RequestResult();
            requestResult.MethodName = "ImportData2Eas";
            if (customerInfo == null)
            {
                requestResult.Status = "ERROR";
                requestResult.Contents = "ERROR : 参数不能为空！";
                return requestResult;
            }
            requestResult = DownloadFile(customerInfo);
            if (requestResult.Status == "SUCCESS")
            {
                var farr = requestResult.Contents.Split(':');
                var fname = farr[1]+":"+ farr[2];
                PDT2SDT pDT = new PDT2SDT(fname, customerInfo);
                requestResult =pDT.Start();
            }
            return requestResult;
        }

        public string ServiceTest(string message)
        {
            return message + "@ Received! " + DateTime.Now;
        }
    }
}
