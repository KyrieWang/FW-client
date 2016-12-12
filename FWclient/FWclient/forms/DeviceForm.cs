using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    /// <summary>
    /// 受保护设备
    /// </summary>
    class DeviceForm
    {
        private string dev_IP;  //设备ip
        private int dev_port;   //设备端口

        public void setDev_IPAndDev_port(string dev_IP, int dev_port)
        {
            this.dev_IP = dev_IP;
            this.dev_port = dev_port;
        }

        public string getDev_IP()
        {
            return dev_IP;
        }

        public int getDev_port()
        {
            return dev_port;
        }
    }
}
