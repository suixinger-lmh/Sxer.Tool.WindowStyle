using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Sxer.Tool.WindowStyle
{
    public class WinApi
    {
        /// <summary>
        /// 根据class名或者窗口名称找窗体
        /// </summary>
        /// <param name="lpClassName">类名</param>
        /// <param name="lpWindowName">窗口名</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 获取系统前台窗口的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 获取当前进程活动窗口的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();



        public const int HWND_BOTTOM = 1; //HWND_BOTTOM：1将窗口置于Z序的底部。
        public const int HWND_NOTOPMOST = -2; //HWND_NOTOPMOST：-2将窗口置于所有非顶层窗口之上（即在所有顶层窗口之后）。
        public const int HWND_TOP = 0; //HWND_TOP:0将窗口置于Z序的顶部。
        public const int HWND_TOPMOST = -1;//HWND_TOPMOST:-1将窗口置于所有非顶层窗口之上。即使窗口未被激活窗口也将保持顶级位置。

        public const uint SWP_NOSIZE = 0x0001; //SWP_NOSIZE：维持当前尺寸（忽略cx和Cy参数）。
        public const uint SWP_NOMOVE = 0x0002;// SWP_NOMOVE：维持当前位置（忽略X和Y参数）。
        public const uint SWP_NOZORDER = 0x0004;//SWP_NOZORDER：维持当前Z序（忽略hWndlnsertAfter参数）。
        public const uint SWP_NOREDRAW = 0x0008;//SWP_NOREDRAW:不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
        public const uint SWP_NOACTIVATE = 0x0010;//  SWP_NOACTIVATE：不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）。
        public const uint SWP_FRAMECHANGED = 0x0020;// SWP_FRAMECHANGED：给窗口发送WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送WM_NCCALCSIZE。
        public const uint SWP_SHOWWINDOW = 0x0040;//  SWP_SHOWWINDOW：显示窗口。
        public const uint SWP_HIDEWINDOW = 0x0080;// SWP_HIDEWINDOW;隐藏窗口。
        public const uint SWP_NOCOPYBITS = 0x0100;// SWP_NOCOPYBITS：清除客户区的所有内容。如果未设置该标志，客户区的有效内容被保存并且在窗口尺寸更新和重定位后拷贝回客户区。
        public const uint SWP_NOOWNERZORDER = 0x0200;//   SWP_NOOWNERZORDER：不改变z序中的所有者窗口的位置。
        public const uint SWP_NOSENDCHANGING = 0x0400;//  SWP_NOSENDCHANGING：防止窗口接收WM_WINDOWPOSCHANGING消息。
        public const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;//
        /*
         * SWP_ASYNCWINDOWPOS：如果调用进程不拥有窗口，系统会向拥有窗口的线程发出需求。这就防止调用线程在其他线程处理需求的时候发生死锁。
           SWP_DEFERERASE：防止产生WM_SYNCPAINT消息。
           SWP_DRAWFRAME：在窗口周围画一个边框（定义在窗口类描述中）。
           SWP_NOREPOSITION：与SWP_NOOWNERZORDER标志相同。
         */
        /// <summary>
        /// 设置窗口位置，大小，z轴序列位置，窗口特性
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter">z序  HWND_xxx</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags">设置窗口特性  SWP_xxx</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        public const int GWL_EXSTYLE = -20;//设定一个新的扩展风格。
        public const int GWL_STYLE = -16;//设定一个新的窗口风格。
        /**
         * GWL_HINSTANCE-6设置一个新的应用程序实例句柄。
           GWL_ID -12 设置一个新的窗口标识符。
           GWL_USERDATA -21 设置与窗口有关的32位值。每个窗口均有一个由创建该窗口的应用程序使用的32位值。
           GWL_WNDPROC -4 为窗口设定一个新的处理函数。
           GWL_HWNDPARENT -8 改变子窗口的父窗口,应使用SetParent函数。
         */

        public const int WS_POPUP = 0x800000;//弹出式窗体，没有标题栏！
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_BORDER = 0x00800000;//细的黑边框
        public const int WS_CAPTION = 0x00C00000;//标题栏
        public const int WS_VISIBLE = 0x10000000;//：创建一个初始状态为可见的窗口。
        public const int WS_EX_TOOLWINDOW = 0x80;//不在任务栏显示
        public const int WS_EX_TRANSPARENT = 0x20;//窗口事件穿透到底层窗口。（需要搭配WS_EX_LAYERED才能实现穿透）
        public const int WS_EX_LAYERED = 0x00080000; //创建一个分层窗口,为了实现异形的窗口，即可以透明融合的窗口
        //SetLayeredWindowAttribute可以设置透明度
        //是否必须 WS_POPUP 搭配 (待测试)
        /*
WS_SYSMENU：系统菜单(只有设置后才有最小化、最大化、关闭)
WS_MINIMIZEBOX ：最小化按钮
WS_MAXIMIZEBOX：最大化按钮
WS_MINIMIZE：启动时最小化
WS_MAXIMIZE：启动时最大化

WS_EX_APPWINDOW：当窗口可见时，将一个顶层窗口放置到任务条上。
WS_EX_TOOLWINDOW：不在任务栏里显示条目
WS_EX_WINDOWEDGE：
WS_EX_CLIENTEDGE：具有凹陷感

WS_EX_NODRAG:防止窗口被移动
WS_EX_ACCEPTFILES：指定以该风格创建的窗口接受一个拖拽文件。

WS_EX_CLIENTEDGE：指定窗口有一个带阴影的边界。
WS_EX_CONTEXTHELP：在窗口的标题条包含一个问号标志。当用户点击了问号时，鼠标光标变为一个问号的指针、如果点击了一个子窗口，则子窗口接收到WM_HELP消息。子窗口应该将这个消息传递给父窗口过程，父窗口再通过HELP_WM_HELP命令调用WinHelp函数。这个Help应用程序显示一个包含子窗口帮助信息的弹出式窗口。 WS_EX_CONTEXTHELP不能与WS_MAXIMIZEBOX和WS_MINIMIZEBOX同时使用。
WS_EX_CONTROLPARENT：允许用户使用Tab键在窗口的子窗口间搜索。
WS_EX_DLGMODALFRAME：创建一个带双边的窗口；该窗口可以在dwStyle中指定WS_CAPTION风格来创建一个标题栏。
WS_EX_LAYERED：创建一个分层窗口
WS_EX_LEFT：窗口具有左对齐属性，这是缺省设置的。
WS_EX_LEFTSCROLLBAR：如果外壳语言是如Hebrew，Arabic，或其他支持reading order alignment的语言，则标题条（如果存在）则在客户区的左部分。若是其他语言，在该风格被忽略并且不作为错误处理。
WS_EX_LTRREADING：窗口文本以LEFT到RIGHT（自左向右）属性的顺序显示。这是缺省设置的。
WS_EX_MDICHILD：创建一个MDI子窗口。
WS_EX_NOPATARENTNOTIFY：指明以这个风格创建的窗口在被创建和销毁时不向父窗口发送WM_PARENTNOTFY消息。
WS_EX_OVERLAPPEDWINDOW：WS_EX_CLIENTEDGE和WS_EX_WINDOWEDGE的组合。
WS_EX_PALETTEWINDOW：WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW和WS_WX_TOPMOST风格的组合WS_EX_RIGHT:窗口具有普通的右对齐属性，这依赖于窗口类。只有在外壳语言是如Hebrew,Arabic或其他支持读顺序对齐（reading order alignment）的语言时该风格才有效，否则，忽略该标志并且不作为错误处理。
WS_EX_RIGHTSCROLLBAR：垂直滚动条在窗口的右边界。这是缺省设置的。
WS_EX_RTLREADING：如果外壳语言是如Hebrew，Arabic，或其他支持读顺序对齐（reading order alignment）的语言，则窗口文本是一自左向右）RIGHT到LEFT顺序的读出顺序。若是其他语言，在该风格被忽略并且不作为错误处理。
WS_EX_STATICEDGE：为不接受用户输入的项创建一个3一维边界风格
WS_EX_TOOLWINDOW：创建工具窗口，即窗口是一个游动的工具条。工具窗口的标题条比一般窗口的标题条短，并且窗口标题以小字体显示。工具窗口不在任务栏里显示，当用户按下alt+Tab键时工具窗口不在对话框里显示。如果工具窗口有一个系统菜单，它的图标也不会显示在标题栏里，但是，可以通过点击鼠标右键或Alt+Space来显示菜单。
WS_EX_TOPMOST：指明以该风格创建的窗口应放置在所有非最高层窗口的上面并且停留在其L，即使窗口未被激活。使用函数SetWindowPos来设置和移去这个风格。
WS_EX_TRANSPARENT：指定以这个风格创建的窗口在窗口下的同属窗口已重画时，该窗口才可以重画。
由于其下的同属窗口已被重画，该窗口是透明的。
WS_BORDER：创建一个带边框的窗口。
WS_CAPTION：创建一个有标题框的窗口（包括WS_BODER风格）。
WS_CHILD：创建一个子窗口。这个风格不能与WS_POPUP风格合用。
WS_CHILDWINDOW：与WS_CHILD相同。
WS_CLIPCHILDREN：当在父窗口内绘图时，排除子窗口区域。在创建父窗口时使用这个风格。
WS_CLIPSIBLINGS：排除子窗口之间的相对区域，也就是，当一个特定的窗口接收到WM_PAINT消息时，WS_CLIPSIBLINGS 风格将所有层叠窗口排除在绘图之外，只重绘指定的子窗口。如果未指定WS_CLIPSIBLINGS风格，并且子窗口是层叠的，则在重绘子窗口的客户区时，就会重绘邻近的子窗口。
WS_DISABLED：创建一个初始状态为禁止的子窗口。一个禁止状态的窗口不能接受来自用户的输入信息。
WS_DLGFRAME：创建一个带对话框边框风格的窗口。这种风格的窗口不能带标题条。
WS_GROUP：指定一组控制的第一个控制。这个控制组由第一个控制和随后定义的控制组成，自第二个控制开始每个控制，具有WS_GROUP风格，每个组的第一个控制带有WS_TABSTOP风格，从而使用户可以在组间移动。用户随后可以使用光标在组内的控制间改变键盘焦点。
WS_HSCROLL：创建一个有水平滚动条的窗口。
WS_ICONIC：创建一个初始状态为最小化状态的窗口。与WS_MINIMIZE风格相同。
WS_MAXIMIZE：创建一个初始状态为最大化状态的窗口。
WS_MAXIMIZEBOX：创建一个具有最大化按钮的窗口。该风格不能与WS_EX_CONTEXTHELP风格同时出现，同时必须指定WS_SYSMENU风格。
WS_OVERLAPPED：产生一个层叠的窗口。一个层叠的窗口有一个标题条和一个边框。与WS_TILED风格相同。
WS_OVERLAPPEDWINDOW：创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXIMIZEBOX风格的层叠窗口，与WS_TILEDWINDOW风格相同。
WS_POPUP：创建一个弹出式窗口。该风格不能与WS_CHILD风格同时使用。
WS_POPUPWINDOW：创建一个具有WS_BORDER，WS_POPUP,WS_SYSMENU风格的窗口，WS_CAPTION和WS_POPUPWINDOW必须同时设定才能使窗口某单可见。
WS_SIZEBOX：创建一个可调边框的窗口，与WS_THICKFRAME风格相同。
WS_SYSMENU：创建一个在标题条上带有窗口菜单的窗口，必须同时设定WS_CAPTION风格。
WS_TABSTOP：创建一个控制，这个控制在用户按下Tab键时可以获得键盘焦点。按下Tab键后使键盘焦点转移到下一具有WS_TABSTOP风格的控制。
WS_THICKFRAME：创建一个具有可调边框的窗口，与WS_SIZEBOX风格相同。
WS_TILED：产生一个层叠的窗口。一个层叠的窗口有一个标题和一个边框。与WS_OVERLAPPED风格相同。
WS_TILEDWINDOW:创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU， WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXIMIZEBOX风格的层叠窗口。与WS_OVERLAPPEDWINDOW风格相同。

WS_VSCROLL：创建一个有垂直滚动条的窗口。
         
         */

        /// <summary>
        /// 改变窗体属性
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex">改变窗口上的何种属性</param>
        /// <param name="dwNewLong">指定该属性为一个新的具体样式</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);


        // 获取窗口的类名
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        // 获取窗口文本长度
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        // 获取窗口文本，文本会塞入StringBuilder中，需要指明字符串最大长度nMaxCount
        [DllImport("User32.dll", EntryPoint = "GetWindowText")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // 最小化
        public const int SW_SHOWMINIMIZED = 2;
        // 最大化
        public const int SW_SHOWMAXIMIZED = 3;
        // 还原
        public const int SW_SHOWRESTORE = 1;
        // 隐藏
        public const int SW_HIDE = 0;
        // 还原
        public const int SW_RESTORE = 9;

  /* 
 SW_HIDE 隐藏窗口并将活动状态传递给其它窗口。
 SW_MINIMIZE 最小化窗口并激活系统列表中的顶层窗口。
 SW_RESTORE 激活并显示窗口。如果窗口是最小化或最大化的，Windows恢复其原来的大小和位置。 
 SW_SHOW 激活窗口并以其当前的大小和位置显示。
 SW_SHOWMAXIMIZED 激活窗口并显示为最大化窗口。
 SW_SHOWMINIMIZED 激活窗口并显示为图标。 
 SW_SHOWMINNOACTIVE 将窗口显示为图标。当前活动的窗口将保持活动状态。
 SW_SHOWNA 按照当前状态显示窗口。当前活动的窗口将保持活动状态。
 SW_SHOWNOACTIVATE 按窗口最近的大小和位置显示。当前活动的窗口将保持活动状态。 
 SW_SHOWNORMAL 激活并显示窗口。如果窗口是最小化或最大化的，则Windows恢复它原来的大小和位置。
  */
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);



        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        public const int LWA_ALPHA = 2;
        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern int SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte bAlpha, int dwFlags);
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool repaint);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        /*// //1.先获得桌面窗口
        CWnd* pDesktopWnd = CWnd::GetDesktopWindow();
        //2.获得一个子窗口
        CWnd* pWnd = pDesktopWnd->GetWindow(GW_CHILD);
        //3.循环取得桌面下的所有子窗口*/
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }


        //为窗口扩展边框区域。
        [DllImport("Dwmapi.dll")]
        public static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        //慎用
        [DllImport("user32.dll")]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);
        //
        //C:\Users\DS\AppData\Roaming\Microsoft\Windows\Start Menu


        //注册表
        //public void RegisterStartOpen()
        //{
        //    try
        //    {
        //        string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        //        RegistryKey rgkRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        //        if (rgkRun == null)
        //        {
        //            rgkRun = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
        //        }
        //        rgkRun.SetValue("dhstest", path);
        //    }
        //    catch
        //    {
        //        Debug.Log(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        //    }
        //    finally
        //    {
        //        regeditkey();
        //    }
        //}

        //public void CancelStartOpen()
        //{
        //    //MessageBox.Show("取消开机自启动，需要修改注册表", "提示");
        //    try
        //    {
        //        string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        //        RegistryKey rgkRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
        //            true);
        //        if (rgkRun == null)
        //        {
        //            rgkRun = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
        //        }
        //        rgkRun.DeleteValue("dhstest", false);
        //    }
        //    catch
        //    {
        //        Debug.Log("error OnBtn2Click");
        //    }
        //    finally
        //    {
        //        regeditkey();
        //    }

        //}
        //private void regeditkey()
        //{
        //    Debug.Log("regeditkey");
        //    RegistryKey rgkRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        //}




        // GetSystemMetrics实际获取的是系统记录的分辨率，不是物理分辨率，如屏幕2560*1600，显示缩放200%，这里获取到的是1280*800
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        public static int SM_CXSCREEN = 0; //主屏幕分辨率宽度
        public static int SM_CYSCREEN = 1; //主屏幕分辨率高度
        public static int SM_CYCAPTION = 4; //标题栏高度
        public static int SM_CXFULLSCREEN = 16; //最大化窗口宽度（减去任务栏）
        public static int SM_CYFULLSCREEN = 17; //最大化窗口高度（减去任务栏）











    }



 
}

