using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class LumiLogger : Form
    {
        public LumiLogger()
        {
            InitializeComponent();
        }

        private void esp8266_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");

            var jsonStr = esp8266.ReadLine();

            Console.WriteLine(jsonStr);

            try
            {
                var lumiData = JsonConvert.DeserializeObject<LumiData>(jsonStr);
                var i = 1;

                if (lumiData == null) return;
                foreach (var cn in lumiData.cnSt)
                {
                    trend.Invoke((MethodInvoker) delegate {
                        trend.AddData($"cn{i}", cn.val, time);
                    });

                    time = DateTime.Now.ToString("MM/dd/yyyy ") + time;
                    datalogger.AddData($"cn{i}", cn.val, time);

                    i++;
                }
            }
            catch (Exception)
            {
                trend.Invoke((MethodInvoker)delegate {
                    trend.AddText(jsonStr);
                });
            }
        }

        private void LumiLogger_Load(object sender, EventArgs e)
        {
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trend.Clear();
            if(!esp8266.IsOpen) esp8266.Open();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            esp8266.Close();
            trend.MoveEnabled = true;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trend.Clear();
        }

        private void openDatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog()
            {
                Filter = "Dat Files | *.dat",
                Multiselect = true,
                InitialDirectory = "C:\\Dat"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                esp8266.Close();
                trend.OpenFiles(openFile.FileNames);
            }
        }
    }

    public class LumiData
    {
        public class CnSt
        {
            public int st { get; set; }
            public int val { get; set; }
        }

        public string ef { get; set; }
        public string time { get; set; }
        public int ntwSt { get; set; }
        public string mac { get; set; }
        public string ip { get; set; }
        public List<CnSt> cnSt { get; set; }
    }


}
