using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Sxer.Tool.WindowStyle
{
    public class SystemTray : IDisposable
    {
        [DllImport("shell32.dll")]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);

        public NotifyIcon trayIcon;
        public ContextMenuStrip trayMenuStrip;
        public ContextMenu trayMenu;
        public static bool useOldMenuAPI = true;
        public SystemTray(System.Drawing.Icon icon)
        {
            trayIcon = new NotifyIcon();
            trayIcon.Text = UnityEngine.Application.productName;

            trayIcon.Icon = icon;

            if (useOldMenuAPI)
            {
                trayMenu = new ContextMenu();
                trayIcon.ContextMenu = trayMenu;
            }
            else
            {
                trayMenuStrip = new ContextMenuStrip();
                trayIcon.ContextMenuStrip = trayMenuStrip;
            }
            trayIcon.Visible = true;
        }

        //Currently does not work
        //public void SetIcon(Texture2D icon)
        //{
        //    using (MemoryStream ms = new MemoryStream(icon.EncodeToPNG()))
        //    {
        //        ms.Seek(0, SeekOrigin.Begin);
        //        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);

        //        System.Drawing.Icon tIcon = System.Drawing.Icon.FromHandle(bmp.GetHicon());
        //        trayIcon.Icon = tIcon;
        //    }
        //}

        public void SetTitle(string title)
        {
            trayIcon.Text = title;
        }

        public ToolStripItem AddItem(string label, Action function)
        {
            return trayMenuStrip.Items.Add(label, null, (object sender, EventArgs e) =>
            {
                if (function != null)
                {
                    function();
                }
            });
        }

        public void AddMenuItem(string label, Action function)
        {
            trayMenu.MenuItems.Add(label, (object sender, EventArgs e) =>
            {
                if (function != null)
                {
                    function();
                }
            });
        }
        public void AddMenuItemSeparator()
        {
            trayMenu.MenuItems.Add("-");
        }


        public void AddSeparator()
        {
            trayMenuStrip.Items.Add("-");
        }

        public void AddDoubleClickEvent(Action action)
        {
            trayIcon.DoubleClick += (object sender, EventArgs e) =>
            {
                if (action != null)
                {
                    action();
                }
            };
        }
        public void AddSingleClickEvent(Action action)
        {
            trayIcon.MouseDown += (object sender, MouseEventArgs e) =>
            {
                if (action != null)
                {
                    action();
                }
            };
        }

        public void ShowNotification(int duration, string title, string text)
        {
            trayIcon.Visible = true;
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = text;
            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.ShowBalloonTip(duration * 1000);
        }

        public void Dispose()
        {
            trayIcon.Visible = false;
            trayMenuStrip.Dispose();
            trayIcon.Dispose();
        }
    }

}
