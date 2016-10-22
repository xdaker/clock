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
using System.Windows.Shapes;
using ColckWindow;

namespace Clock.DialogBox
{
    /// <summary>
    /// DialongWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialongWindow : Window
    {
        public Configure _Configure { get; set; }
        public event EventHandler<OkEventArgs> OkEvent;
        public event EventHandler<OkEventArgs> CancelEvent;
        private ImageState okImageState = ImageState.Default;
        private ImageState cancelImageState = ImageState.Default;
        public DialongWindow()
        {
            InitializeComponent();
            ImageOk.MouseLeave += OkOnMouseLeaveHandle;
            ImageOk.MouseEnter += OkOnMouseEnterHandle;

            ImageCancel.MouseLeave += CancelOnMouseLeaveHandle;
            ImageCancel.MouseEnter += CancelOnMouseEnterHandle;

            double WH = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double WW = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            this.Top = (WH - this.Height) / 3;
            this.Left = (WW - this.Width) / 2;
        }

        public void Show(WindowsType type)
        {
            foreach (var chi in GridWindow.Children)
            {
                if (chi is UserControl)
                {
                    GridWindow.Children.Remove(chi as UserControl);
                }
            }
            UserControl userControl =null;
            switch (type)
            {
                case WindowsType.Setting:
                    userControl = new Setting();
                    (userControl as Setting)._configure = _Configure;
                    (userControl as Setting).Window = this;
                    break;
            }
            if (userControl != null)
            {
                GridWindow.Children.Add(userControl);
                Height = userControl.Height + ImageOk.Height;
                GridWindow.Height = userControl.Height + ImageOk.Height;
                GridWindow.DataContext = _Configure;
                Show();
            }
        }

        private void GridWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed )
            {
                if (okImageState == cancelImageState && okImageState == ImageState.Default)
                {
                    this.DragMove();
                }
                else
                {
                    if (okImageState == ImageState.Focus)
                    {
                        ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_disable.png", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_disable.png",
                 UriKind.RelativeOrAbsolute));
                    }
                }

            }
            
        }

        private void CancelOnMouseEnterHandle(object sender, MouseEventArgs e)
        {
            ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_hover.png",
                UriKind.RelativeOrAbsolute));
            cancelImageState = ImageState.Focus;
        }

        private void CancelOnMouseLeaveHandle(object sender, MouseEventArgs e)
        {
            ImageCancel.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/cancel_normal.png",
                UriKind.RelativeOrAbsolute));
            cancelImageState = ImageState.Default;
        }

        private void OkOnMouseEnterHandle(object sender, MouseEventArgs e)
        {
            ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_hover.png",
                UriKind.RelativeOrAbsolute));
            okImageState = ImageState.Focus;
        }

        private void OkOnMouseLeaveHandle(object sender, MouseEventArgs e)
        {
            ImageOk.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/ok_normal.png",
    UriKind.RelativeOrAbsolute));
            okImageState = ImageState.Default;
        }

        enum ImageState
        {
            Default = 0,
            Focus,
        }

        private void GridWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (okImageState == ImageState.Focus)
            {
                OkEvent?.Invoke(sender, new OkEventArgs());
                Close();
            }
            else if (cancelImageState == ImageState.Focus)
            {
                CancelEvent?.Invoke(sender, new OkEventArgs());
                Close();
            }

        }
    }
    public class OkEventArgs : EventArgs
    {

    }

    public enum WindowsType
    {
        Setting = 0,
        Alarm,
        Remind,
        Shutdown,
        Wifi,
    }
}
