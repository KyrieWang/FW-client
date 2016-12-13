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
    class FWDeviceForm
    {
        private string fw_IP;   //防火墙设备初始IP
        private string fw_lab;  //防火墙设备MAC地址，唯一的标识出该防火墙
        private List<string> devIP_list = new List<string>();   //连接在该防火墙上的受保护设备

        public FWDeviceForm(string fw_IP, string fw_lab, string devIP)
        {
            this.fw_IP = fw_IP;
            this.fw_lab = "FW-" + fw_lab;
            this.devIP_list.Add(devIP);
        }

        public void setFw_IP(string fw_IP)
        {
            this.fw_IP = fw_IP;
        }

        public string getFw_IP()
        {
            return fw_IP;
        }

        public void setFw_lab(string fw_lab)
        {
            this.fw_lab = fw_lab;
        }

        public string getFw_lab()
        {
            return fw_lab;
        }

        public void addDev_IP(string devIP)
        {
            devIP_list.Add(devIP);
        }

        public List<string> getDevIP_list()
        {
            return devIP_list;
        } 
    }
}
