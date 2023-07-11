using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIGuns : UIItem
{
    public List<GameObject> gamesIcons;
    public GameObject IconOfInfinityAmo;
    public Text amo;


    public void setNowGun(int i){
        var tmpInd = Math.Abs(i) % gamesIcons.Count ;
        foreach(var tmpIc in gamesIcons){
            tmpIc.SetActive(false);
        }
        gamesIcons[tmpInd].SetActive(true);
    }

    public void setAmoNow(int now, int max, bool infinityNow=false){
        if(infinityNow){
            amo.gameObject.SetActive(false);
            IconOfInfinityAmo.SetActive(true);
        }else{
            amo.gameObject.SetActive(true);
            IconOfInfinityAmo.SetActive(false);
        }
        amo.text = now.ToString()+"/"+max.ToString();

    }





    void Start()  { }
    void Update()   { }


    public override void setValueOfThisUI(float val)  { 
        // throw new System.NotImplementedException(); 
    }
}
