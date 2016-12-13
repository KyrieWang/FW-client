#define debug

using FWclient;
using FWclient.forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    /// <summary>
    /// 接口测试
    /// </summary>
    class Test
    {
        static void Main(string[] args)
        {
            string dev_IP = "172.16.10.77";
            string dst_IP = "172.16.10.77";
            string src_IP = "172.16.10.33";
            string src_startIP = "172.16.10.10";
            string src_endIP = "172.16.10.12";
            string min_addr = "10";
            string max_addr = "100";
            string[] function_code_select = { "01", "02", "03" };

            string startIP = "172.16.10.76";
            string endIP = "172.16.10.78";

            ///RulesManage接口测试

            //IRulesManage rulesmg = new RulesManage();
            //rulesmg.AddDNP3Rules(dst_IP, src_IP, dev_IP);
            //rulesmg.AddOPCRules(dst_IP, src_IP, dev_IP,false);
            //rulesmg.AddModbusTcpRules(dst_IP, src_startIP, src_endIP, min_addr, max_addr, function_code_select, dev_IP ,false);
            //rulesmg.ClearAllRules(dev_IP); 

            ///DeviceScan接口测试

            //IDevicesScan devScan = new DevicesScan();
            //devScan.ScanDevice(startIP, endIP);
            //Console.ReadLine();

            ///DeviceConfirm接口测试
            IDevicesCheck devConfirm = new DevicesCheck();
            //devConfirm.CheckDevices();
            List<FWDeviceForm> fws = devConfirm.CheckDevices("172.16.10.108", "172.16.10.110");
            foreach (FWDeviceForm fw in fws)
            {
                string fwip = fw.getFw_IP();
                string fwlabl = fw.getFw_lab();
                List<string> deviplist = fw.getDevIP_list();

                Console.WriteLine("fwip is : {0} fwlabl is : {1}", fwip, fwlabl);
                Console.WriteLine("devips are :");
                foreach (string  devip in deviplist )
                {
                    Console.WriteLine(devip);
                }

                Console.WriteLine("no ip config start :");
                INoIPConfig noip = new NoIPConfig();
                noip.NoipConfig(fw);
            }

#if debug
            Console.ReadLine();
#endif
        }
    }
}
