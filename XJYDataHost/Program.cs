using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace XJYDataHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(XJYDataLib.XJYDataService)))
            {
                host.Open();

                Console.WriteLine("WCF 已经启动@" + DateTime.Now);

                Console.ReadKey();
            }
        }
    }
}
