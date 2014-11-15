using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MyJournal
{
    static class Program
    {
        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void BringToFront()
        {
            IntPtr handle = FindWindow(null, "Journal");

            if (handle == IntPtr.Zero)
            {
                return;
            }

            SetForegroundWindow(handle);
            ShowWindow(handle, 9); //restore
        }

        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    BringToFront();
                    return;
                }

                Application.ThreadException += ThreadExceptionFunction;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionFunction;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1());
            }
        }

        private static void UnhandledExceptionFunction(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show(ex.Message, @"UPS...");
        }

        private static void ThreadExceptionFunction(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, @"UPS");
        }
    }
}
