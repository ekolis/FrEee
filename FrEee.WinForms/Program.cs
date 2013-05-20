using System;
using System.Threading;
using System.Windows.Forms;
using FrEee.WinForms.Forms;

namespace FrEee.WinForms
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
            Thread.Sleep(3000);
            Application.Run(new GameForm());
        }
    }
}
