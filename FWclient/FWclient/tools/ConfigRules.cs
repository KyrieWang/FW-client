using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    /// <summary>
    /// 定义防火墙规则
    /// </summary>
    class ConfigRules : IConfigRules
    {
        private String default_rules = "iptables -P FORWARD  DROP";
        private DeviceForm devform;

        public ConfigRules(DeviceForm devform)
        {
            this.devform = devform;
        }

        public void ConfigModbusTcpRules(ModbusTcpRulesForm mtrf)
        {
            RulesDataProcess.ModbusTcpRulesDataProcess(mtrf);

            String dpi_pro = "modbusTcp";
            string dpi_rules_from_master_to_slave = "iptables" + " -A" + " " + "FORWARD" + " " + "-p tcp" + " " + "--dport" + "=" + "502" + " " + "-s " + mtrf.getSrc_IP() + " " + "-d" + " " + mtrf.getDst_IP() + " " + "-m" + " " + dpi_pro + " " + "--min-addr" + " " + mtrf.getMin_addr() + " " + "--max-addr" + " " + mtrf.getMax_addr() + " " + "--lfc-flag " + mtrf.getLfc_flag() + " " + "--hfc-flag " + mtrf.getHfc_flag() + " -j" + " " + "LOG" + " " + "--log-prefix " + "\"" + "modbustcp" + "\"";
            string dpi_rules_from_slave_to_master = "iptables" + " -A" + " " + "FORWARD" + " " + "-p tcp" + " " + "--sport" + "=" + "502" + " " + "-s " + mtrf.getDst_IP() + " " + "-d" + " " + mtrf.getSrc_IP() + " " + "-m" + " " + dpi_pro + " " + "--min-addr" + " " + mtrf.getMin_addr() + " " + "--max-addr" + " " + mtrf.getMax_addr() + " " + "--lfc-flag " + mtrf.getLfc_flag() + " " + "--hfc-flag " + mtrf.getHfc_flag() + " -j" + " " + "LOG" + " " + "--log-prefix " + "\"" + "modbustcp" + "\"";

            SendInfo sendcmd = new SendInfo(devform);

            sendcmd.SendConfigInfo(default_rules);
            sendcmd.SendConfigInfo(dpi_rules_from_master_to_slave);
            sendcmd.SendConfigInfo(dpi_rules_from_slave_to_master);
        }

        public void ConfigOPCRules(OPCRulesForm orf)
        {
            String opc_rules_from_client_to_server = "iptables -A FORWARD -p tcp -s " + orf.getSrc_IP() + " -d " + orf.getDst_IP() + " --dport 135 -m state --state ESTABLISHED -j NFQUEUE --queue-num 1";
            String opc_rules_from_server_to_client = "iptables -A FORWARD -p tcp -s " + orf.getDst_IP() + " -d " + orf.getSrc_IP() + " --sport 135 -m state --state ESTABLISHED -j NFQUEUE --queue-num 1";

            SendInfo sendcmd = new SendInfo(devform);

            sendcmd.SendConfigInfo(opc_rules_from_client_to_server);
            sendcmd.SendConfigInfo(opc_rules_from_server_to_client);
        }

        public void ConfigDNP3Rules(DNP3RulesForm dnp3rf)
        {
            String dnp3_rules_from_client_to_server_new = "iptables -A FORWARD -p tcp -s " + dnp3rf.getSrc_IP() + " -d " + dnp3rf.getDst_IP() + " --dport 20000 -m state --state NEW -j ACCEPT";
            String dnp3_rules_from_server_to_client_new = "iptables -A FORWARD -p tcp -s " + dnp3rf.getDst_IP() + " -d " + dnp3rf.getSrc_IP() + " --sport 20000 -m state --state NEW -j ACCEPT";
            String dnp3_rules_from_client_to_server_established = "iptables -A FORWARD -p tcp -s " + dnp3rf.getSrc_IP() + " -d " + dnp3rf.getDst_IP() + " --dport 20000 -m state --state ESTABLISHED -j ACCEPT";
            String dnp3_rules_from_server_to_client_established = "iptables -A FORWARD -p tcp -s " + dnp3rf.getDst_IP() + " -d " + dnp3rf.getSrc_IP() + " --sport 20000 -m state --state ESTABLISHED -j ACCEPT";

            SendInfo sendcmd = new SendInfo(devform);

            sendcmd.SendConfigInfo(dnp3_rules_from_client_to_server_new);
            sendcmd.SendConfigInfo(dnp3_rules_from_server_to_client_new);
            sendcmd.SendConfigInfo(dnp3_rules_from_client_to_server_established);
            sendcmd.SendConfigInfo(dnp3_rules_from_server_to_client_established);
        }

        public void ClearAllRules()
        {
            SendInfo sendcmd = new SendInfo(devform);

            sendcmd.SendConfigInfo("iptables -P FORWARD ACCEPT");
            sendcmd.SendConfigInfo("iptables -F");
            sendcmd.SendConfigInfo("iptables -X");
            sendcmd.SendConfigInfo("iptables -Z");
            //sendcmd.SendConfigInfo("kill 'ps -e | grep snort | awk '{print $1}' |head -1"+"!");

        }
    }
}
