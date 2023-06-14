using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{

    public void playDead();
    public void tookDamage(float damageSize);
    public void tookHeal(float healSize);
}
