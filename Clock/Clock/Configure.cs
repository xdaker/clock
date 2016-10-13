using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ColckWindow
{
   [XmlType("Configure")]
   public class Configure
    {
        
        /// <summary>
        /// 窗口颜色
        /// </summary>
        [XmlElement("ViewColor")]
        public Color ViewColor { get; set; }

        /// <summary>
        /// 总在最前
        /// </summary>
        [XmlElement("AllFirst")]
        public bool AllFirst { get; set; }
        /// <summary>
        /// 闹钟音量
        /// </summary>
        [XmlElement("AlarmVolume")]
        public int AlarmVolume { get; set; }
        /// <summary>
        /// 提醒音量
        /// </summary>
        [XmlElement("RemindVolume")]
        public int RemindVolume { get; set; }
        /// <summary>
        /// 闹钟音乐
        /// </summary>
        [XmlElement("AlarmPath")]
        public string AlarmPath { get; set; }
        /// <summary>
        /// 提醒音乐
        /// </summary>
        [XmlElement("RemindPath")]
        public string RemindPath { get; set; }
         
        public Configure()
        {
        }
        public void Default()
        {
            ViewColor = Color.FromArgb(200,230,230,230);
            AllFirst = true;
            AlarmVolume = 100;
            RemindVolume = 100;
            AlarmPath = @"package/Notify.SAO.Present.wav";
            RemindPath = @"package/Notify.SAO.Message.wav";
        }
        private void Colne(Configure config)
        {
            ViewColor = config.ViewColor;
            AllFirst = config.AllFirst;
            AlarmVolume = config.AlarmVolume;
            RemindVolume = config.RemindVolume;
            AlarmPath = config.AlarmPath;
            RemindPath = config.RemindPath;
        }
        public void Start()
        {
            if (File.Exists(@"Config.xml"))
            {
                var config = XmlSerializer.XmlSerializer.LoadFromXml<Configure>(@"Config.xml");
                Colne(config);
            }
            else
            {
                Default();
                XmlSerializer.XmlSerializer.SaveToXml(@"Config.xml", this, GetType(), "Config");
            }
        }
    }
}
