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

namespace Clock.DialogBox
{
    /// <summary>
    /// AlarmClockSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmClockSetting : UserControl
    {
        public event EventHandler<OkEventArgs> OkEvent;
        public event EventHandler<OkEventArgs> CancelEvent;
        public AlarmClockSetting()
        {
            InitializeComponent();
            ImageOk.MouseLeave += OkOnMouseLeaveHandle;
            ImageOk.MouseEnter += OkOnMouseEnterHandle;
            ImageOk.MouseLeftButtonDown += OkOnMouseLeftButtonDownHandle;
            ImageOk.MouseLeftButtonUp += (sender, args) =>
            {
                OkEvent?.Invoke(sender, new OkEventArgs());
            };
            ImageCancel.MouseLeave += CancelOnMouseLeaveHandle;
            ImageCancel.MouseEnter += CancelOnMouseEnterHandle;
            ImageCancel.MouseLeftButtonDown += CancelOnMouseLeftButtonDownHandle;
            ImageCancel.MouseLeftButtonUp += (sender, args) =>
            {
                CancelEvent?.Invoke(sender, new OkEventArgs());
            };
        }

        private void CancelOnMouseLeftButtonDownHandle(object sender, MouseButtonEventArgs e)
        {
            ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_disable.png",
                UriKind.RelativeOrAbsolute));
        }

        private void CancelOnMouseEnterHandle(object sender, MouseEventArgs e)
        {
            ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_hover.png",
                UriKind.RelativeOrAbsolute));
        }

        private void CancelOnMouseLeaveHandle(object sender, MouseEventArgs e)
        {
            ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_normal.png",
                UriKind.RelativeOrAbsolute));
        }

        private void OkOnMouseLeftButtonDownHandle(object sender, MouseButtonEventArgs e)
        {
            ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_disable.png", UriKind.RelativeOrAbsolute));
        }

        private void OkOnMouseEnterHandle(object sender, MouseEventArgs e)
        {
            ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_hover.png",
                UriKind.RelativeOrAbsolute));
        }

        private void OkOnMouseLeaveHandle(object sender, MouseEventArgs e)
        {
            ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_normal.png",
    UriKind.RelativeOrAbsolute));
        }
    }
}
