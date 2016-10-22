using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Clock
{
   public class SelfStarting
    {
        public static bool SetSelfStarting(bool started, string exeName)
        {
            RegistryKey key = null;
            try
            {

                string exeDir = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//打开注册表子项

                if (key == null)//如果该项不存在的话，则创建该子项
                {
                    key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                if (started)
                {
                    try
                    {
                        object ob = key.GetValue(exeName, -1);

                        if (!ob.ToString().Equals(exeDir))
                        {
                            if (!ob.ToString().Equals("-1"))
                            {
                                key.DeleteValue(exeName);//取消开机启动
                            }
                            key.SetValue(exeName, exeDir);//设置为开机启动
                        }
                        key.Close();

                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        key.DeleteValue(exeName);//取消开机启动
                        key.Close();
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                if (key != null)
                {
                    key.Close();
                }
                return false;
            }
        }
    }
}
