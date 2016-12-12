using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallClientTest
{
    /// <summary>
    /// modbusTcp规则
    /// </summary>
    class ModbusTcpRulesForm
    {
        private String dst_IP;
        private String src_IP;
        private String min_addr;
        private String max_addr;
        private long lfc_flag = 0;
        private long hfc_flag = 0;
        private string[] function_code_select;
        private bool value_select = false;

        public bool getValue_select()
        {
            return value_select;
        }

        public string getDst_IP()
        {
            return dst_IP;
        }

        public string getSrc_IP()
        {
            return src_IP;
        }

        public String getMin_addr()
        {
            return min_addr;
        }

        public String getMax_addr()
        {
            return max_addr;
        }

        public long getLfc_flag()
        {
            return lfc_flag;
        }

        public long getHfc_flag()
        {
            return hfc_flag;
        }

        public String[] getFunction_code_select()
        {
            return function_code_select;
        }

        public void setLfc_flag(long lfc_flag)
        {
            this.lfc_flag = lfc_flag;
        }

        public void setHfc_flag(long hfc_flag)
        {
            this.hfc_flag = hfc_flag;
        }

        public void setIP_Addr_Funcode(string dst_IP, string src_IP, String min_addr, String max_addr, string[] function_code_select)
        {
            this.dst_IP = dst_IP;
            this.src_IP = src_IP;
            this.min_addr = min_addr;
            this.max_addr = max_addr;

            if (function_code_select[0] != null)
            {
                this.function_code_select = function_code_select;
                value_select = true;
            }
            else
            {
                value_select = false;
            }
        }
    }
}
