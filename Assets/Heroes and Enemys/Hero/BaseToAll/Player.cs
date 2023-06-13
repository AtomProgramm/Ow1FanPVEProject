using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HeroBehaviors hero; // tmp
    public float hp = 100f;
    public Canvas uiCanvas;
    public FirstPersonController FPSController = null;
    
    [Space(5)]
    public bool injured;

    void Start()
    {
        FPSController = GetComponent<FirstPersonController>();
        uiCanvas = GetComponentInChildren<Canvas>();
    }

    void Update(){}

    public void playWon(){

    }
    public void playDefeat(){

    }
}
