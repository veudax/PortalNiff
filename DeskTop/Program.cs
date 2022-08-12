
using Syncfusion.WinForms.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Suportte
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Força a cultura do Brasil para a data e hora

            // Creating a Global culture specific to our application.
            System.Globalization.CultureInfo cultureInfo =
                  new System.Globalization.CultureInfo("pt-BR");
            // Assigning our custom Culture to the application.
            Application.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            #endregion

            Syncfusion.WinForms.Controls.SfSkinManager.LoadAssembly(typeof(Office2016Theme).Assembly);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Principal());
            Application.Run(new TelaInicial());
        }
    }
}
