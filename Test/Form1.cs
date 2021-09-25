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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timerTrend_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            trend.AddData("Digital 1", DateTime.Now.Second % 2 == 0, time);
            trend.AddData("Digital 2", DateTime.Now.Second % 2 != 0, time);
            trend.AddData("Analog", DateTime.Now.Millisecond + (DateTime.Now.Second * 1000),time);

            time = DateTime.Now.ToString("MM/dd/yyyy ") + time;
            datalogger.AddData("Digital 1", DateTime.Now.Second % 2 == 0, time);
            datalogger.AddData("Digital 2", DateTime.Now.Second % 2 != 0, time);
            datalogger.AddData("Analog", DateTime.Now.Millisecond + (DateTime.Now.Second * 1000), time);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trend.Clear();
            timerTrend.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerTrend.Stop();
            trend.MoveEnabled = true;
        }

        private void openDatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog()
            {
                Filter = "Dat Files | *.dat",
                Multiselect = true,
                InitialDirectory = "C:\\Dat"
            };

            if(openFile.ShowDialog() == DialogResult.OK)
            {
                timerTrend.Stop();
                trend.OpenFiles(openFile.FileNames);                
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trend.Clear();
        }
    }
}
