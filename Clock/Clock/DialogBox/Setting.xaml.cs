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
                slider.DataContext = _configure;
            }
        }

        public Setting()
        {
            InitializeComponent();
        }

        private void WindowOnCancelEvent(object sender, OkEventArgs okEventArgs)
        {
            
        }

        private void WindowOnOkEvent(object sender, OkEventArgs okEventArgs)
        {
            
        }
    }
}
