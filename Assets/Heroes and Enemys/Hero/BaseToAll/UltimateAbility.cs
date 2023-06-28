using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UltimateAbility : Ability
{
    public float nowUltimateCharge = 0f;
    

    public void ultimateExecute(){
        if(nowUltimateCharge >= 100f){
            nowUltimateCharge = 0;
            execute();
        }
    }
    public void chargeUltimate(float size){
        nowUltimateCharge = nowUltimateCharge + size;
    }
}
