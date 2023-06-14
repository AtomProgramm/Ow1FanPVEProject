using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleProjectileBullet : MonoBehaviour
{
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

    void OnCollisionEnter(Collision other)
    {
        var tmpEnemy = other.gameObject.GetComponent<Enemy>();
        if(tmpEnemy != null){
            tmpEnemy.tookDamage(sizeOfDamage);
            Debug.Log("Ha got damage ");
        }
        Debug.Log("Collision");
        Destroy(gameObject);
    }
}
