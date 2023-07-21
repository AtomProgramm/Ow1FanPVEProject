using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HittableEntity : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public bool injured; // ?

    public abstract void initOnStart();


    public abstract float calculateTheAmountOfDamage(float damageIn);
    public abstract void playEffectsOnDamage(float damageIn);
    public void tookDamage(float damageSize){
        damageSize = calculateTheAmountOfDamage(damageSize);
        playEffectsOnDamage(damageSize);
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
