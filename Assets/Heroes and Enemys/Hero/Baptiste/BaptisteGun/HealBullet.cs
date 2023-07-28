using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class HealBullet : MonoBehaviour
{
    public HittableEntity owner;
    public GameObject effectHit;
    public float healSize = 1;
    public float forwardSpeed = 1;
    public float downSpeed = 1;
    public float radius = 3;
    
    public UnityAction<HealBullet> doOnHit;


    private Rigidbody b;
    private float accDown = 0;
    
    void Start()
    {
        b = GetComponent<Rigidbody>();
    }
    void Update() {
        // b.velocity = b.velocity + (transform.forward * forwardSpeed * Time.deltaTime) + (Vector3.down * downSpeed * Time.deltaTime);
        transform.position = transform.position + (transform.forward * forwardSpeed * Time.deltaTime) + (Vector3.down * downSpeed * accDown * Time.deltaTime);
        accDown = accDown + Time.deltaTime;
    }


    void OnCollisionEnter(Collision other)
    {
        accDown = 0;
        // print("OnCollisionEnter");
        Instantiate(effectHit, transform.position,transform.rotation);
        if(other.contacts.Length > 0){
            var hist = Physics.OverlapSphere(other.contacts[0].point, radius);
            foreach(var i in hist){
                if(owner.gameObject == i.gameObject){
                    continue;
                }
                var hi = i.gameObject.GetComponent<HittableEntity>();
                if(hi != null){
                    if(hi.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player){
                        hi.tookHeal(healSize);
                    }
                }
            }
        }
        doOnHit.Invoke(this);
    }
}
