﻿using FWclient.forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWclient
{
    interface INoIPConfig
    {
        bool NoipConfig(FWDeviceForm fw_dev);
    }
}
