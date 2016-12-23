using FirewallClientTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FWclient.forms
{
    class ProtecDeviceForm : DeviceForm
    {
        private string dev_MAC;
        private string dev_type;

        public ProtecDeviceForm(string dev_IP, string dev_MAC):base(dev_IP)
        {
            this.dev_MAC = dev_MAC;
        }

        public string getDev_MAC()
        {
            return dev_MAC;
        }

        public string getDev_type()
        {
            string[] macArray = dev_MAC.Split(':');
            string macQuery = macArray[0] + "-" + macArray[1] + "-" + macArray[2];
            string connStr = "Server=172.16.10.62;Database=MACs;Uid=root;Pwd=123456;CharSet=utf8";
            string sqlSearch = "select Manufacturers from macs where Macs="+"'"+ macQuery + "'";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sqlSearch, con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.HasRows)
                        dev_type = reader.GetString(0);
                    else
                        dev_type = "Unknown Device";
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("exceptionInfo {0}", e);
            }
            finally
            {
                con.Close();
            }

            return dev_type;
        }
    }
}
