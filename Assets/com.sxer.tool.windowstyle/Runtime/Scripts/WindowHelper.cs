using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sxer.Tool.WindowStyle
{
    public class WindowHelper
    {
        public enum WindowStyleName
        {
            //窗口样式
            WithoutToolBar,//无标题栏
            WithToolBar,//带标题栏
            WithoutToolBar1,//弹出式窗口 会有边框  可以搭配其他参数去掉边框
            WithToolBar1,//取消弹出式窗口 

            //
            Sppppp1,//鼠标穿透


            Visible,//显示
            Hide,//隐藏

            ShowMinimized,//最小化
            ShowMaximized,//最大化
            ShowRestore,//还原
            ShowHide,
            ShowRestore2,


           // FullScreen,//
            ShowTopMost,//显示在最顶层且不被遮盖
            ShowBottom,//显示在最底层
            ShowNoTopMost,//能够被遮盖
            ShowTop//显示在最顶层
        }
        public enum PosStyle
        {
            UpperLeft,
            UpperCenter,
            UpperRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            LowerLeft,
            LowerCenter,
            LowerRight
        }

        void DisplayInf()
        {
            //显示器数量
            int displayCount = Display.displays.Length;
            for(int i = 0; i < displayCount; i++)
            {
                Debug.Log(string.Format("display{0}：物理分辨率({1},{2}) 渲染分辨率({3},{4})", i, Display.displays[i].systemWidth, Display.displays[i].systemHeight, Display.displays[i].renderingWidth, Display.displays[i].renderingHeight));
            }
        }

        //void ScreenInf()
        //{
        //    //当前屏幕大小
        //    Debug.Log(string.Format("当前屏幕分辨率({0},{1})", Screen.currentResolution.width, Screen.currentResolution.height));
        //}

        public static IEnumerator ResizeGameScreenRes(float targetScreenWidth, float targetScreenHeight, bool isLimit = true,bool uesWinApi = false)
        {
            Debug.Log(string.Format("当前屏幕分辨率({0},{1})", Screen.currentResolution.width, Screen.currentResolution.height));
            Debug.Log(string.Format("目标窗口分辨率({0},{1})", targetScreenWidth, targetScreenHeight));
            float currentScreenWidth, currentScreenHeight;

            if (isLimit)//屏幕兼容
            {
                //判断当前屏幕大小是否能放下
                if (targetScreenHeight > Screen.currentResolution.height || targetScreenWidth > Screen.currentResolution.width)
                {
                    Debug.Log("需要兼容适配：");
                    //有任意一边放不下
                    float screenScale = (1.0f * Screen.currentResolution.width) / (float)Screen.currentResolution.height;
                    float gameScale = (1.0f * targetScreenWidth) / (float)targetScreenHeight;
                    if (gameScale <= screenScale)
                    {
                        currentScreenHeight = Screen.currentResolution.height;
                        currentScreenWidth = Mathf.FloorToInt(currentScreenHeight * gameScale);
                    }
                    else
                    {
                        currentScreenWidth = Screen.currentResolution.width;
                        currentScreenHeight = Mathf.FloorToInt(currentScreenWidth / gameScale);
                    }
                }
                else
                {
                    currentScreenWidth = targetScreenWidth;
                    currentScreenHeight = targetScreenHeight;
                }
            }
            else
            {
                currentScreenWidth = targetScreenWidth;
                currentScreenHeight = targetScreenHeight;
            }


            Debug.Log(string.Format("实际窗口分辨率({0},{1})", currentScreenWidth, currentScreenHeight));

            yield return new WaitForSeconds(0.1f);

            if (uesWinApi)
            {
                var hwnd = WinApi.FindWindow(null, Application.productName);
                WinApi.SetWindowPos(hwnd, WinApi.HWND_TOP, 0, 0, (int)currentScreenWidth, (int)currentScreenHeight, WinApi.SWP_NOMOVE | WinApi.SWP_SHOWWINDOW | WinApi.SWP_NOZORDER);
            }
            else
            {
                Screen.SetResolution((int)currentScreenWidth, (int)currentScreenHeight, false);
            }


            //ClearRegisterRes();//清理注册表信息

        }
        //public static IEnumerator ResizeGameScreenRes(float targetScreenWidth, float targetScreenHeight)
        //{
        //    Debug.Log(string.Format("当前屏幕分辨率({0},{1})", Screen.currentResolution.width, Screen.currentResolution.height));
        //    Debug.Log(string.Format("目标窗口分辨率({0},{1})", targetScreenWidth, targetScreenHeight));
        //    float currentScreenWidth, currentScreenHeight;


        //    //判断当前屏幕大小是否能放下
        //    if (targetScreenHeight > Screen.currentResolution.height || targetScreenWidth > Screen.currentResolution.width)
        //    {
        //        Debug.Log("需要兼容适配：");
        //        //有任意一边放不下
        //        float screenScale = (1.0f * Screen.currentResolution.width) / (float)Screen.currentResolution.height;
        //        float gameScale = (1.0f * targetScreenWidth) / (float)targetScreenHeight;
        //        if (gameScale <= screenScale)
        //        {
        //            currentScreenHeight = Screen.currentResolution.height;
        //            currentScreenWidth = Mathf.FloorToInt(currentScreenHeight * gameScale);
        //        }
        //        else
        //        {
        //            currentScreenWidth = Screen.currentResolution.width;
        //            currentScreenHeight = Mathf.FloorToInt(currentScreenWidth / gameScale);
        //        }
        //    }
        //    else
        //    {
        //        currentScreenWidth = targetScreenWidth;
        //        currentScreenHeight = targetScreenHeight;
        //    }


        //    Debug.Log(string.Format("实际窗口分辨率({0},{1})", currentScreenWidth, currentScreenHeight));

        //    yield return new WaitForSeconds(0.1f);

        //    //Screen.SetResolution((int)currentScreenWidth, (int)currentScreenHeight, false);


        //    var hwnd = WinApi.FindWindow(null, Application.productName);
        //    WinApi.SetWindowPos(hwnd, WinApi.HWND_TOP, 0, 0, (int)currentScreenWidth, (int)currentScreenHeight, WinApi.SWP_NOMOVE | WinApi.SWP_SHOWWINDOW | WinApi.SWP_NOZORDER);



        //    //ClearRegisterRes();//清理注册表信息

        //}


        public static void GetRegisterDefaultRes(out int width,out int height)
        {
            width = PlayerPrefs.GetInt("Screenmanager Resolution Width Default");
            height = PlayerPrefs.GetInt("Screenmanager Resolution Height Default");
        }

        public static void ClearRegisterRes()
        {
            PlayerPrefs.DeleteKey("Screenmanager Resolution Width");
            PlayerPrefs.DeleteKey("Screenmanager Resolution Height");
        }
        public static void ClearRegisterAll()
        {
            PlayerPrefs.DeleteAll();
        }

     
        //窗口全屏
        public static void WindowStyle_FullScreen()
        {
            var hwnd = WinApi.FindWindow(null, Application.productName);
            WinApi.SetWindowLong(hwnd, WinApi.GWL_STYLE, WinApi.WS_BORDER);   //窗口全屏
            WinApi.SetWindowPos(hwnd, WinApi.HWND_TOPMOST, 0, 0, 0, 0, WinApi.TOPMOST_FLAGS | WinApi.SWP_SHOWWINDOW | WinApi.SWP_NOZORDER);
        }

        public static void WindowStyleSet(WindowStyleName windowStyleName)
        {
            //样式修改在非全屏下修改，全屏会拉伸
            if(Screen.fullScreen)
                Screen.fullScreen = false;

            var hwnd = WinApi.FindWindow(null, Application.productName);
            int swl_type = 0;
            int swl_value = 0;
            int swp_hwnd = WinApi.HWND_TOP; 
            uint swp_value = WinApi.TOPMOST_FLAGS;
            switch (windowStyleName)
            {
                case WindowStyleName.WithoutToolBar:
                    swl_type = WinApi.GWL_STYLE;
                    swl_value = WinApi.GetWindowLong(hwnd, WinApi.GWL_STYLE);
                    swl_value &= ~WinApi.WS_CAPTION;//无标题栏
                    //swl_value = ~WinApi.WS_CAPTION;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    swp_value |= WinApi.SWP_NOZORDER;
                    break;
                case WindowStyleName.WithToolBar:
                    swl_type = WinApi.GWL_STYLE;
                    swl_value = WinApi.GetWindowLong(hwnd, WinApi.GWL_STYLE);
                    swl_value |= WinApi.WS_CAPTION;//带标题栏
                    //swl_value |= ~WinApi.WS_BORDER;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    swp_value |= WinApi.SWP_NOZORDER;
                    break;

                case WindowStyleName.Hide:
                    //swp_value &= ~WinApi.SWP_SHOWWINDOW;
                    swp_value |= WinApi.SWP_HIDEWINDOW;
                    swp_value |= WinApi.SWP_NOZORDER;

                    break;

                case WindowStyleName.Visible:
                    //swp_value &= ~WinApi.SWP_SHOWWINDOW;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    swp_value |= WinApi.SWP_NOZORDER;

                    break;

                case WindowStyleName.ShowTopMost:
                    swp_hwnd = WinApi.HWND_TOPMOST;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    break;

                case WindowStyleName.ShowBottom:
                    swp_hwnd = WinApi.HWND_BOTTOM;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    break;
                case WindowStyleName.ShowNoTopMost:
                    swp_hwnd = WinApi.HWND_NOTOPMOST;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    break;
                case WindowStyleName.ShowTop:
                    swp_hwnd = WinApi.HWND_TOP;
                    swp_value |= WinApi.SWP_SHOWWINDOW;
                    break;


                case WindowStyleName.Sppppp1:
                    //SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP | WS_VISIBLE);
                    //SetWindowLong(windowHandle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TOOLWINDOW | WS_EX_TRANSPARENT); // 实现鼠标穿透

                    break;
               
                    //case WindowStyleName.WithoutToolBar1:
                    //    swl_type = WinApi.GWL_STYLE;
                    //    swl_value = WinApi.GetWindowLong(hwnd, WinApi.GWL_STYLE);

                    //    swl_value &= ~WinApi.WS_BORDER;
                    //    swl_value &= ~WinApi.WS_CAPTION;

                    //    //swl_value = WinApi.WS_POPUP ;
                    //    break;

                    //case WindowStyleName.WithToolBar1:
                    //    swl_type = WinApi.GWL_STYLE;
                    //    swl_value = WinApi.GetWindowLong(hwnd, WinApi.GWL_STYLE);
                    //    //swl_value |= WinApi.WS_BORDER;
                    //    //swl_value |= WinApi.WS_CAPTION;
                    //    swl_value |= WinApi.WS_CAPTION;
                    //    break;
            }

            WinApi.SetWindowLong(hwnd, swl_type, swl_value);
            WinApi.SetWindowPos(hwnd, swp_hwnd, 0, 0, 0, 0, swp_value);
        }



        public static void WindowPosSet(PosStyle posStyle)
        {
            int screenWidth = Screen.currentResolution.width;
            int screenHeight = Screen.currentResolution.height;

            int useX = 0, useY =0;

            switch (posStyle)
            {
                case PosStyle.UpperLeft:
                    useX = 0;
                    break;
                case PosStyle.UpperCenter:
                    useX = (screenWidth  - Screen.width)/2;
                    break;
                case PosStyle.UpperRight:
                    useX = screenWidth - Screen.width;
                    break;
                case PosStyle.MiddleLeft:
                    useY = (screenHeight - Screen.height) / 2;
                    break;
                case PosStyle.MiddleCenter:
                    useX = (screenWidth - Screen.width) / 2;
                    useY = (screenHeight - Screen.height) / 2;
                    break;
                case PosStyle.MiddleRight:
                    useX = screenWidth - Screen.width;
                    useY = (screenHeight - Screen.height) / 2;
                    break;
                case PosStyle.LowerLeft:
                    useY = screenHeight - Screen.height;
                    break;
                case PosStyle.LowerCenter:
                    useX = (screenWidth - Screen.width) / 2;
                    useY = screenHeight - Screen.height;
                    break;
                case PosStyle.LowerRight:
                    useX = screenWidth - Screen.width;
                    useY = screenHeight - Screen.height;
                    break;
            }

            var hwnd = WinApi.FindWindow(null, Application.productName);
            int swp_hwnd = WinApi.HWND_TOP;
            uint swp_value = WinApi.SWP_NOSIZE | WinApi.SWP_NOZORDER;
            WinApi.SetWindowPos(hwnd, swp_hwnd, useX, useY, 0, 0, swp_value);
        }

        public static void WindowPosSet(int x,int y)
        {
            var hwnd = WinApi.FindWindow(null, Application.productName);
            int swp_hwnd = WinApi.HWND_TOP;
            uint swp_value = WinApi.SWP_NOSIZE | WinApi.SWP_NOZORDER;
            WinApi.SetWindowPos(hwnd, swp_hwnd, x, y, 0, 0, swp_value);
        }

        public static void WindowShowSet(WindowStyleName windowStyleName)
        {
            var hwnd = WinApi.FindWindow(null, Application.productName);
            int cmdShow = 0;
            switch (windowStyleName)
            {
                case WindowStyleName.ShowMinimized:
                    cmdShow = WinApi.SW_SHOWMINIMIZED;
                    break;
                case WindowStyleName.ShowMaximized:
                    cmdShow = WinApi.SW_SHOWMAXIMIZED;
                    break;
                case WindowStyleName.ShowRestore:
                    cmdShow = WinApi.SW_SHOWRESTORE;
                    break;
                case WindowStyleName.ShowHide:
                    cmdShow = WinApi.SW_HIDE;
                    break;
                case WindowStyleName.ShowRestore2:
                    cmdShow = WinApi.SW_RESTORE;
                    break;
            }
            WinApi.ShowWindow(hwnd, cmdShow);
        }


        //IEnumerator ChangeRes(int x, int y)
        //{

        //    yield return wa

        //}

        //public static IEnumerator Setpositiossn(int x, int y)
        //{
        //    yield return new WaitForSeconds(0.1f);      //不知道为什么发布于行后，设置位置的不会生效，我延迟0.1秒就可以

        //    IntPtr ptr = FindWindow(null, Application.productName); //TestAnimation为程序名，需要替换
        //    //IntPtr ptr = GetForegroundWindow();
        //    //SetWindowLong(ptr, GWL_STYLE, WS_BORDER);   //窗口全屏
        //    SetWindowPos(ptr, 0, (int)0, (int)0, (int)x, (int)y, 0x0040);   //窗口置顶

        //    Screen.fullScreen = true;

        //    //    SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);      //无边框
        //    //    bool result = SetWindowPos(GetForegroundWindow(), 0, _posX, _posY, _Txtwith, _Txtheight, SWP_SHOWWINDOW);       //设置屏幕大小和位置
        //    //
        //}

        public static SystemTray _icon;
        public static System.Windows.Forms.ToolStripItem _topmost, _runOnStart;
        public static System.Windows.Forms.ToolStripItem[] _roleItem;

        // 创建托盘图标、添加选项
        public static void AddSystemTray(System.Drawing.Icon icon)
        {
            _icon = new SystemTray(icon);
            _topmost = _icon.AddItem("置顶显示", ()=> {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _runOnStart = _icon.AddItem("开机自启", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddItem("重置位置", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddSeparator();
           // AddRoleItem(_icon);
            _icon.AddSeparator();
            _icon.AddItem("查看文档", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddItem("检查更新", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddSeparator();
            _icon.AddItem("退出", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddDoubleClickEvent(() => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _icon.AddSingleClickEvent(() => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });

            _topmost.Image =  null;
            _runOnStart.Image = null;
        }


        public static System.Drawing.Icon CustomTrayIcon(string iconPath, int width, int height)
        {
            System.Drawing.Bitmap bt = new System.Drawing.Bitmap(iconPath);

            System.Drawing.Bitmap fitSizeBt = new System.Drawing.Bitmap(bt, width, height);

            return System.Drawing.Icon.FromHandle(fitSizeBt.GetHicon());
        }


        public static SystemTray _systemTray;
        public static void CreateSystemTray(System.Drawing.Icon icon)
        {
            _systemTray = new SystemTray(icon);
            _systemTray.AddMenuItem("显示", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowRestore2);
            });
            _systemTray.AddMenuItem("隐藏", () => {
                WindowShowSet(WindowHelper.WindowStyleName.ShowHide);
            });
            _systemTray.AddMenuItemSeparator();
            _systemTray.AddMenuItem("退出", () => {
                _systemTray.trayIcon.Visible = false;
                _systemTray.trayIcon.Dispose();
                Application.Quit();
            });
            //System.Windows.Forms.MenuItem closeMenu = new System.Windows.Forms.MenuItem("关闭");
            //System.Windows.Forms.MenuItem closeMenu3 = new System.Windows.Forms.MenuItem("-");
            //System.Windows.Forms.MenuItem closeMenu2 = new System.Windows.Forms.MenuItem("tuichu");
            //System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { closeMenu, closeMenu3, closeMenu2 };
            //_systemTray.trayIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //closeMenu.Click += (a, aa) =>
            //{
            //    WindowShowSet(WindowHelper.WindowStyleName.ShowRestore2);
            //};
            //closeMenu2.Click += (a, aa) =>
            //{
            //    Application.Quit();
            //};



        }


        //仅穿透
        public static void TestMouse()
        {
            var hwnd = WinApi.FindWindow(null, Application.productName);
            int xx = WinApi.GetWindowLong(hwnd,WinApi.GWL_EXSTYLE);
            WinApi.SetWindowLong(hwnd, WinApi.GWL_EXSTYLE, xx | WinApi.WS_EX_TRANSPARENT | WinApi.WS_EX_LAYERED); // 实现鼠标穿透
            WinApi.SetWindowPos(hwnd, 0, 0, 0, 0, 0, WinApi.SWP_NOMOVE | WinApi.SWP_NOSIZE | WinApi.SWP_NOZORDER);

            //只有上边：会穿透，但不会透明

            //加上下边：player setting 需要在分辨率那边 取消勾选use dxgi flip model...d3d11
            //能够透明和穿透

            //WinApi.MARGINS margins = new WinApi.MARGINS() { cxLeftWidth = -1 };
            //// Extend the window into the client area
            ////See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa969512%28v=vs.85%29.aspx 
            //WinApi.DwmExtendFrameIntoClientArea(hwnd, ref margins);


            //改变窗体整体透明度的，包括标题栏
            // WinApi.SetLayeredWindowAttributes(hwnd, 0, 128, WinApi.LWA_ALPHA);
            
        }


        //仅透明
        public static void TestMouse1()
        {
            var hwnd = WinApi.FindWindow(null, Application.productName);
            int xx = WinApi.GetWindowLong(hwnd, WinApi.GWL_EXSTYLE);
            WinApi.SetWindowLong(hwnd, WinApi.GWL_EXSTYLE, xx | WinApi.WS_EX_LAYERED); 
            WinApi.SetWindowPos(hwnd, 0, 0, 0, 0, 0, WinApi.SWP_NOMOVE | WinApi.SWP_NOSIZE | WinApi.SWP_NOZORDER);

            //只有上边：会穿透，但不会透明

            //加上下边：player setting 需要在分辨率那边 取消勾选use dxgi flip model...d3d11
            //能够透明和穿透

            WinApi.MARGINS margins = new WinApi.MARGINS() { cxLeftWidth = -1 };
            // Extend the window into the client area
            //See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa969512%28v=vs.85%29.aspx 
            WinApi.DwmExtendFrameIntoClientArea(hwnd, ref margins);


            //改变窗体整体透明度的，包括标题栏
            // WinApi.SetLayeredWindowAttributes(hwnd, 0, 128, WinApi.LWA_ALPHA);

        }




        public static Vector2 GetSystemResolution()
        {
            int x = WinApi.GetSystemMetrics(WinApi.SM_CXSCREEN);
            int y = WinApi.GetSystemMetrics(WinApi.SM_CYSCREEN);

            return new Vector2(x, y);
        }









    }
}

