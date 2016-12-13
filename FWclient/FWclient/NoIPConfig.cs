using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWclient.forms;
using System.Net.Sockets;
using System.Net;

namespace FWclient
{
    class NoIPConfig : INoIPConfig
    {
        void INoIPConfig.NoipConfig(FWDeviceForm fw_dev)
        {
            //string fwip = fw_dev.getFw_IP();
            //List<string> deviplist = fw_dev.getDevIP_list();
            string cmd = "ifconfig br0 down;ifconfig br0 0.0.0.0 up";

            byte[] head = { 0x0f, 0x0f };   //自定义数据包包头
            byte[] body = Encoding.ASCII.GetBytes(cmd + "!");
            byte[] data = head.Concat(body).ToArray();

            UdpClient client = null;
            IPAddress remoteIP = IPAddress.Parse(fw_dev.getFw_IP());
            int remotePort = 22222;
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);

            client = new UdpClient();

#if debug
            Console.WriteLine("start noIP sending:");
#endif

            client.Send(data, data.Length, remotePoint);
            client.Close();

#if debug
            Console.WriteLine("send noIP successfully!");
#endif
        }
    }
}
