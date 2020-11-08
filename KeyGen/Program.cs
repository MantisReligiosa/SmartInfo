using Common.Keygen;
using Constants;
using System;
using System.Windows.Forms;

namespace KeyGen
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(Activation.Key, Activation.IV));
        }
    }
}
