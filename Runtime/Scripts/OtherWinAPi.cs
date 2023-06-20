using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Sxer.Tool.WindowStyle
{
    public class OtherWinAPi : MonoBehaviour
    {
        // 浏览对话框中包含一个编辑框，在该编辑框中用户可以输入选中项的名字。
        const int BIF_EDITBOX = 0x00000010;
        // 新用户界面
        const int BIF_NEWDIALOGSTYLE = 0x00000040;
        const int BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
        const int MAX_PATH_LENGTH = 2048;

        public static string FolderBrowserDlg(string defaultPath = "")
        {
            OpenDlgDir dlg = new OpenDlgDir();
            dlg.pszDisplayName = defaultPath;
            dlg.ulFlags = BIF_USENEWUI;
            //设置hwndOwner==0时，是非模态对话框，设置hwndOwner!=0时为模态对话框
            dlg.hwndOwner = DllOpenFileDialog.GetForegroundWindow();

            IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(dlg);
            char[] charArray = new char[MAX_PATH_LENGTH];
            DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
            string foldPath = new String(charArray);
            foldPath = foldPath.Substring(0, foldPath.IndexOf('\0'));
            return foldPath;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OpenDlgDir
        {
            public IntPtr hwndOwner = IntPtr.Zero;
            public IntPtr pidlRoot = IntPtr.Zero;
            public String pszDisplayName = null;
            public String lpszTitle = null;
            public UInt32 ulFlags = 0;
            public IntPtr lpfn = IntPtr.Zero;
            public IntPtr lParam = IntPtr.Zero;
            public int iImage = 0;
        }

        public class DllOpenFileDialog
        {
            [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
            public static extern IntPtr SHBrowseForFolder([In, Out] OpenDlgDir odd);

            [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
            public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

            /// <summary>
            /// 获取当前窗口句柄
            /// </summary>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();
        }

        //获取硬盘剩余大小
        public static long GetFreeSpace(string pan)
        {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            foreach (DriveInfo d in driveInfos)
            {
                if (d.Name.StartsWith(pan))
                {
                    return d.TotalFreeSpace;
                }

            }
            return 0;
        }





















        
       static  string RegistXTLBBLauncher ;
        public static string GetRegistData(string name)
        {
            RegistXTLBBLauncher = string.Format("Software\\\\{0}\\\\{1}", Application.companyName, Application.productName);
            string registData = "";

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistXTLBBLauncher, true);

            if (reg == null)
            {
                return "";
            }

            if (IsRegistExistKey(reg, name))
            {
                registData = reg.GetValue(name).ToString();
            }

            reg.Close();
            return registData;

        }
        public static void WriteRegistData(string name, string tovalue)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistXTLBBLauncher, true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistXTLBBLauncher);
            }

            reg.SetValue(name, tovalue);

            reg.Close();
        }

        public static bool IsRegistExistKey(RegistryKey key, string name)
        {
            if (key == null)
            {
                return false;
            }
            string[] subkeyNames = key.GetValueNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    return true;
                }
            }
            return false;

        }

        const string RegistRun = @"Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        const string RegistWin32ApiExe = "Win32ApiExe";

        public static string GetExePath
        {
            get
            {
                string saPath = Application.streamingAssetsPath;
                string[] bufSA = saPath.Split('/');
                string end = bufSA[bufSA.Length - 2] + "/" + bufSA[bufSA.Length - 1];
                string updaterPath = saPath.Replace(end, "Win32Api.exe");
                return updaterPath;
            }
        }







        public static void SetStartWithWindows()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistRun, true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistRun);
            }

            reg.SetValue(RegistWin32ApiExe, GetExePath);

        }

        public static bool IsRunWithWindows()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistRun, true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistRun);
            }

            if (reg.GetValue(RegistWin32ApiExe) != null)
            {
                return true;
            }
            return false;
        }


        public static void ClearStartWithWindows()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistRun, true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistRun);
            }

            if (reg.GetValue(RegistWin32ApiExe) != null)
            {
                reg.DeleteValue(RegistWin32ApiExe);
            }
        }






    }

}
