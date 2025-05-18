using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vRatServer
{
    static class Program
    {
        public static MainForm f1;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// var n = new NotifyIcon();
        /// 
                
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f1 = new MainForm();
            Application.Run(f1);

        }
    }
}
