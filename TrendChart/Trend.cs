using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TrendChart
{
    public partial class Trend : UserControl
    {
        public TrendChartSettings Settings { get; set; } = new TrendChartSettings();

        private Dictionary<string, Series> digSeries = new Dictionary<string, Series>();
        private Dictionary<string, Series> analogSeries = new Dictionary<string, Series>();
        private List<Series> digSeriesList = new List<Series>();
        private int cont = 0;
        private int startPosX = 0;

        public bool MoveEnabled { get; set; }

        public Trend()
        {
            InitializeComponent();
            chart.MouseWheel += Chart_MouseWheel;
            chart.MouseMove += Chart_MouseMove;
            chart.MouseDown += Chart_MouseDown;
        }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                foreach (var chartArea in chart.ChartAreas)
                {
                    //chartArea.AxisX.Minimum -= e.Delta;
                    chartArea.AxisX.Maximum += e.Delta;
                    Settings.Axis.X.Size = chartArea.AxisX.Maximum - chartArea.AxisX.Minimum;
                }
            }
            else
            {
                if (!MoveEnabled) return;
                foreach (var chartArea in chart.ChartAreas)
                {
                    chartArea.AxisX.Minimum += e.Delta;
                    chartArea.AxisX.Maximum += e.Delta;
                }
            }
        }

        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (!MoveEnabled) return;
            startPosX = e.X;
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (!MoveEnabled) return;
            if (MouseButtons == MouseButtons.Left)
            {
                int moveX = startPosX - e.X;
                foreach (var chartArea in chart.ChartAreas)
                {
                    chartArea.AxisX.Minimum += moveX;
                    chartArea.AxisX.Maximum += moveX;
                }
                startPosX = e.X;
            }
        }

        public void OpenFiles(string[] files)
        {
            Clear();

            foreach (var file in files)
            {
                OpenFile(file);
            }
            MoveEnabled = true;
        }

        public void OpenFile(string file)
        {
            var jsonStr = File.ReadAllText(file);
            var data = JsonConvert.DeserializeObject<List<Dat>>(jsonStr);

            foreach (Dat dat in data)
            {
                var trendIdx = AddData(dat.Name, dat.Value, dat.Time, false);
            }
        }

        public int AddData(string name, object value, string time, bool removeInvisiblePoints = true)
        {
            var trendIdx = 0;
            var series = GetSeries(name, value);
            if (value == null) return 0;
            if (value is bool)
            {
                trendIdx = AddBooleanData(name, (bool)value, time);
            }
            else
            {
                trendIdx = series.Points.AddXY(time, value);
            }
            if (trendIdx > Settings.Axis.X.Size && removeInvisiblePoints)
            {
                series.Points.RemoveAt(0);
            }
            MoveEnabled = false;
            return trendIdx;
        }

        public void Clear()
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            digSeries.Clear();
            analogSeries.Clear();
        }

        private int AddBooleanData(string name, bool value, string time)
        {
            var series = GetSeries(name, value);
            var idx = digSeriesList.IndexOf(series) + 1;
            var offSet = Settings.Axis.Y.DigitalSignals.SizeOffSet;
            cont = series.Points.AddXY(time, idx - 1, idx - offSet);
            series.Points[cont].Color = series.Color;
            series.Points[cont].Color = value ? series.Color : Color.Transparent;
            return cont;
        }

        private Series GetSeries(string name, object value)
        {
            if (digSeries.ContainsKey(name))
            {
                return digSeries[name];
            }
            if (analogSeries.ContainsKey(name))
            {
                return analogSeries[name];
            }
            var newSeries = chart.Series.Add(name);
            ConfigureSeries(ref newSeries, value);
            return newSeries;
        }

        private ChartArea GetDigitalChartArea()
        {
            var chartArea = chart.ChartAreas.FindByName("DigitalChartArea");
            if (chartArea == null)
            {
                var newChartArea = new ChartArea("DigitalChartArea");
                newChartArea.Name = "DigitalChartArea";
                newChartArea.Position = new ElementPosition(0, 0, 100, 100);
                newChartArea.AxisY.Enabled = AxisEnabled.False;
                newChartArea.AxisX.Maximum = Settings.Axis.X.Size;
                newChartArea.AxisY.Maximum = Settings.Axis.Y.DigitalSignals.Scale;
                chart.ChartAreas.Add(newChartArea);
                return newChartArea;
            }
            return chartArea;
        }

        private void ConfigureSeries(ref Series series, object value)
        {
            if (value is bool)
            {
                digSeries.Add(series.Name, series);
                digSeriesList.Add(series);
                series.ChartType = SeriesChartType.Range;
                series.ChartArea = GetDigitalChartArea().Name;
            }
            else
            {
                var newChart = new ChartArea(series.Name);
                ConfigureNewChart(ref newChart);

                analogSeries.Add(series.Name, series);
                series.ChartType = SeriesChartType.Line;
                series.ChartArea = newChart.Name;
            }
        }

        private void ConfigureNewChart(ref ChartArea chartArea)
        {
            chartArea.AlignmentOrientation = AreaAlignmentOrientations.All;
            chartArea.AxisX.IsMarginVisible = false;
            chartArea.AxisY.Enabled = AxisEnabled.False;
            chartArea.AxisX.Enabled = AxisEnabled.False;
            chartArea.AxisX.Maximum = Settings.Axis.X.Size;
            chartArea.AlignWithChartArea = GetDigitalChartArea().Name;
            chartArea.BackColor = Color.Transparent;

            chart.ChartAreas.Add(chartArea);
        }
    }

    [DesignTimeVisible(false)]
    public class TrendChartSettings : Component
    {
        public TrendChartAxisSettings Axis { get; set; } = new TrendChartAxisSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisSettings : Component
    {
        public TrendChartAxisXSettings X { get; set; } = new TrendChartAxisXSettings();
        public TrendChartAxisYSettings Y { get; set; } = new TrendChartAxisYSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisXSettings : Component
    {
        public double Size { get; set; } = 1000;
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisYSettings : Component
    {
        public TrendChartAxisYDigitalSettings DigitalSignals { get; set; } = new TrendChartAxisYDigitalSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisYDigitalSettings : Component
    {
        public int Scale { get; set; } = 25;
        public float SizeOffSet { get; set; } = 0.3F;
    }
}
