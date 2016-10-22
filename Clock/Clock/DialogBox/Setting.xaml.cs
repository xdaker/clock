using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColckWindow;
using Microsoft.Win32;

namespace Clock.DialogBox
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : UserControl
    {
        public Configure _configure { get; set; }
        private DialongWindow _window;

        public DialongWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                Window.OkEvent += WindowOnOkEvent;
                Window.CancelEvent += WindowOnCancelEvent;
                RemindPathButton.Click += RemindPathButtonOnClick;
                AlarmPathButton.Click += AlarmPathButtonOnClick;
                AllFirstCheckBox.Click += AllFirstCheckBoxOnClick;
            }
        }

        private void AllFirstCheckBoxOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AllFirstCheckBox.IsChecked != null)
            {
                _configure.AllFirst = AllFirstCheckBox.IsChecked.Value;
            }
        }

        private void AlarmPathButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".wav";
            ofd.Filter = "WAV文件|*.wav|MP3文件|*.mp3";
            if (ofd.ShowDialog() == true)
            {
                AlarmPathText.Text = ofd.FileName;
            }
        }

        private void RemindPathButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".wav";
            ofd.Filter = "WAV文件|*.wav|MP3文件|*.mp3";
            if (ofd.ShowDialog() == true)
            {
                RemindPathText .Text= ofd.FileName; 
            }
        }

        public Setting()
        {
            InitializeComponent();
        }

        private void WindowOnCancelEvent(object sender, OkEventArgs okEventArgs)
        {
            _configure.Read();
        }

        private void WindowOnOkEvent(object sender, OkEventArgs okEventArgs)
        {
            _configure.Write();
        }
    }
}
