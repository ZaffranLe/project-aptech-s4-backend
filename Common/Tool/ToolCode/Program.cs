using System;
using System.Windows.Forms;

namespace CommonicationMemory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LoginScreen());
            Application.Run(new MainScreen());
        }
    }
}