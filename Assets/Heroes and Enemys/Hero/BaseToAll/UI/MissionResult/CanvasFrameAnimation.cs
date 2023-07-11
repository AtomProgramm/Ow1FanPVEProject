using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CanvasFrameAnimation : MonoBehaviour
{
    public Image toShow;
    public List<Sprite> frames;
    public int framesPerSecond = 25;
    [Space(5)]
    public UnityAction doOnEndAnimation;

    private IEnumerator nowAnimation = null;
    
    IEnumerator doAnimation(){
        toShow.enabled = true;
        toShow.color = Color.white;
        foreach(var nowFrame in frames){
            toShow.sprite = nowFrame;
            yield return new WaitForSeconds(1f / (float)framesPerSecond);
        }
        nowAnimation = null;
        toShow.enabled = false;
        toShow.color = new Color(0,0,0,1);
    }
    public Boolean tryStartAnimation(){
        if(nowAnimation == null){
            nowAnimation = doAnimation();
            StartCoroutine(nowAnimation);
            return true;
        }else{
            return false;
        }
    }

    void Start() {}
    void Update() {}
}
