using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clock.Misc
{
   public class CreateCmd
    {
        public static string create(string str)
        {
            //process用于调用外部程序
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //调用cmd.exe
            p.StartInfo.FileName = "cmd.exe";
            //是否指定操作系统外壳进程启动程序
            p.StartInfo.UseShellExecute = false;
            //可能接受来自调用程序的输入信息
            //重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            //重定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
            //输入命令
            p.StandardInput.WriteLine(str);
            //一定要关闭。
            System.Threading.Thread.Sleep(500);
            p.StandardInput.WriteLine("exit");
            p.Close();
            return p.StandardOutput.ReadToEnd();
        }
    }
}
