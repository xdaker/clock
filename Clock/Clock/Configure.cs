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
        [XmlIgnore]
        private byte _miniTransparency =>25;
        [XmlIgnore]
        private byte _viewColorR;
        [XmlIgnore]
        private byte _viewColorG;
        [XmlIgnore]
        private byte _viewColorB;
        [XmlIgnore]
        private byte _viewColorA;

        [XmlIgnore]
        private byte _fontColorR;
        [XmlIgnore]
        private byte _fontColorG;
        [XmlIgnore]
        private byte _fontColorB;
        [XmlIgnore]
        private byte _fontColorA;

        [XmlIgnore]
        public byte ViewColorR
        {
            get { return _viewColorR; }
            set
            {
                _viewColorR = value;
                ViewColor = Color.FromArgb(ViewColor.A , _viewColorR , ViewColor.G , ViewColor.B);
                OnPropertyChanged("ViewColorR");
            }
        }
        [XmlIgnore]
        public byte ViewColorG
        {
            get { return _viewColorG; }
            set
            {
                _viewColorG = value;
                ViewColor = Color.FromArgb(ViewColor.A, ViewColor.R, _viewColorG, ViewColor.B);
                OnPropertyChanged("ViewColorG");
            }
        }
        [XmlIgnore]
        public byte ViewColorB
        {
            get { return _viewColorB; }
            set
            {
                _viewColorB = value;
                ViewColor = Color.FromArgb(ViewColor.A, ViewColor.R, ViewColor.G, _viewColorB);
                OnPropertyChanged("ViewColorB");
            }
        }
        [XmlIgnore]
        public byte ViewColorA
        {
            get { return _viewColorA; }
            set
            {
                _viewColorA = value;
                if (value < 25)
                {
                    _viewColorA = _miniTransparency;
                }
                ViewColor = Color.FromArgb(_viewColorA, ViewColor.R, ViewColor.G, ViewColor.B);
                OnPropertyChanged("ViewColorA");
            }
        }

        [XmlIgnore]
        public byte FontColorR
        {
            get { return _fontColorR; }
            set
            {
                _fontColorR = value;
                FontColor = Color.FromArgb(FontColor.A, _fontColorR, FontColor.G, FontColor.B);
                OnPropertyChanged("FontColorR");
            }
        }
        [XmlIgnore]
        public byte FontColorG
        {
            get { return _fontColorG; }
            set
            {
                _fontColorG = value;
                FontColor = Color.FromArgb(FontColor.A, FontColor.R, _fontColorG, FontColor.B);
                OnPropertyChanged("FontColorG");
            }
        }
        [XmlIgnore]
        public byte FontColorB
        {
            get { return _fontColorB; }
            set
            {
                _fontColorB = value;
                FontColor = Color.FromArgb(FontColor.A, FontColor.R, FontColor.G, _fontColorB);
                OnPropertyChanged("FontColorB");
            }
        }
        [XmlIgnore]
        public byte FontColorA
        {
            get { return _fontColorA; }
            set
            {
                _fontColorA = value;
                if (value < 25)
                {
                    _fontColorA = _miniTransparency;
                }
                FontColor = Color.FromArgb(_fontColorA, FontColor.R, FontColor.G, FontColor.B);
                OnPropertyChanged("FontColorA");
            }
        }

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

        [XmlElement("SelfStarting")]
        public bool SelfStarting
        {
            get { return _selfStarting; }
            set
            {
                _selfStarting = value;
                OnPropertyChanged("SelfStarting");
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

        private bool _selfStarting { get; set; }

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
            SelfStarting = true;
            AlarmVolume = 100;
            RemindVolume = 100;
            AlarmPath = @"package/Notify.SAO.Present.wav";
            RemindPath = @"package/Notify.SAO.Message.wav";
            _viewColorR = 230;
            _viewColorA = 200;
            _viewColorB = 230;
            _viewColorG = 230;
            _fontColorA = 255;
            _fontColorB = 0;
            _fontColorG = 0;
            _fontColorR = 0;
        }
        private void Colne(Configure config)
        {
            ViewColor = config.ViewColor;
            FontColor = config.FontColor;
            AllFirst = config.AllFirst;
            SelfStarting = config.SelfStarting;
            AlarmVolume = config.AlarmVolume;
            RemindVolume = config.RemindVolume;
            AlarmPath = config.AlarmPath;
            RemindPath = config.RemindPath;
            ViewColorR = ViewColor.R;
            ViewColorA = ViewColor.A;
            ViewColorB = ViewColor.B;
            ViewColorG = ViewColor.G;

            FontColorA = FontColor.A;
            FontColorB = FontColor.B;
            FontColorG = FontColor.G;
            FontColorR = FontColor.R;
        }

        public void Read()
        {
            if (File.Exists(@"Configure.xml"))
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

        public void Write()
        {
            XmlSerializer.XmlSerializer.SaveToXml(@"Configure.xml", this, GetType(), "Configure");
        }
    }
}
