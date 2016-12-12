using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    /// <summary>
    /// 防火墙规则管理，包括添加和删除规则
    /// </summary>
    class RulesManage : IRulesManage
    {
        public void ClearAllRules(string dev_IP)
        {
            DeviceForm devform = new DeviceForm();
            devform.setDev_IPAndDev_port(dev_IP,22222);

            IConfigRules configDevice = new ConfigRules(devform);
            configDevice.ClearAllRules();
        }
        
        public void AddDNP3Rules(string dst_IP, string src_IP, string dev_IP, bool log_record)
        {
            DNP3RulesForm dnp3rf = new DNP3RulesForm();
            dnp3rf.setDst_IPAndSrc_IP(dst_IP, src_IP);

            DeviceForm devform = new DeviceForm();
            devform.setDev_IPAndDev_port(dev_IP, 22222);

            IConfigRules configDevice = new ConfigRules(devform);
            configDevice.ConfigDNP3Rules(dnp3rf);

            if (log_record)
            {
                //记录日志
            }
        }

        public void AddModbusTcpRules(string dst_IP, string src_IP, string min_addr, string max_addr, string[] function_code_select, string dev_IP, bool log_record)
        {
            ModbusTcpRulesForm mtrf = new ModbusTcpRulesForm();
            mtrf.setIP_Addr_Funcode(dst_IP, src_IP, min_addr, max_addr, function_code_select);

            DeviceForm devform = new DeviceForm();
            devform.setDev_IPAndDev_port(dev_IP, 22222);

            IConfigRules configDevice = new ConfigRules(devform);
            configDevice.ConfigModbusTcpRules(mtrf);
            

            if (log_record)
            {
                //记录日志
            }
        }

        public void AddOPCRules(string dst_IP, string src_IP, string dev_IP, bool log_record)
        {
            OPCRulesForm orf = new OPCRulesForm();
            orf.setDst_IPAndSrc_IP(dst_IP, src_IP);

            DeviceForm devform = new DeviceForm();
            devform.setDev_IPAndDev_port(dev_IP, 22222);

            IConfigRules configDevice = new ConfigRules(devform);
            configDevice.ConfigOPCRules(orf);

            if (log_record)
            {
                //记录日志
            }
        }
    }
}
