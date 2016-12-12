using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;

namespace FirewallClientTest
{
    class DevicesCheck : IDevicesCheck 
    {
        private static List<string> devIP_list = new List<string>();

        public List<string> CheckDevices(string start_IP, string end_IP)
        {
            CaptureDeviceList devices = CaptureDeviceList.Instance;

            // If no devices were found print an error
            if (devices.Count < 1)
            {
               // Console.WriteLine("No devices were found on this machine");
                return null;
            }

            //Console.WriteLine("\nThe following devices are available on this machine:");
            //Console.WriteLine("----------------------------------------------------\n");

            // Print out the available network devices
            /*
            foreach (ICaptureDevice dev in devices)
                Console.WriteLine("{0} {1}", dev.Name, dev.Description); */

            devIP_list.Clear();

            ICaptureDevice device = devices[0];

            device.OnPacketArrival +=
                new PacketArrivalEventHandler(device_OnPacketArrival);

            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            string filter = "ip and udp";
            device.Filter = filter;
            
            device.StartCapture();

            IDevicesScan devScan = new DevicesScan();
            devScan.ScanDevice(start_IP, end_IP);

            device.StopCapture();

            device.Close();

            return devIP_list;
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
                    devIP_list.Add(srcIp.ToString());
                }
            }
        }
    }
}
