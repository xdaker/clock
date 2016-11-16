using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clock.DialogBox
{
    public interface IButtonEvent
    {
         event EventHandler<OkEventArgs> CloseEvent;
        DialongWindow Window { get; set; }
    }
}
