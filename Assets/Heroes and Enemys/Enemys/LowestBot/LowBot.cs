using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(ProjectileFactory))]
public class LowBot : Enemy
{
    [Header("Choice target settings")]
    public float canSeeRange = 250;
    public float seeAngle = 30;
    public bool canSeeThrowWalls  = false;
    public bool getToTargetWhoHit = true;
    public bool momentumForgetTheTarget = false;
    public bool haveTimeToForget = true;
    public float timeToForget = 5;


    [Header("Shooting system")]
    public float shootingRange = 80f;
    public GameObject prefabOfBullet;
    public GameObject pointToSpawnBullet;
    public bool useAnimationEventNotTimer = true;
    [DescriptionAttribute("timer between do shoot if 'useAnimationEventNotTimer' == false")]
    public float timeBetweenShoot = 0.5f;
    public ProjectileFactory projectileFactory;
    // [Range(0,1f)]
    // public float weightToTurnSpineBone = 0.5f;
    // [Range(0,1f)]
    // public float weightToTurnGunHandBone = 0.5f;
    public Transform boneTarget ;
    
    
    [Header("Effects")]
    public GameObject deadEffect;
    public GameObject shootEffect;




    private float timerToForgetting;

    private Transform nowTarget = null;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Collider thisCollider;
    private Animator nowAnimator;
    private float timerBetweenShoot = -1;






    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        thisCollider = GetComponent<Collider>();
        nowAnimator = GetComponent<Animator>();
        projectileFactory = GetComponent<ProjectileFactory>();
        projectileFactory.bulletPrefab = prefabOfBullet;
        projectileFactory.owner = this;

        MissionController.instance.enemyOnMission.Add(this);
        timerToForgetting = timeToForget;
        regSelfSummon();
        initOnStart();
    }

    void FixedUpdate()
    {
        var tmpTarget = getTarget();
        if((tmpTarget == null) && (nowTarget != null)){
            if(momentumForgetTheTarget){
                nowTarget = null;
            }else if(haveTimeToForget){
                if(timerToForgetting < 0){
                    timerToForgetting = timeToForget;
                    nowTarget = null;
                }else{
                    timerToForgetting = timerToForgetting - Time.fixedDeltaTime;
                }
            }

        }else{
            nowTarget = tmpTarget;
        }
    }

    void Update()
    {
        if(nowTarget != null){
            boneTarget.transform.position  = nowTarget.transform.position;
            navAgent.SetDestination(nowTarget.transform.position);
            nowAnimator.SetBool("move", true);
            navAgent.isStopped = false;
            if(Vector3.Distance(transform.position, nowTarget.transform.position) < shootingRange){
                navAgent.SetDestination(nowTarget.transform.position);
                nowAnimator.SetBool("Shooting", true);
                if(! useAnimationEventNotTimer) processShooting();
            }else{
                nowAnimator.SetBool("Shooting", false);
            }
        }else{
            nowAnimator.SetBool("move", false);
            nowAnimator.SetBool("Shooting", false);
            navAgent.isStopped = true;
        }
    }
    void processShooting(){ // ! AnimEvent or EveryFrame -- call animation event and instant do shot     or    calling per frame to counting time and od shoot 
        if(timerBetweenShoot <= 0 || useAnimationEventNotTimer){
            pointToSpawnBullet.transform.LookAt(nowTarget);
            projectileFactory.doInst(pointToSpawnBullet.transform.position, pointToSpawnBullet.transform.rotation);
            if(shootEffect != null) Instantiate(shootEffect, pointToSpawnBullet.transform);
            timerBetweenShoot = timeBetweenShoot;
        }
        timerBetweenShoot = timerBetweenShoot - Time.deltaTime;
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
                if(Physics.Raycast(from, dir, out hit)){ 
                    if (hit.transform.GetComponent<Player>() != null){
                        return nowPlayer.transform;
                    }
                }
            }
        }
        return null;
    }




    public override void playInjure()
    {
        if(lastBittedBy != null){
            StatsController.inst.lastMatchKills = StatsController.inst.lastMatchKills + 1;
            StatsController.inst.saveValues();
        }
        regSelfDel();
        Destroy(Instantiate(deadEffect,transform.position,transform.rotation), 4);
        Destroy(gameObject);
    }

    public override void regSelfSummon()
    {
        if(EnemyStateObserver.instances != null){
        foreach(var i in EnemyStateObserver.instances){
            i.allEnemy.Add(gameObject);
        }}
    }
    public override void regSelfDel()
    {
        if(EnemyStateObserver.instances != null){
        foreach(var i in EnemyStateObserver.instances){
            i.allEnemy.Remove(gameObject);
        }}
        projectileFactory.destroyFactory();
    }


    public override void initOnStart(){
    }
    public override float calculateTheAmountOfDamage(damage damageIn)
    {
        return damageIn.damageSize;
    }
    public override void playEffectsOnDamage(damage damageIn){
        lastBittedBy = damageIn.owner;
        if(getToTargetWhoHit){
            nowTarget = damageIn.owner.transform;
        }
    }

    public override float calculateTheAmountOfHeal(float healIn)
    {
        return healIn;
    }
    public override void playEffectsOnHeal(float healIn){}
    
}
