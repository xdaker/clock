using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ColckWindow
{
    [Serializable]
    [XmlType("Configure")]
   public class Configure : INotifyPropertyChanged
    {  
        /// <summary>
        /// 窗口颜色
        /// </summary>
        [XmlElement("ViewColor")]
        public Color ViewColor {
            get { return _viewColor; }
            set
            {
                _viewColor = value;
                OnPropertyChanged("ViewColor");
                OnPropertyChanged("FrontViewColorBrush");
            }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [XmlElement("FontColor")]
        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                OnPropertyChanged("FontColor");
                OnPropertyChanged("FontColorBrush");
            }
        }

        /// <summary>
        /// 总在最前
        /// </summary>
        [XmlElement("AllFirst")]
        public bool AllFirst
        {
            get { return _allFirst; }
            set
            {
                _allFirst = value;
                OnPropertyChanged("AllFirst");
            }
        }
        /// <summary>
        /// 闹钟音量
        /// </summary>
        [XmlElement("AlarmVolume")]
        public int AlarmVolume
        {
            get { return _alarmVolume; }
            set
            {
                _alarmVolume = value;
                OnPropertyChanged("AllFirst");
            }
        }
        /// <summary>
        /// 提醒音量
        /// </summary>
        [XmlElement("RemindVolume")]
        public int RemindVolume {
            get { return _remindVolume; }
            set
            {
                _remindVolume = value;
                OnPropertyChanged("AllFirst");
            }
        }
        /// <summary>
        /// 闹钟音乐
        /// </summary>
        [XmlElement("AlarmPath")]
        public string AlarmPath {
            get { return _alarmPath; }
            set
            {
                _alarmPath = value;
                OnPropertyChanged("AllFirst");
            }
        }
        /// <summary>
        /// 提醒音乐
        /// </summary>
        [XmlElement("RemindPath")]
        public string RemindPath
        {
            get { return _remindPath; }
            set
            {
                _remindPath = value;
                OnPropertyChanged("RemindPath");
            }
        }

        [XmlIgnore]
        public Brush FrontViewColorBrush =>new SolidColorBrush(ViewColor);

        [XmlIgnore]
        public Color BottomViewColor => Color.FromArgb(0, _viewColor.R, _viewColor.G, _viewColor.B);

        [XmlIgnore]
        public Brush FontColorBrush => new SolidColorBrush(FontColor);
        [XmlIgnore]
        private Color _viewColor { get; set; }
        [XmlIgnore]
        private Color _fontColor { get; set; }
        [XmlIgnore]
        private bool _allFirst { get; set; }
        [XmlIgnore]
        private int _alarmVolume { get; set; }
        [XmlIgnore]
        private int _remindVolume { get; set; }
        [XmlIgnore]
        private string _alarmPath { get; set; }
        [XmlIgnore]
        private string _remindPath { get; set; }
        public Configure()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Default()
        {
            ViewColor = Color.FromArgb(200,230,230,230);
            FontColor = Color.FromArgb(255,0,0,0);
            AllFirst = true;
            AlarmVolume = 100;
            RemindVolume = 100;
            AlarmPath = @"package/Notify.SAO.Present.wav";
            RemindPath = @"package/Notify.SAO.Message.wav";
        }
        private void Colne(Configure config)
        {
            ViewColor = config.ViewColor;
            FontColor = config.FontColor;
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
                try
                {
                    var config = XmlSerializer.XmlSerializer.LoadFromXml<Configure>(@"Configure.xml");
                    Colne(config);
                }
                catch
                {
                    Default();
                    XmlSerializer.XmlSerializer.SaveToXml(@"Configure.xml", this, GetType(), "Configure");
                }
            }
            else
            {
                Default();
                XmlSerializer.XmlSerializer.SaveToXml(@"Configure.xml", this, GetType(), "Configure");
            }
        }
    }
}
