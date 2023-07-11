using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public HeroBehaviors owner;
    public bool destroySelfAfterHit = true;
    public float ultimateChargeSize = 1f;
    public float sizeOfDamage = 1;
    public float speed = 1;
    public float maxTimeAlive = 60 * 15;
    
    void Start()
    {
        Destroy(gameObject, maxTimeAlive);
    }
    
    void Update()
    {
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
    }
    public void hitEnemyProcess(Enemy enemy){
        enemy.tookDamage(sizeOfDamage, owner);
        if(owner != null){
            owner.chargeUltimate(ultimateChargeSize);
        }
        tryDestroySelfAfterHit();
    }
    public void tryDestroySelfAfterHit(){
        if(destroySelfAfterHit){
            Destroy(gameObject);
        }
    }
}
