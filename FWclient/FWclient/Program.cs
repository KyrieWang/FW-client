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
    /*
    IDevicesCheck devConfirm = new DevicesCheck();
    //devConfirm.CheckDevices();
    List<FWDeviceForm> fws = devConfirm.CheckDevices("172.16.10.108", "172.16.10.110");
    foreach (FWDeviceForm fw in fws)
    {
        string fwip = fw.getDev_IP();
        string fwlabl = fw.getFw_lab();
        List<string> deviplist = fw.getDevIP_list();

        Console.WriteLine("防火墙设备IP : {0} 防火墙设备编号 : {1}", fwip, fwlabl);
        Console.WriteLine("关联的受保护IP :");
        foreach (string  devip in deviplist )
        {
            Console.WriteLine(devip);
        }

        Console.WriteLine("无IP模式配置测试 :");
        INoIPConfig noip = new NoIPConfig();
        if (noip .NoipConfig (fw))
        {
            Console.WriteLine("无IP模式配置成功！！！");
            Console.WriteLine("IP :{0}", fw.getDev_IP());
        }
        else
        {
            Console.WriteLine("无IP模式配置失败！！！");
            Console.WriteLine("IP :{0}", fw.getDev_IP());
        } 

    } */
  /*
    FWDeviceForm fwdev = new FWDeviceForm("172.16.10.219", 22222, "test","172.16.10.109");
    INoIPConfig noip = new NoIPConfig();
    Console.WriteLine("无IP模式配置测试 :");
    if (noip.NoipConfig(fwdev))
    {
        Console.WriteLine("无IP模式配置成功！！！");
        Console.WriteLine("IP :{0}", fwdev.getDev_IP());
    }
    else
    {
        Console.WriteLine("无IP模式配置失败！！！");
        Console.WriteLine("IP :{0}", fwdev.getDev_IP());
    } 
    */


            IDevicesCheck devConfirm = new DevicesCheck();
            List<FWDeviceForm> fws = devConfirm.CheckDevices("172.16.10.19", "172.16.10.25");
            Console.WriteLine("打印扫描结果 :");
            foreach (FWDeviceForm fw in fws)
            {
                string fwip = fw.getDev_IP();
                string fwlabl = fw.getFw_lab();
                List<ProtecDeviceForm> protecDev_list = fw.getProtecDev_list();

                Console.WriteLine("防火墙设备IP : {0} 防火墙设备编号 : {1}", fwip, fwlabl);
                Console.WriteLine("关联的受保护设备 :");
                /*
                foreach (var item in devipmac_dict)
                {
                    Console.WriteLine("IP {0}   MAC {1}", item.Key, item.Value);
                }
                */
                foreach (var item in protecDev_list)
                {
                    string dev_type = item.getDev_type();
                    Console.WriteLine("IP {0}   MAC {1} 设备制造商 {2}", item.getDev_IP(), item.getDev_MAC(),dev_type);
       
                }
            }

                       
#if debug
            Console.ReadLine();
#endif
        }
    }
}
