using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ColckWindow;

namespace Clock.DialogBox
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBox : Window
    {
        private Configure _configure { get; set; }
        public Configure configure {
            get { return _configure; }
            set
            {
                _configure = value;
                DataContext = value;
            }
        }
        private imageSate Sate = imageSate.Leave;
        //构造一个DispatcherTimer类实例
        DispatcherTimer dTimer = new System.Windows.Threading.DispatcherTimer();
        public MessageBox()
        {
            InitializeComponent();
            image.MouseEnter += ImageOnMouseEnter;
            image.MouseLeave += ImageOnMouseLeave;

            ViewGrid.MouseLeftButtonDown += ViewGridOnMouseLeftButtonDown;
            double WH = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double WW = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            this.Top = WH - this.Height;
            this.Left = WW - this.Width;
        }

        private void ViewGridOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (Sate == imageSate.Leave)
            {
                dTimer.Interval = new TimeSpan(0, 0, 60);
                this.DragMove();
            }
            else
            {
                dTimer.Stop();
                Close();
            }
            
        }

        private void ImageOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            Sate = imageSate.Leave;
            image.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/Close_normal.png" , UriKind.RelativeOrAbsolute));
        }

        private void ImageOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            Sate = imageSate.Enter;
            Voice voice = new Voice(_configure);
            voice.Play(VoiceType.Click);
            image.Source = new BitmapImage(new Uri(@"/Clock;component/Imge/Close_hover.png" , UriKind.RelativeOrAbsolute));
        }

        public void SetPrompt(string prompt)
        {
            Prompt.Text = prompt;
        }

        public void SetMessage(string message)
        {
            Message.Text = message;
        }
        
        public void SetCloseTime(int time)
        {
            //设置事件处理函数
            dTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, time);
            dTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        enum imageSate
        {
            Enter,
            Leave,
        }
    }
}
