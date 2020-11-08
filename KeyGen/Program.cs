using Constants;
using SmartTechnologiesM.Activation;
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
            Application.Run(new KeygenForm(Activation.Key, Activation.IV));
        }
    }
}
