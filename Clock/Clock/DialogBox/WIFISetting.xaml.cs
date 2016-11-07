using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clock.Misc;
using ColckWindow;

namespace Clock.DialogBox
{
    /// <summary>
    /// WIFISetting.xaml 的交互逻辑
    /// </summary>
    public partial class WIFISetting : UserControl
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
            }
        }

        private void WindowOnCancelEvent(object sender, OkEventArgs e)
        {
            if (UserSettings.Wifi)
            {
                string str = "netsh wlan stop hostednetwork";
                CreateCmd.create(str);
                UserSettings.Wifi = false;
                MessageBox message = new MessageBox();
                message.SetPrompt("WIFI");
                message.SetMessage("热点已关闭");
                message.configure = _configure;
                message.Show();
            }
        }

        private void WindowOnOkEvent(object sender, OkEventArgs e)
        {
            if (! UserSettings.Wifi)
            {
                MessageBox message = new MessageBox();
                message.SetPrompt("WIFI");
                message.configure = _configure;  
                if (User.Text.Length<1)
                {
                    message.SetMessage("名称不能为空");
                    message.Show();
                    return;
                }
                if (Password.Text.Length<=7)
                {
                    message.SetMessage("密码长度不能小于8位数");
                    message.Show();
                    return;
                }
                string [] strings = new string[2];
                strings[0] = new string(User.Text.ToCharArray());
                strings[1] = new string(Password.Text.ToCharArray());
                Openwifi(strings);
                message.SetMessage("已创建热点");
                message.Show();
            }
        }

        private void Openwifi(string [] stringArray)
        {
            string str = "netsh wlan set hostednetwork mode=allow ssid=" + stringArray[0] + " key=" + stringArray[1];
            CreateCmd.create(str);
            // 命令行输入命令，用来自动连接wifi：netsh interface ip set address name="本地连接" source=dhcp 
            str = "netsh wlan start hostednetwork";
            CreateCmd.create(str);
            str = "netsh interface ip set address name=宽带连接 source=dhcp";
            CreateCmd.create(str);
            str = "netsh interface ip set address name=本地连接 source=dhcp";
            CreateCmd.create(str);
            UserSettings.Wifi = true;
        }

        public WIFISetting()
        {
            InitializeComponent();
        }
    }
}
