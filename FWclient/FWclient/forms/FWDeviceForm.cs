using FirewallClientTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWclient.forms
{
    /// <summary>
    /// 防火墙设备
    /// </summary>
    class FWDeviceForm : DeviceForm
    {
        private string fw_lab;  //防火墙设备MAC地址，唯一的标识出该防火墙
        //private Dictionary<string, string> ProDevIP_MAC_dict = new Dictionary<string , string>();   //连接在该防火墙上的受保护设备的IP-MAC
        private List<string> ProdevIP_list = new List<string>();
        private List<ProtecDeviceForm> ProtecDev_list = new List<ProtecDeviceForm>();

        public FWDeviceForm(string fw_IP, int fw_port, string fw_lab, string dev_IP, string dev_MAC) : base (fw_IP, fw_port)
        {
            ProtecDeviceForm protecDev = new ProtecDeviceForm(dev_IP, dev_MAC);
            ProtecDev_list.Add(protecDev);
            this.fw_lab = "FW-" + fw_lab;
            //this.ProDevIP_MAC_dict.Add(dev_IP, dev_MAC);
        }

        public void setFw_lab(string fw_lab)
        {
            this.fw_lab = fw_lab;
        }

        public string getFw_lab()
        {
            return fw_lab;
        }
        /*
        public void addDev_IP_MAC(string dev_IP, string dev_MAC)
        {
            if(!ProDevIP_MAC_dict.ContainsKey(dev_IP))
                ProDevIP_MAC_dict.Add(dev_IP, dev_MAC);
        }
        */
        /*
        public Dictionary<string, string> getDevIP_MAC_dict()
        {
            return ProDevIP_MAC_dict;
        }
        */
        public void addProtecDev(ProtecDeviceForm protecDev)
        {
            ProtecDev_list.Add(protecDev);
        }

        public List<ProtecDeviceForm> getProtecDev_list()
        {
            return ProtecDev_list;
        }

        public void addProtecDevIP(string dev_IP)
        {
            ProdevIP_list.Add(dev_IP);
        }

        public List<string> getProtecDevIP_list()
        {
            return ProdevIP_list;
        }
    }
}
