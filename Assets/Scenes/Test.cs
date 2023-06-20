using Sxer.Tool.WindowStyle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text tt;
    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;
        StartCoroutine(WindowHelper.ResizeGameScreenRes(3840, 1920));
        //StartCoroutine(WindowHelper.ResizeGameScreenRes(200, 200));
        //Screen.fullScreenMode = FullScreenMode.Windowed;
        //Screen.fullScreen = false;
        //StartCoroutine( WindowHelper.ResizeGameScreenRes(5600, 5600,false));
        //Debug.Log();
        //WindowHelper.ClearRegisterAll();
        tt.text += Screen.width + "/" + Screen.height + "\r\n";

        tt.text += Screen.currentResolution.width + "/" + Screen.currentResolution.height + "\r\n";
        //WindowHelper.ResizeGameScreenRes(560, 560);
        //WindowHelper.ResizeGameScreenRes(560,560);
        //WindowHelper.WindowStyle_WithoutToolBar();
        // WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.WithoutToolBar);
        //  WindowHelper.WindowStyle_FullScreen();
        //Screen.fullScreen = false;
        //StartCoroutine(WinApi.Setpositiossn(1920, 1080));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.WithoutToolBar);
            StartCoroutine(WindowHelper.ResizeGameScreenRes(3840, 1920,true,true));
            //WindowHelper.ResizeGameScreenRes(560, 560);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.WithToolBar);
            StartCoroutine(WindowHelper.ResizeGameScreenRes(3840, 1920, true, true));
            //WindowHelper.ResizeGameScreenRes(560, 560);
            //WindowHelper.ResizeGameScreenRes(560, 560);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            StartCoroutine(WindowHelper.ResizeGameScreenRes(5760, 2160,false,true));
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            //WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.Hide);
            WindowHelper.WindowShowSet( WindowHelper.WindowStyleName.ShowHide);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            WindowHelper.WindowShowSet(WindowHelper.WindowStyleName.ShowRestore2);
            //WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.Visible);
        }


        if (Input.GetKeyUp(KeyCode.Z))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.ShowTopMost);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.ShowBottom);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.ShowNoTopMost);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            WindowHelper.WindowStyleSet(WindowHelper.WindowStyleName.ShowTop);
        }


        if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.UpperLeft);
        }
        if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.UpperCenter);
        }
        if (Input.GetKeyUp(KeyCode.Keypad9))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.UpperRight);
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.MiddleLeft);
        }
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.MiddleCenter);
        }
        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.MiddleRight);
        }
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.LowerLeft);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.LowerCenter);
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            WindowHelper.WindowPosSet(WindowHelper.PosStyle.LowerRight);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            WindowHelper.WindowShowSet(WindowHelper.WindowStyleName.ShowMinimized);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            WindowHelper.WindowShowSet(WindowHelper.WindowStyleName.ShowMaximized);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            WindowHelper.WindowShowSet(WindowHelper.WindowStyleName.ShowRestore);
        }

         
        if (Input.GetKeyUp(KeyCode.L))
        {
            //  WindowHelper.WindowShowSet(WindowHelper.WindowStyleName.ShowHide);

            WindowHelper.TestMouse();
            //WindowHelper.CreateSystemTray(WindowHelper.CustomTrayIcon(@"C:\Users\DS\Desktop\1\Sxer.jpg", 100, 100));
            //WindowHelper.AddSystemTray(WindowHelper.CustomTrayIcon(@"C:\Users\DS\Desktop\1\Sxer.jpg", 100, 100));
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            OtherWinAPi.SetStartWithWindows();
            tt.text += "\r\n"+OtherWinAPi.GetRegistData("Screenmanager Resolution Height Default");
            tt.text += "\r\n" + OtherWinAPi.GetRegistData("unity.player_sessionid");
        }

    }

    //测试模式影响
    //void TestScreenMode()
    //{
    //    WinApi.GetActiveWindow();
    //}

    private void OnApplicationQuit()
    {
        //WindowHelper.
    }

}
