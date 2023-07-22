using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HittableEntity : MonoBehaviour
{
        // to killField if it gone be
    public virtual String nameEntity{get;}  
    public virtual Sprite icon{get;set;}  

    public float maxHp;
    public float hp;
    public bool injured; // ?


    public class damage{
        public float damageSize;
        public HittableEntity owner;
        public Player playerOwner = null;
    }




    public abstract void initOnStart();


    public abstract float calculateTheAmountOfDamage(damage damageIn);
    public abstract void playEffectsOnDamage(damage damageIn);
    public void tookDamage(damage damage){
        float damageSize = calculateTheAmountOfDamage(damage);
        playEffectsOnDamage(damage);
        hp = hp - damageSize;
        if(hp < 0){
            playInjure();
        }
    }

    public abstract float calculateTheAmountOfHeal(float healIn);
    public abstract void playEffectsOnHeal(float healIn);
    public void tookHeal(float healSize){
        healSize = calculateTheAmountOfHeal(healSize);
        playEffectsOnHeal(healSize);
        hp = Mathf.Min(maxHp, hp + healSize);
    }

    public abstract void playInjure();

}