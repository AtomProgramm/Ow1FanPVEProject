using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleProjectileBullet : Projectile
{
    public new bool destroySelfAfterHit = true;
    public new float sizeOfDamage = 1;
    public new float speed = 1;
    public new float maxTimeAlive = 60 * 15;

    void OnCollisionEnter(Collision other)
    {
        var tmpEnemy = other.gameObject.GetComponent<Enemy>();
        if(tmpEnemy != null){
            hitEnemyProcess(tmpEnemy);
        }
    }
}
