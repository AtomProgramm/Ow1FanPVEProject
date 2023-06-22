using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HeroBehaviors hero; // tmp
    public float hp = 100f;
    public float damageModifier = 1f;
    public Canvas uiCanvas;
    public FirstPersonController FPSController = null;
    
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

    void Update(){ }


    public void tookDamage(float damageSize){
        hp = hp - damageSize;
        // nowAnimator.SetBool("tookDamage", false);
        if(hp < 0){
            playInjure();
        }

    }
    public void tookHeal(float healSize){
        hp = Math.Min(hero.maxHealth ,hp + healSize);
    }

    public void playWon(){ }
    public void playDefeat(){ }
    public void playInjure(){ }
}
