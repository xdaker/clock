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
                this.DragMove();
            }
            else
            {
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

        enum imageSate
        {
            Enter,
            Leave,
        }
    }
}
