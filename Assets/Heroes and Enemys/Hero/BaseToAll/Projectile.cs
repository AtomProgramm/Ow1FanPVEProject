using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public HittableEntity.damage damage;
    public bool destroySelfAfterHit = true;
    public float ultimateChargeSize = 1f;
    public float speed = 1;
    public float maxTimeAlive = 60 * 3;
    
    void Start()
    {
        Destroy(gameObject, maxTimeAlive);
    }
    
    void Update()
    {
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
    }
    public void hitProcess(HittableEntity target){
        target.tookDamage(damage);
        if(damage.owner.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player){
            ((Player)damage.owner).doPerHitEnemy(damage);
            ((Player)damage.owner).hero.chargeUltimate(ultimateChargeSize);
        }
        tryDestroySelfAfterHit();
    }
    public void tryDestroySelfAfterHit(){
        if(destroySelfAfterHit){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        var hittableObj = other.gameObject.GetComponent<HittableEntity>();
        if(hittableObj != null){
            if(damage.canDamageOnly == HittableEntity.damage.CanDamageOnly.all){
                hitProcess(hittableObj);
            }else if((damage.canDamageOnly == HittableEntity.damage.CanDamageOnly.allNotPlayerAndNotEnemy) && (hittableObj.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.other)){
                hitProcess(hittableObj);
            }else if((damage.canDamageOnly == HittableEntity.damage.CanDamageOnly.enemy) && (hittableObj.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.enemy)){
                hitProcess(hittableObj);
            }else if((damage.canDamageOnly == HittableEntity.damage.CanDamageOnly.player) && (hittableObj.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player)){
                hitProcess(hittableObj);
            }
        }
    }
}