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
    public Text amoSecond;


    public void setNowGun(int i){
        var tmpInd = Math.Abs(i) % gamesIcons.Count ;
        foreach(var tmpIc in gamesIcons){
            tmpIc.SetActive(false);
        }
        gamesIcons[tmpInd].SetActive(true);
    }

    public void setAmoNow(int now, int max, bool infinityNow=false, int nowSecond = -1, int maxSecond = -1){
        if(infinityNow){
            amo.gameObject.SetActive(false);
            if(nowSecond != -1){
                amoSecond.gameObject.SetActive(false);
            }
            IconOfInfinityAmo.SetActive(true);
        }else{
            amo.gameObject.SetActive(true);
            if(nowSecond != -1){
                amoSecond.gameObject.SetActive(true);
            }
            IconOfInfinityAmo.SetActive(false);
        }
        amo.text = now.ToString()+"/"+max.ToString();
        if(nowSecond != -1){
            amoSecond.text = nowSecond.ToString()+"/"+maxSecond.ToString();
        }else{
            amoSecond.gameObject.SetActive(false);
        }

    }





    void Start()  { }
    void Update()   { }


    public override void setValueOfThisUI(float val)  { 
        // throw new System.NotImplementedException(); 
    }
}
