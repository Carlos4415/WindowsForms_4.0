// Aplicação Windows Forms com menu principal

using System;
using System.Windows.Forms;
using WindowsForms;

namespace WindowsFormsApp
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Menu_Principal_UC());
        }
    }
}
