using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HeroBehaviors hero; 
    public float damageModifier = 1f;
    public Canvas uiCanvas;
    public FirstPersonController FPSController = null;

    [Space(5)]
    public CanvasFrameAnimation plDefeat;
    public CanvasFrameAnimation plVictory;
    [Space(5)]
    public bool injured;

    void Start()
    {
        FPSController = GetComponent<FirstPersonController>();
        uiCanvas = GetComponentInChildren<Canvas>();
        //Tmp
        // if(MissionController.instance.playersOnMission == null)MissionController.instance.playersOnMission = new List<Player>();
        // if(!MissionController.instance.playersOnMission.Contains(this)) MissionController.instance.playersOnMission.Add(this);
    }

    void Update(){}


    public void tookDamage(float damageSize){
        hero.hp = hero.hp - damageSize;
        // nowAnimator.SetBool("tookDamage", false);
        if(hero.hp < 0){
            playInjure();
        }

    }
    public void tookHeal(float healSize){
        hero.hp = Math.Min(hero.maxHealth ,hero.hp + healSize);
    }

    public void playWon(){
        plVictory.tryStartAnimation();
        void won(){
            // change scene and other...
        }    
        plVictory.doOnEndAnimation += won;  
    }
    public void playDefeat(){ 
        plDefeat.tryStartAnimation();
        void defeat(){
            // change scene and other...
        }  
        plDefeat.doOnEndAnimation += defeat;  
    }
    public void playInjure(){ 
        injured = true;
        FPSController.enabled = false;

        playDefeat();
    }
}
