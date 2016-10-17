using NetworkLibrary;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColckWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColckWindowView : Window
    {
        public Colck colck { get; set; }
        public Network network { get; set; }
        private ControllerColck Controller;
        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }
        private Voice _voice;
        public RectangleState rectangleState { get; set; }
        public Configure Config { get; set; }
        public ColckWindowView()
        {
            Config = new Configure();
            Config.Start();

            InitializeComponent();
            colck = new Colck();

            network = new Network();
          
            Controller = new ControllerColck(this);

            WindowHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            WindowWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            SetWindowPosition(50, WindowWidth - Width);

            rectangleState = RectangleState.Default;
            DragRect.DataContext = Config;
            FunctionRcet.DataContext = Config;
        }

        /// <summary>
        /// 声明一个事件
        /// </summary>
        private delegate void UpdateText(string massage);
        private delegate void SetPosition(bool b);
        /// <summary>
        /// 设置总在最前
        /// </summary>
        /// <param name="b"></param>
        private delegate void Allows(bool b);
        public void UpdateTimeText(string massage)
        {
            if (TimeText.Dispatcher.CheckAccess())
            {
                TimeText.Text = massage;
            }
            else
            {
                UpdateText _timeText = UpdateTimeText;
                TimeText.Dispatcher.Invoke(_timeText, massage);
            }
        }
        public void UpdateSecondText(string massage)
        {
            if (SecondText.Dispatcher.CheckAccess())
            {
                SecondText.Text = massage;
            }
            else
            {
                UpdateText _secondText = UpdateSecondText;
                SecondText.Dispatcher.Invoke(_secondText, massage);
            }
        }
        public void UpdateDownloadText(string massage)
        {
            if (DownloadText.Dispatcher.CheckAccess())
            {
                DownloadText.Text = massage;
            }
            else
            {
                UpdateText _downloadText = UpdateDownloadText;
                DownloadText.Dispatcher.Invoke(_downloadText, massage);
            }
        }
        public void UpdateUploadText(string massage)
        {
            if (UploadText.Dispatcher.CheckAccess())
            {
                UploadText.Text = massage;
            }
            else
            {
                UpdateText _uploadText = UpdateUploadText;
                UploadText.Dispatcher.Invoke(_uploadText, massage);
            }
        }
        private void DragRcet_Dowm(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                rectangleState = RectangleState.Drag;
                DragMove();
            }
        }


        private void DragRect_Enter(object sender, MouseEventArgs e)
        {
            if (rectangleState != RectangleState.DragEnd)
            {
                rectangleState = RectangleState.Focus;
            }
            _voice = new Voice(VoiceType.Click);
            _voice.Play();
        }
        public void ViewClose()
        {
            Close();
        }
        public void SetWindowPosition(double top , double left)
        {
            Top = top;
            Left = left;
        }

        public void SetTopmost(bool b)
        {
            if (Dispatcher.CheckAccess())
            {
                Topmost = b;
            }
            else
            {
                Allows all = SetTopmost;
                Dispatcher.Invoke(all, b);
            }
            
        }

        private void DragRect_MouseLeave(object sender, MouseEventArgs e)
        {
            if (rectangleState == RectangleState.Drag)
            {
                rectangleState = RectangleState.DragEnd;
            }
            else
                rectangleState = RectangleState.Default;
        }
        /// <summary>
        /// 改变时钟视图的位置
        /// </summary>
        /// <param name="b">true在左边，false在右边</param>
        public void PositionTransformation(bool b)
        {
            if (ViewGrid.Dispatcher.CheckAccess())
            {
                if (b )//左边
                {
                    //ViewGrid.Margin = new Thickness(0,6, 7.167, 5);
                    ViewGrid.Margin = new Thickness(0, 6, 7.167, 5);
                }
                else
                {//右边
                    ViewGrid.Margin = new Thickness(0, 6,180, 5);
                }
            }
            else
            {
                SetPosition position = PositionTransformation;
                ViewGrid.Dispatcher.Invoke(position, b);
            }

        }
    }
    public enum RectangleState
    {
        Default = 0,
        Drag,
        DragEnd,
        Focus,
    }
}
