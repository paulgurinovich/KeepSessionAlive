using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace KeepSessionAlive
{
    class Program
    {


        [STAThread]
        static void Main(string[] args)
        {
            long timeout = long.Parse(ConfigurationManager.AppSettings["timeout"]);
            long interval = long.Parse(ConfigurationManager.AppSettings["interval"]);

            KeepAliveScript(timeout, interval);

        }

        public static void KeepAliveScript(long timeout, long interval)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch swC = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < timeout)
            {
                Process[] processes = Process.GetProcessesByName("WowClassic");

                if (swC.ElapsedMilliseconds > interval)
                {
                    foreach (Process proc in processes)
                    {
                        //SetForegroundWindow(proc.MainWindowHandle);
                        SwitchToThisWindow(proc.MainWindowHandle, true);

                        SendKeys.SendWait("{UP}");
                        SendKeys.SendWait("{DOWN}");
                    }
                    swC.Restart();
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);

    }
}
