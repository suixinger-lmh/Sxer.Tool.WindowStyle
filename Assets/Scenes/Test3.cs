using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sxer.Tool.WindowStyle;
public class Test3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WindowHelper.CreateSystemTray(WindowHelper.CustomTrayIcon("C:/Users/DS/Desktop/1/Sxer.jpg", 100,100));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
