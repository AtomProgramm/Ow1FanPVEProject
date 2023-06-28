using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsContainer : MonoBehaviour
{
    public static SettingsContainer instanceOfSettingContainer;



    public KeyCode jump = KeyCode.Space;


    public KeyCode ultimate = KeyCode.Q;

    public KeyCode ability1 = KeyCode.E;
    public KeyCode ability2 = KeyCode.LeftShift;


    // public KeyCode gunChange = KeyCode;
    // public KeyCode gunFirst = KeyCode;
    // public KeyCode gunSecond = KeyCode;


    void Start()
    {
        instanceOfSettingContainer = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
