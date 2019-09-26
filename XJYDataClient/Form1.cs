using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XJYDataClient
{
    public partial class Form1 : Form
    {
        XJYDataServiceClient client = null;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            client = new XJYDataServiceClient();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 始终关闭客户端。
            client.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            var msg =  client.ServiceTest("WCF_Client:服务通讯测试消息 ");
            var url = client.Endpoint.ListenUri.OriginalString;
            MessageBox.Show(msg+System.Environment.NewLine+ url);
           
        }
        private async void button2_Click(object sender, EventArgs e)
        { 
            XJYDataLib.RequestResult result= await client.DownloadFileAsync(GetCustomerInfo());
            if (result != null)
                MessageBox.Show(result.Descripts);
        }

        private async void button3_Click(object sender, EventArgs e)
        {            
            XJYDataLib.RequestResult result = await client.ImportData2EasAsync(GetCustomerInfo());
            if (result != null)
                MessageBox.Show(result.Descripts);
        }
        private XJYDataLib.CustomerInfo GetCustomerInfo()
        {
            XJYDataLib.CustomerInfo customerInfo = new XJYDataLib.CustomerInfo();
            customerInfo.CustomerID = "e703ffdf-cdf9-4111-97ee-0747f531ebb2";
            customerInfo.WP_GUID = "e703ffdf-cdf9-4111-97ee-0747f531ebb2";
            customerInfo.WP_Host = "pan.hptjcpa.com.cn";
            customerInfo.WP_PathName = "/FBBD1986-5648-435B-BE27-168371C1C8C3/CwV114_001.34301_合肥圣达电子科技实业有限_2019.001";
            customerInfo.AuditYear = 2019;
            customerInfo.ProjectID = "CwV114_001";
            customerInfo.BeginDate = DateTime.Parse("2019-01-01");
            customerInfo.EndDate = DateTime.Parse("2019-12-31");
            return customerInfo;
        }
    }   
}
