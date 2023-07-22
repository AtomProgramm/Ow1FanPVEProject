using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBullet : MonoBehaviour
{
    HittableEntity ownerOfBullet;
    public bool destroySelfAfterHit = true;
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
    public void hitHeroProcess(Player pl){
        HittableEntity.damage damage = new HittableEntity.damage();
        damage.damageSize = sizeOfDamage;  
        damage.owner = ownerOfBullet;
        pl.tookDamage(damage);
        tryDestroySelfAfterHit();
    }
    public void tryDestroySelfAfterHit(){
        if(destroySelfAfterHit){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        var playerToSendDamage = other.gameObject.GetComponent<Player>();
        if(playerToSendDamage != null){
            hitHeroProcess(playerToSendDamage);
        }
    }
}