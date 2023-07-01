using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICooldownAbility : UIItem
{
    public float minSizeTextInAnimation = 0.7f;
    // public float standardTextInSize = 25;
    // public float minSizeTextInAnimation = 24;

    public GameObject imageHaveAbility;
    public GameObject imageNowColdown;
    public Text nowColdown;

    public override void setValueOfThisUI(float val)
    {
        if(val > 0){
            nowColdown.gameObject.SetActive(true);
            imageHaveAbility.SetActive(false);
            imageNowColdown.SetActive(true);

            int tmpWhole = (int)val;
            nowColdown.text = (tmpWhole.ToString());
            // nowColdown.transform.localScale =  Vector3.Lerp(Vector3.one, Vector3.one * minSizeTextInAnimation, val - tmpWhole);
            // nowColdown.fontSize = Mathf.Lerp(minSizeTextInAnimation,standardTextInSize,  val - tmpWhole);
            nowColdown.rectTransform.localScale =  Vector3.Lerp(Vector3.one * minSizeTextInAnimation,Vector3.one , val - tmpWhole);
        }else{
            nowColdown.gameObject.SetActive(false);
            imageHaveAbility.SetActive(true);
            imageNowColdown.SetActive(false);
        }
    }


    void Start(){}
    void Update() {}
}
