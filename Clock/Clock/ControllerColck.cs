using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkLibrary;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Clock.DialogBox;

namespace ColckWindow
{
    class ControllerColck
    {
        private ColckWindowView View { get; set; }
        private Network network => View.network;
        private Colck colck => View.colck;
        private Grid viewGrid => View.ViewGrid;
        private MyMenu menu { get; set; }
        private Voice _voice { get; set; }
        public Configure Configure => View.Config;
        private delegate void CalculationPosition();
        private delegate void DragEndEvent();
        private ViewPosition _thisViewPosition { get; set; }
        public ControllerColck(ColckWindowView View)
        {
            this.View = View;
            colck.SecondChange += SecondChangeHandler;
            
            colck.MinuteChange += MinuteChangeHandler;
            colck.HourChange += HourChangeHandler;
            colck.YearChange += YearChangeHandler;
            colck.DayChange += DayChangeHandler;
            colck.MonthChange += MonthChangeHandler;;
            colck.MillisecondChange += MillisecondChangeEventHandler;

            network.Update += NetworkChangeHandler;
            
            menu = new MyMenu();
            viewGrid.ContextMenu = menu;
            AddMenuEvent();
            _thisViewPosition = ViewPosition.InitialValue;
        }

        private void MillisecondChangeEventHandler(object sender, TimeUpdateEventArgs e)
        {
            StateIntegrate(View.rectangleState);
            if (Configure.AllFirst)
            {
               View.SetTopmost(true);
            }
        }

        private void DragEnd()
        {
            if (View.Dispatcher.CheckAccess())
            {
                switch (_thisViewPosition)
                {
                    case ViewPosition.LetfEdge:
                        View.SetWindowPosition(View.Top, View.WindowWidth - 
                            View.Width + viewGrid.Width
                            + View.DragRect.Width);
                        break;
                    case ViewPosition.RightEdge:
                        View.SetWindowPosition(View.Top,- viewGrid.Width - 10);
                        break;
                }
            }
            else {
                DragEndEvent dragEnd = DragEnd;
                View.Dispatcher.Invoke(dragEnd);
            }
        }

        private void MonthChangeHandler(object sender, TimeUpdateEventArgs e)
        {

        }

        private void DayChangeHandler(object sender, TimeUpdateEventArgs e)
        {

        }

        private void YearChangeHandler(object sender, TimeUpdateEventArgs e)
        {

        }

        private void HourChangeHandler(object sender, TimeUpdateEventArgs e)
        {

        }

        private void MinuteChangeHandler(object sender, TimeUpdateEventArgs e)
        {

        }

        private void NetworkChangeHandler(object sender, NicDataEventArgs e)
        {
            View.UpdateUploadText(network.Upload.ToString());
            View.UpdateDownloadText(network.Download.ToString());
        }
        /// <summary>
        /// 控制时钟上冒号的闪烁效果（true为显示）
        /// </summary>
        private bool _time = true;
        private void SecondChangeHandler(object sender, TimeUpdateEventArgs e)
        {
            View.UpdateSecondText(IntToString(colck.Second));
            string Hour = IntToString(colck.Hour), Minute = IntToString(colck.Minute);
            if (_time)
            {
                View.UpdateTimeText($"{Hour}:{Minute}");
                _time = false;
            }
            else {
                View.UpdateTimeText($"{Hour} {Minute}");
                _time = true;
            }
            
        }
        private string IntToString(int value)
        {
            if (value < 10 && value >= 0)
            {
                return $"0{value}";
            }
            return value.ToString();
        }

        private void AddMenuEvent()
        {
            menu.menuItem["退出"].Click += (ss, ee) => {
                View.ViewClose();
            };
            menu.menuItem["设置"].Click += SettingClick;
        }

        private void SettingClick(object sender, RoutedEventArgs e)
        {
            DialongWindow win = new DialongWindow();
            win.GridWindow.Children.Add(new Setting());
            win.Show();
        }

        private void StateIntegrate(RectangleState state)
        {
            switch (state)
            {
                case RectangleState.Drag:
                    CalculationViewPosition();
                    Drag();
                    break;
                case RectangleState.DragEnd:
                    CalculationViewPosition();
                    DragEnd();
                    break;
            }
        }
        private void CalculationViewPosition()
        {
            if (View.Dispatcher.CheckAccess())
            {
                var thisPoint = View.Left - View.Width / 2;
                var core = (View.WindowWidth - View.Width) / 2 - View.Width;
                if (thisPoint >= core)//左
                {
                    var edgePoint = View.WindowWidth - View.Width - viewGrid.Width /2;
                    if (thisPoint >= edgePoint)
                    {
                        _thisViewPosition = ViewPosition.LetfEdge;
                    }
                    else
                    {
                        _thisViewPosition = ViewPosition.Letf;
                    }
                   
                }
                else {//右边
                    var edgePoint = - viewGrid.Width - 50;
                    if (thisPoint <= edgePoint)
                    {
                        _thisViewPosition = ViewPosition.RightEdge;
                    }
                    else
                    {
                        _thisViewPosition = ViewPosition.Right;
                    }
                }
                
            }
            else
            {
                CalculationPosition Calculation = CalculationViewPosition;
                View.Dispatcher.Invoke(Calculation);
            }
            
        }

        private void Drag()
        {
            switch (_thisViewPosition)
            {
                case ViewPosition.Letf:
                    View.PositionTransformation(true);
                    break;
                case ViewPosition.LetfEdge:
                    View.PositionTransformation(true);
                    break;
                case ViewPosition.Right:
                    View.PositionTransformation(false);
                    break;
                case ViewPosition.RightEdge:
                    View.PositionTransformation(false);
                    break;
            }
        }
    }
    enum ViewPosition
    {
        Letf=0,
        Right,
        RightEdge,
        LetfEdge,
        Null,
        InitialValue,
    }
}
