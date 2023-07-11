using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBullet : MonoBehaviour
{
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
        pl.tookDamage(sizeOfDamage);
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