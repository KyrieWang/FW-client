#define debug

using FWclient.forms;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FirewallClientTest
{
    class DevicesCheck : IDevicesCheck 
    {
        private static  List<FWDeviceForm> fw_list = new List<FWDeviceForm>(); //单次扫描到的防火墙设备
        private static List<string> fwIP_list = new List<string>(); //单次扫描到的防火墙设备的IP
        private static int  fw_num = 1; //防火墙标识计数

        public List<FWDeviceForm> CheckDevices(string start_IP, string end_IP)
        {
            fw_list.Clear();
            fwIP_list.Clear();

            CaptureDeviceList devices = CaptureDeviceList.Instance;
            // If no devices were found print an error
            if (devices.Count < 1)
            {
                // Console.WriteLine("No devices were found on this machine");
                return fw_list;
            }
            ICaptureDevice device = devices[0];
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            int readTimeoutMilliseconds = 100;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            string filter = "ip and udp";
            device.Filter = filter;
            device.StartCapture();

            IDevicesScan devScan = new DevicesScan();
            int ip_num = devScan.ScanDevice(start_IP, end_IP);

            Thread.Sleep(ip_num * 5000);
#if debug
            Console.WriteLine("扫描完成！！！");
#endif
            device.StopCapture();
            device.Close();
            return fw_list;
        }

        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            //var time = e.Packet.Timeval.Date;
            //var len = e.Packet.Data.Length;

            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            UdpPacket udpPacket = UdpPacket.GetEncapsulated(packet);

            if (udpPacket != null)
            {
                var ipPacket = (PacketDotNet.IpPacket)udpPacket.ParentPacket;
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                //System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                int srcPort = udpPacket.SourcePort;
                int dstPort = udpPacket.DestinationPort;

                if (srcPort == 30330 && dstPort == 30331)
                {
                    #if debug
                        Console.WriteLine("捕获到返回信息！！！");
                    #endif

                    byte[] payload = udpPacket.PayloadData;
                    string[] sArray_IP_MAC = System.Text.Encoding.Default.GetString(payload).Split('&'); 
                    string fw_IP = sArray_IP_MAC[0];    //防火墙IP
                    string dev_mac = sArray_IP_MAC[1];  //受保护设备MAC
                    string dev_IP = srcIp.ToString();   //受保护设备IP

                    if ((fwIP_list.Contains(fw_IP)))    //如果已经存在
                    {
                        foreach (FWDeviceForm fwdev in fw_list)
                        {
                            if (fwdev.getDev_IP() == fw_IP)
                            {
                                fwdev.addDev_IP_MAC(dev_IP, dev_mac);
                            }
                        }
                    }
                    else
                    {
                        FWDeviceForm fw_dev = new FWDeviceForm(fw_IP, 22222, fw_num.ToString(), dev_IP, dev_mac);   //如果不存在
                        fwIP_list.Add(fw_IP);
                        fw_list.Add(fw_dev);
                        fw_num++;
                    }
                    #if debug
                        Console.WriteLine("保存设备信息！！！");
                    #endif
                }
            }
        }
    }
}
