using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public UIItem uiSet;
    [Space(10)]
    public int countToUse = 1;
    public float coolDown;


    protected float coolDownTimer;
    protected bool coolDownNow;



    public abstract void execute();


    // void Start(){}
    // void Update(){countCoolDownIfItIs();}

//todo: type call this method on all already implemented child class
    public void countCoolDownIfItIs(){
        if(coolDownNow){
            coolDownTimer = coolDownTimer - Time.deltaTime;
            if(coolDownTimer < 0){
                coolDownNow = false;
                coolDownTimer  = coolDown;
            } 
        }
    }
    public void setNowCoolDown(){
        coolDownNow = true;
        coolDownTimer  = coolDown;
    }
}
