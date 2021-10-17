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
            Init();
        }

        public Datalogger(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var dir = new DirectoryInfo(Directory);
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                if (file.Extension == ".dat")
                {
                    fileCount++;
                }
            }
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
        public dynamic Value { get; set; }
        public string Time { get; set; }

        public Dat(dynamic value, string time)
        {
            Value = value;
            Time = time;
        }

    }

    public class DatFile
    {
        Dictionary<string, List<Dat>> data;
        
        public string FileName { get; set; }

        private int size;

        public DatFile()
        {
            data = new Dictionary<string, List<Dat>>();
            size = 0;
        }

        public int AddData(string name, dynamic value, string time)
        {
            if (data.ContainsKey(name))
            {
                data[name].Add(new Dat(value, time));
            }
            else
            {
                data[name] = new List<Dat>();
                data[name].Add(new Dat(value, time));
            }
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
            data = JsonConvert.DeserializeObject<Dictionary<string, List<Dat>>>(jsonStr);
        }
    }
}
