using System;

namespace NetworkLibrary
{
    public class Colck
    {
        Model Time = new Model();
        Controller Control { get; set; }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 分
        /// </summary>
        public int Minute { get; set; }
        /// <summary>
        /// 时
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// 天
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        public int Millisecond { get; set; }
        /// <summary>
        /// 秒针改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> SecondChange;
        /// <summary>
        /// 时针改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> HourChange;
        /// <summary>
        /// 年份改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> YearChange;
        /// <summary>
        /// 分针改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> MinuteChange;
        /// <summary>
        /// 天数改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> DayChange;
        /// <summary>
        /// 月份改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> MonthChange;
        /// <summary>
        /// 每毫秒改变时发生
        /// </summary>
        public event EventHandler<TimeUpdateEventArgs> MillisecondChange;
        /// <summary>
        /// 时钟
        /// </summary>
        public Colck()
        {
            Control = new Controller(this,Time);
            Event();
        }
        private void Event()
        {
            Control.SecondChange += (ss, ee) =>
            {
                SecondChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.DayChange += (ss, ee) =>
            {
                DayChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.HourChange += (ss, ee) =>
            {
                HourChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.MinuteChange += (ss, ee) =>
            {
                MinuteChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.MonthChange += (ss, ee) =>
            {
                MonthChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.YearChange += (ss, ee) =>
            {
                YearChange?.Invoke(this, new TimeUpdateEventArgs());
            };
            Control.MillisecondChange += (ss, ee) =>
            {
                MillisecondChange?.Invoke(ss, ee);
            };
        }
    }
    public class TimeUpdateEventArgs : EventArgs
    {

    }
}
