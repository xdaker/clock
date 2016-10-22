using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Clock.Misc
{
  public  class NetworkInterfaces
    {
        //http://www.cnblogs.com/wfwup/archive/2009/12/04/1616904.html
        public static bool Test()
      {
          NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
          foreach (NetworkInterface adapter in fNetworkInterfaces)
          {
              if (adapter.Name.Contains("WLAN"))
                  return true;
          }
            return false;
      }
    }
}
