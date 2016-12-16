using FWclient.forms;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;

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
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            string filter = "ip and udp";
            device.Filter = filter;
            device.StartCapture();

            IDevicesScan devScan = new DevicesScan();
            devScan.ScanDevice(start_IP, end_IP);

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
                    byte[] payload = udpPacket.PayloadData;
                    string fw_IP = System.Text.Encoding.Default.GetString(payload); //防火墙IP
                    string dev_IP = srcIp.ToString();   //受保护设备IP

                    if ((fwIP_list.Contains(fw_IP)))
                    {
                        foreach (FWDeviceForm fwdev in fw_list)
                        {
                            if (fwdev.getDev_IP() == fw_IP)
                            {
                                fwdev.addDev_IP(dev_IP);
                            }
                        }
                    }
                    else
                    {
                        FWDeviceForm fw_dev = new FWDeviceForm(fw_IP, 22222, fw_num.ToString(), dev_IP);
                        fwIP_list.Add(fw_IP);
                        fw_list.Add(fw_dev);
                        fw_num++;
                    }
                }
            }
        }
    }
}
