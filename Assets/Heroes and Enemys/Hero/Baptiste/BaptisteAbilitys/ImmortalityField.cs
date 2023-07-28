using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class ImmortalityField : HittableEntity
{
    public SphereCollider field;
    public Rigidbody r;

    public float timeToAlive = 5f;
    public float fieldRadius = 6.5f;
    public float forwardSpeed = 2f;
    public float partToHoldHp = 0.25f;

    public float timeToStarting = 0.5f;
    public float heightOnStart = 2f;

    private bool nowDoing;
    private List<HittableEntity> toHoldHp = new List<HittableEntity>();
    IEnumerator startDoing = null;

    void Start()
    {
        field = GetComponent<SphereCollider>();
        field.isTrigger = true;
        field.enabled = false;
        field.radius = fieldRadius;
        r = GetComponent<Rigidbody>();
        r.velocity  = transform.forward * forwardSpeed;
    }

    void Update()
    {
        if(nowDoing){
            foreach(var toHoldOne in toHoldHp){
                toHoldOne.hp = Mathf.Max(toHoldOne.hp , toHoldOne.maxHp * partToHoldHp);
            }
        }else{
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            // Debug.DrawRay(ray.origin, ray.direction, Color.cyan,1);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction, Color.cyan,1);
            if(Physics.Raycast(ray, out hit, 1 )){
                StartCoroutine(st(hit.point));
            }
        }

    }




    IEnumerator st(Vector3 floorPosition){
        float timer = 0;
        transform.rotation = Quaternion.identity;
        while (timer < timeToStarting){
            timer = timer + Time.deltaTime;
            transform.position = floorPosition + (Vector3.up * Mathf.Lerp(0, heightOnStart, (timer / timeToStarting)));
            hp = maxHp;
            yield return null;
        }
        nowDoing = true;
        // field.isTrigger = true;
        field.enabled = true;
        // Destroy(r);
        r.velocity = Vector3.zero;
        r.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject, timeToAlive);
    }





    void OnTriggerEnter(Collider other) {
        var he = other.GetComponent<HittableEntity>() ;
        if(he != null){
            if(he.typeOfHittableEntity == TypeOfHittableEntity.player){
                toHoldHp.Add(he);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        toHoldHp.RemoveAll(item => (item==null));
        var tmp = toHoldHp.FindAll(item => (item.gameObject==other.gameObject));
        if(tmp.Count > 0){
            toHoldHp.Remove(tmp[0]);
        }
    }







    
    public override float calculateTheAmountOfDamage(damage damageIn) {  return damageIn.damageSize; }
    public override float calculateTheAmountOfHeal(float healIn)  { return healIn; }

    public override void initOnStart() { }

    public override void playEffectsOnDamage(damage damageIn) { }

    public override void playEffectsOnHeal(float healIn) { }

    public override void playInjure()
    {
        Destroy(gameObject);
    }

}
