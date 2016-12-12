using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    interface IConfigRules
    {
        /// <summary>
        /// 配置modbusTcp规则
        /// </summary>
        /// <param name="mtrf">modbusTcp规则实例</param>
        void ConfigModbusTcpRules(ModbusTcpRulesForm mtrf);

        /// <summary>
        /// 配置OPC规则
        /// </summary>
        /// <param name="orf">OPC规则实例</param>
        void ConfigOPCRules(OPCRulesForm orf);

        /// <summary>
        /// 配置DNP3规则
        /// </summary>
        /// <param name="dnp3rf">DNP3规则实例</param>
        void ConfigDNP3Rules(DNP3RulesForm dnp3rf);
        
        /// <summary>
        /// 清除规则
        /// </summary>
        void ClearAllRules();
    }
}
