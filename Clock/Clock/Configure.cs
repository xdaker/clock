using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ColckWindow
{
   public class Configure
    {
        /// <summary>
        /// 窗口颜色
        /// </summary>
        Color ViewColor { get; set; }
        /// <summary>
        /// 总在最前
        /// </summary>
        bool AllFirst { get; set; }
        /// <summary>
        /// 闹钟音量
        /// </summary>
        int AlarmVolume { get; set; }
        /// <summary>
        /// 提醒音量
        /// </summary>
        int RemindVolume { get; set; }
        /// <summary>
        /// 闹钟音乐
        /// </summary>
        string AlarmPath { get; set; }
        /// <summary>
        /// 提醒音乐
        /// </summary>
        string RemindPath { get; set; }
        public Configure()
        {
            //Path
        }
    }
}
