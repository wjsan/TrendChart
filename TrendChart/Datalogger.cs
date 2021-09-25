using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TrendChart
{
    public partial class Datalogger : Component
    {
        public string Directory { get; set; } = "C:\\Dat";

        public int MaxSize { get; set; } = 1000;

        private DatFile file = new DatFile();

        private int fileCount = 0;

        public Datalogger()
        {
            InitializeComponent();
        }

        public Datalogger(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void AddData(string name, dynamic value, string time)
        {
            if(file.AddData(name, value, time) >= MaxSize)
            {
                Save();
            }
        }

        public void Save()
        {
            file.FileName = Directory + "\\DAT" + fileCount + ".dat";
            fileCount++;
            file.Save();
            file = new DatFile();
        }
    }

    public class Dat
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string Time { get; set; }

        public Dat(string name, dynamic value, string time)
        {
            Name = name;
            Value = value;
            Time = time;
        }

    }

    public class DatFile
    {
        List<Dat> data;

        public string FileName { get; set; }

        private int size;

        public DatFile()
        {
            data = new List<Dat>();
            size = 0;
        }

        public int AddData(string name, dynamic value, string time)
        {
            data.Add(new Dat(name, value, time));
            return size++;
        }

        public void Save()
        {
            var jsonStr = JsonConvert.SerializeObject(data);
            File.WriteAllText(FileName, jsonStr);
        }

        public void Load()
        {
            string jsonStr = File.ReadAllText(FileName);
            data = JsonConvert.DeserializeObject<List<Dat>>(jsonStr);
        }
    }
}
