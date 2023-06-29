using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ultimateUI : UIItem
{
    public GameObject imageFullCharge;
    // public TMPro.TMP_Text num;
    public Text num;
    public Slider chargeCircle;
    public float timeToChange = 1;

    private float valNow;
    private float valNew;

    void Update() { 
        updateValOnFrame();
    }

     
    public override void setValueOfThisUI(float val){
        valNew = val;
    }

    public void updateValOnFrame(){
        float step = (valNew - valNow) / timeToChange;
        valNow = valNow + (step * Time.deltaTime);
        valNow = Math.Max(0, valNow);
        if(valNow >= 100){
            imageFullCharge.SetActive(true);
            num.gameObject.SetActive(false);
            chargeCircle.gameObject.SetActive(false);
        }else{
            // num.SetText(((int)Math.Round(valNow)).ToString());
            num.text = (((int)Math.Round(valNow)).ToString());

            chargeCircle.value = valNow / 100;
        }
    }
}
