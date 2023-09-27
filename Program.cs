using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polygots.Properties;

namespace Polygots
{
    internal static class Program
    {
        public static List<DialogResult> Dialogs = new List<DialogResult>();

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("Initializeing Polygots v{0}", Resources.version);

            using (var tray = new NotifyIcon())
            {
                tray.Text = "Polygots";
                tray.Icon = Resources.Rect;
                tray.Visible = true;

                tray.ContextMenuStrip = new ContextMenuStrip();
                tray.ContextMenuStrip.Items.Add("About", null, (sender, e) => MessageBox.Show($"Polygots v{Resources.version}", "Info", MessageBoxButtons.OK, MessageBoxIcon.None));
                tray.ContextMenuStrip.Items.Add("Summon", null, TrayMenu_Summon);
                tray.ContextMenuStrip.Items.Add("Exit", null, TrayMenu_Exit);

                Application.Run();
            }
            // Application.Run(new Form1());
        }

        private static void TrayMenu_Summon(object sender, EventArgs e)
        {
            new PolygotForm().Show();
        }

        private static void TrayMenu_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
