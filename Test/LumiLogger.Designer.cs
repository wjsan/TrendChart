
namespace Test
{
    partial class LumiLogger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.esp8266 = new System.IO.Ports.SerialPort(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.trendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trend = new TrendChart.Trend();
            this.datalogger = new TrendChart.Datalogger(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // esp8266
            // 
            this.esp8266.BaudRate = 74400;
            this.esp8266.PortName = "COM10";
            this.esp8266.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.esp8266_DataReceived);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trendToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 4;
            this.menu.Text = "menuStrip1";
            // 
            // trendToolStripMenuItem
            // 
            this.trendToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.openDatToolStripMenuItem});
            this.trendToolStripMenuItem.Name = "trendToolStripMenuItem";
            this.trendToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.trendToolStripMenuItem.Text = "Trend";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // openDatToolStripMenuItem
            // 
            this.openDatToolStripMenuItem.Name = "openDatToolStripMenuItem";
            this.openDatToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openDatToolStripMenuItem.Text = "Open Dat";
            this.openDatToolStripMenuItem.Click += new System.EventHandler(this.openDatToolStripMenuItem_Click);
            // 
            // trend
            // 
            this.trend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trend.Location = new System.Drawing.Point(0, 24);
            this.trend.MoveEnabled = false;
            this.trend.Name = "trend";
            this.trend.Size = new System.Drawing.Size(800, 545);
            this.trend.TabIndex = 3;
            // 
            // datalogger
            // 
            this.datalogger.Directory = "C:\\dat";
            this.datalogger.MaxSize = 1000;
            // 
            // LumiLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 569);
            this.Controls.Add(this.trend);
            this.Controls.Add(this.menu);
            this.Name = "LumiLogger";
            this.Text = "LumiLogger";
            this.Load += new System.EventHandler(this.LumiLogger_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrendChart.Trend trend;
        private TrendChart.Datalogger datalogger;
        private System.IO.Ports.SerialPort esp8266;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem trendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDatToolStripMenuItem;
    }
}