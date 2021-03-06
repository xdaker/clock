﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace ColckWindow
{
    class MyMenu : ContextMenu
    {
        public Dictionary<string, MenuItem> menuItem;
        private string path;
        private Voice _voice { get; set;}
 
        public MyMenu(Configure configure)
        {
            menuItem = new Dictionary<string, MenuItem>();
            path = System.Environment.CurrentDirectory;
            Installed();
            Opacity = 0.9;
            var binding = new Binding("FrontViewColorBrush")
            {
                Source = configure,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            BindingOperations.SetBinding(
                this // 需绑定的控件  
                ,
                BackgroundProperty // 需绑定的控件属性  
                ,
                binding);
        }

        private void Installed()
        {
            var item = NewMenuItem("设置闹钟", new Uri(path + @"\package\clock1_normal.ico", UriKind.RelativeOrAbsolute));
            item.Background = Background;
            menuItem.Add("设置闹钟", item);
            Items.Add(item);
            AddVoice(item);

            var item4 = NewMenuItem("提醒我", new Uri(path + @"\package\modify_normal.ico", UriKind.RelativeOrAbsolute));
            menuItem.Add("提醒我", item4);
            Items.Add(item4);
            AddVoice(item4);

            var item1 = NewMenuItem("关机计时", new Uri(path + @"\package\expand3_normal.ico", UriKind.RelativeOrAbsolute));
            menuItem.Add("设置关机", item1);
            Items.Add(item1);
            AddVoice(item1);

            var item2 = NewMenuItem("WIFI", new Uri(path + @"\package\rss_normal.ico", UriKind.RelativeOrAbsolute));
            menuItem.Add("WIFI", item2);
            Items.Add(item2);
            AddVoice(item2);

            var item5 = NewMenuItem("设置", new Uri(path + @"\package\option_normal.ico", UriKind.RelativeOrAbsolute));
            menuItem.Add("设置", item5);
            Items.Add(item5);
            AddVoice(item5);

            var item3 = NewMenuItem("退出", new Uri(path + @"\package\logout_normal.ico", UriKind.RelativeOrAbsolute));
            menuItem.Add("退出", item3);
            Items.Add(item3);
            AddVoice(item3);

        }
        private MenuItem NewMenuItem(string name, Uri path)
        {
            return new MenuItem() {
                Header = name,
                Icon = new Image()
                {
                    Source =new BitmapImage(path),
                },
            };
        }
        private void AddVoice(MenuItem item)
        {
            item.MouseEnter += (ee, ss) =>
            {
                var config = new Configure();
                config.Read();
                _voice = new Voice(config);
                _voice.Play(VoiceType.Click);
            };
        }
    }
}
