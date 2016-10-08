using System;
using System.Collections.Generic;
using System.Timers;

namespace NetworkLibrary
{
    class Controller
    {
        //计时器
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        System.Timers.Timer timer1 = new System.Timers.Timer(1);
        Colck _colck;
        Model _model;
        List<TimeChange> timeChange { get; set; }
        public Controller(Colck colck , Model model)
        {
            _colck = colck;
            _model = model;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer1.Elapsed += timer1_Elapsed;
            timer1.Enabled = true;
            Assignment();
            timeChange = new List<TimeChange>();
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            _colck.Millisecond = _model.Millisecond;
            MillisecondChange?.Invoke(this,new TimeUpdateEventArgs());
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeState();
            Assignment();
            Event();
        }
        private void Event()
        {
            try
            {
                foreach (var list in timeChange)
                {
                    switch (list)
                    {
                        case TimeChange.Second:
                            SecondChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        case TimeChange.Minute:
                            MinuteChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        case TimeChange.Hour:
                            HourChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        case TimeChange.Day:
                            DayChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        case TimeChange.Month:
                            MonthChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        case TimeChange.Year:
                            YearChange?.Invoke(this, new TimeUpdateEventArgs());
                            break;
                        default: break;
                    }
                }
            }
            catch
            {

            }
        }
        private void Assignment()
        {
            _colck.Hour = _model.Hour;
            _colck.Second = _model.Second;
            _colck.Year = _model.Year;
            _colck.Minute = _model.Minute;
            _colck.Day = _model.Day;
            _colck.Month = _model.Month; 
        }
        private void TimeState()
        {
            timeChange.Clear();
            if (_colck.Second != _model.Second)
            {
                timeChange.Add(TimeChange.Second);
            }
            if (_colck.Minute != _model.Minute)
            {
                timeChange.Add(TimeChange.Minute);
            }
            if (_colck.Hour != _model.Hour)
            {
                timeChange.Add(TimeChange.Hour);
            }
            if (_colck.Day != _model.Day)
            {
                timeChange.Add(TimeChange.Day);
            }
            if (_colck.Month != _model.Month)
            {
                timeChange.Add(TimeChange.Month);
            }
            if (_colck.Year != _model.Year)
            {
                timeChange.Add(TimeChange.Year);
            }
        }
        public event EventHandler<TimeUpdateEventArgs> SecondChange;
        public event EventHandler<TimeUpdateEventArgs> HourChange;
        public event EventHandler<TimeUpdateEventArgs> YearChange;
        public event EventHandler<TimeUpdateEventArgs> MinuteChange;
        public event EventHandler<TimeUpdateEventArgs> DayChange;
        public event EventHandler<TimeUpdateEventArgs> MonthChange;
        public event EventHandler<TimeUpdateEventArgs> MillisecondChange;
        enum TimeChange
        {
            Second = 0,
            Minute,
            Hour,
            Day,
            Month,
            Year,
            
        }
    }

}
