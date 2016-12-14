#define debug

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWclient.forms;
using System.Net.Sockets;
using System.Net;
using FirewallClientTest;
using SharpPcap;

namespace FWclient
{
    class NoIPConfig : INoIPConfig
    {
        bool INoIPConfig.NoipConfig(FWDeviceForm fw_dev)
        {
            string cmd = "ifconfig br0 down && ifconfig br0 0.0.0.0 up";

            SendInfo sendcmd = new SendInfo(fw_dev);
            if (sendcmd.SendConfigInfo(cmd))
            {
                fw_dev.setDev_IP("0.0.0.0");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
