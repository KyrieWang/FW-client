//#define debug

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FirewallClientTest
{
    /// <summary>
    /// 向防火墙设备发送信息
    /// </summary>
    class SendInfo
    {
        DeviceForm devform;

        /*初始化设备*/
        public SendInfo(DeviceForm devform)
        {
            this.devform = devform;
        }

        /// <summary>
        /// 发送配置信息
        /// </summary>
        /// <param name="cmd">需要配置的规则</param>
        public void SendConfigInfo(string cmd)
        {
            byte[] head = { 0x0f, 0x0f };   //自定义数据包包头
            byte[] body = Encoding.ASCII.GetBytes(cmd + "!");
            byte[] data = head.Concat(body).ToArray();

            UdpClient client = null;
            IPAddress remoteIP = IPAddress.Parse(devform.getDev_IP());
            int remotePort = devform.getDev_port();
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);

            client = new UdpClient();

#if debug
            Console.WriteLine("start sending:");
#endif

            client.Send(data, data.Length, remotePoint);
            client.Close();

#if debug
            Console.WriteLine("send successfully!");
#endif
        }

        /// <summary>
        /// 发送扫描设备的数据
        /// </summary>
        public void SendCheckInfo()
        {
            string mac = GetLocalMacAddr.GetMacAddr();  //获取本机MAC地址
            //Console.WriteLine("local mac is {0}", mac);

            byte[] head = { 0x0f, 0x0f };   //自定义数据包包头
            byte[] body = Encoding.ASCII.GetBytes(mac + "!");
            byte[] data = head.Concat(body).ToArray();

            UdpClient client = null;
            IPAddress remoteIP = IPAddress.Parse(devform.getDev_IP());
            int remotePort = devform.getDev_port();
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);

            client = new UdpClient(); 

#if debug
            Console.WriteLine("start sending:");
#endif

            client.Send(data, data.Length, remotePoint);
            client.Close();

#if debug
            Console.WriteLine("send successfully!");
#endif
            
        }
    }
}
