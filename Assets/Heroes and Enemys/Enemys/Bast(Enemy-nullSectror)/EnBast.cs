using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnBast : MonoBehaviour, Enemy
{
    [Space(5)]
    public float canSeeRange = 200;
    public float seeAngle = 360;
    public float shootingRange;
    public Boolean canSeeThrowWalls;
    [Space(5)]
    public Boolean getToTargetWhoHit;
    public Boolean momentumForgetTheTarget;
    public Boolean haveTimeToForget;
    public float timeToForget;
    [Space(5)]
    public GameObject prefabOfBullet;
    // public float sizeOfDamage;
    public GameObject pointToSpawnBullet;
    public float timeBetweenShoot = 0.5f;
    private float timerBetweenShoot = -1;
    [Space(5)]
    public float maxHealth = 100;
    public float health = 100;
    [Space(5)]
    public GameObject deadEffect;
    public GameObject shootEffect;



    float timerToForget;

    private Transform nowTarget = null;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Collider thisCollider;
    private Animator nowAnimator;

    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        thisCollider = GetComponent<Collider>();
        nowAnimator = GetComponent<Animator>();

        // MissionController.instance.enemyOnMission.Add(this);
        timerToForget = timeToForget;
        regSelfSummon();
    }

    void FixedUpdate()
    {
        var tmpTarget = getTarget();
        if((tmpTarget == null) && (nowTarget != null)){
            if(momentumForgetTheTarget){
                nowTarget = null;
            }else if(haveTimeToForget){
                if(timerToForget < 0){
                    timerToForget = timeToForget;
                    nowTarget = null;
                }else{
                    timerToForget = timerToForget - Time.fixedDeltaTime;
                }
            }

        }else{
            nowTarget = tmpTarget;
        }
    }

    void Update()
    {
        if(nowTarget != null){
            navAgent.SetDestination(nowTarget.transform.position);
            nowAnimator.SetBool("walk", true);
            navAgent.isStopped = false;
            if(Vector3.Distance(transform.position, nowTarget.transform.position) < shootingRange){
                navAgent.SetDestination(nowTarget.transform.position);
                nowAnimator.SetBool("shooting", true);
                processShooting();
            }else{
                nowAnimator.SetBool("shooting", false);
            }
        }else{
            nowAnimator.SetBool("walk", false);
            nowAnimator.SetBool("shooting", false);
            navAgent.isStopped = true;
        }
    }

    // bool processShooting(){
    void processShooting(){
        if(timerBetweenShoot <= 0){
            Instantiate(prefabOfBullet, pointToSpawnBullet.transform.position, pointToSpawnBullet.transform.rotation);
            Instantiate(shootEffect, pointToSpawnBullet.transform);
            timerBetweenShoot = timeBetweenShoot;
        }
        timerBetweenShoot = timerBetweenShoot - Time.deltaTime;
    }

    public void tookDamage(float damageSize, HeroBehaviors fromWho = null){
        if(getToTargetWhoHit && (fromWho != null)){
            nowTarget = fromWho.transform;
        }
        health = health - damageSize;
        // nowAnimator.SetBool("tookDamage", false);
        if(health < 0){
            playDead();
        }
    }
    public void tookHeal(float healSize){
        health = Math.Min(maxHealth ,health + healSize);
    }

    Transform getTarget(){
        var tmpCan = MissionController.instance.playersOnMission.FindAll(item => (Vector3.Angle((item.transform.position - transform.position), transform.forward) < seeAngle));
        tmpCan.Sort((it1, it2)=>Vector3.Distance(it1.transform.position, transform.position).CompareTo(Vector3.Distance(it2.transform.position, transform.position)));
        foreach(var nowPlayer in tmpCan){
            if(Vector3.Distance(nowPlayer.transform.position, transform.position) > canSeeRange){
                break;
            }
            if(canSeeThrowWalls){
                return nowPlayer.transform;
            }else{
                RaycastHit  hit;
                Vector3 dir = nowPlayer.transform.position - transform.position;
                Vector3 from = transform.position + (Vector3.up * 0.5f);
                // Vector3 from = transform.position + thisCollider.bounds.;
                // Vector3 from = transform.position;
                Debug.DrawRay(from, dir,Color.blue,2);
                if(Physics.Raycast(from, dir, out hit)){ 
                    if (hit.transform.GetComponent<Player>() != null){
                        return nowPlayer.transform;
                    }
                }
            }
        }
        return null;
    }

    public void playDead()
    {
        regSelfDel();
        Destroy(Instantiate(deadEffect,transform.position,transform.rotation), 4);
        Destroy(gameObject);
    }

    public void regSelfSummon()
    {
        foreach(var i in EnemyStateObserver.instances){
            i.allEnemy.Add(gameObject);
        }
    }

    public void regSelfDel()
    {
        foreach(var i in EnemyStateObserver.instances){
            i.allEnemy.Remove(gameObject);
        }
    }

    // public Vector3 getSize()
    // {
    //     return 
    // }
}
