using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeaguePing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> ipDictionary;
        private Dictionary<string, TextBlock> textBlockDictionary;

        public MainWindow()
        {
            InitializeComponent();

            //setup ip dictionareis
            ipDictionary = GetIPDictionary();
            textBlockDictionary = GetTextBlockDictionary();

            foreach (KeyValuePair<string, string> server in ipDictionary)
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(server.Value); //todo consider getting an average rather than single value
                string rtt;
                if (reply.Status.ToString() == "Success")
                {
                    int time = (int)reply.RoundtripTime; //todo round to ceiling 
                    rtt = time.ToString();
                }
                else
                {
                    rtt = "No response";
                }
                TextBlock textBlock;
                textBlockDictionary.TryGetValue(server.Key, out textBlock);
                textBlock.Text = rtt;

            }


        }

        private Dictionary<string, string> GetIPDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("ping_NA", "104.160.131.3");
            dictionary.Add("ping_EUW", "104.160.141.3");
            dictionary.Add("ping_EUNE", "104.160.142.3");
            dictionary.Add("ping_OCE", "104.160.156.1");
            dictionary.Add("ping_LAN", "104.160.136.3");
            return dictionary; 
        }

        private Dictionary<string, TextBlock> GetTextBlockDictionary()
        {
            Dictionary<string, TextBlock> dictionary = new Dictionary<string, TextBlock>();
            dictionary.Add("ping_NA", ping_NA);
            dictionary.Add("ping_EUW", ping_EUW);
            dictionary.Add("ping_EUNE", ping_EUNE);
            dictionary.Add("ping_OCE", ping_OCE);
            dictionary.Add("ping_LAN", ping_LAN);
            return dictionary;
        }
    }
}
