#define debug

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
            List<string> devip_list = devConfirm.CheckDevices("172.16.10.76", "172.16.10.78");
            foreach (string devip in devip_list)
            {
                Console.WriteLine("devip is : {0}", devip);
            }

            IDevicesCheck devconfirm = new DevicesCheck();
            List<string> devIP_list = devconfirm.CheckDevices("172.16.10.76", "172.16.10.78");
            foreach (string devip in devIP_list)
            {
                Console.WriteLine(" ano devip is : {0}", devip);
            }

#if debug
            Console.ReadLine();
#endif
        }
    }
}
