using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test2 : MonoBehaviour
{
    public Text text; 
    // Start is called before the first frame update
    void Start()
    {
        text.text = Sxer.Tool.WindowStyle.WindowHelper.GetSystemResolution().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDestroy()
    {
        Sxer.Tool.WindowStyle.WindowHelper.ClearRegisterAll();
    }
}
