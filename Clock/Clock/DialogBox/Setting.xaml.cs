using System.Windows;
using System.Windows.Controls;
using ColckWindow;

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
            ofd.Filter = "WAV文件|*.wav";
            if (ofd.ShowDialog() == true)
            {
                AlarmPathText.Text = ofd.FileName;
            }
        }

        private void RemindPathButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".wav";
            ofd.Filter = "WAV文件|*.wav";
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
            SelfStarting.SetSelfStarting(_configure.SelfStarting, "Clock.exe");
        }
    }
}
