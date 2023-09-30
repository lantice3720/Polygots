using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polygots.Properties;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Polygots
{
    internal static class Program
    {
        public static List<PolygotForm> PolygotList = new List<PolygotForm>();

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("Initializeing Polygots v{0}", Resources.version);

            var timer = new Timer();
            timer.Interval = 1000 / 60d;
            timer.Elapsed += new ElapsedEventHandler(Work);
            timer.Start();

            using (var tray = new NotifyIcon())
            {
                tray.Text = "Polygots";
                tray.Icon = Resources.Rect;
                tray.Visible = true;

                tray.ContextMenuStrip = new ContextMenuStrip();
                tray.ContextMenuStrip.Items.Add("About", null,
                    (sender, e) => MessageBox.Show($"Polygots v{Resources.version}", "Info", MessageBoxButtons.OK,
                        MessageBoxIcon.None));
                tray.ContextMenuStrip.Items.Add("Summon", null, TrayMenu_Summon);
                tray.ContextMenuStrip.Items.Add("Kill All", null, TrayMenu_KillAll);
                tray.ContextMenuStrip.Items.Add("Exit", null, TrayMenu_Exit);

                Application.Run();
            }

            timer.Stop();
            timer.Dispose();

            PolygotList.ForEach((polygot) =>
            {
                polygot.Close();
                polygot.Dispose();
            });

            Console.WriteLine("Bye!");
        }

        private static void TrayMenu_Summon(object sender, EventArgs e)
        {
            new PolygotForm().Show();
        }

        private static void TrayMenu_KillAll(object sender, EventArgs e)
        {
            PolygotList.ForEach((polygot) =>
            {
                polygot.Close();
                polygot.Dispose();
            });
            PolygotList.Clear();
        }

        private static void TrayMenu_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void Work(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(PolygotList.Count);
            PolygotList.ForEach((polygot) =>
            {
                // Physics Calculation
                Vector2 velocity = polygot.Velocity;
                Point location = polygot.Location;
                int bottom = Screen.PrimaryScreen.WorkingArea.Height;

                // Gravity effect
                velocity.Y += 0.98f;

                // Bounce effect
                if (polygot.Location.Y + polygot.Height >= bottom)
                {
                    velocity.Y *= -0.9f;
                }

                // MultiThread Safety
                polygot.Invoke(new MethodInvoker(delegate ()
                {
                    polygot.Location = new Point(polygot.Location.X + (int)velocity.X,
                        polygot.Location.Y + (int)velocity.Y);

                    polygot.Velocity = velocity;
                }));
            });
        }
    }
}