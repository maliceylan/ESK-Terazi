using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Windows.Forms;

namespace TeraziDevEx
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            Application.Run(new Terazi());


        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                Application.Exit();
            }
        }


        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                MessageBox.Show(e.Exception.Message + e.Exception.StackTrace);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
