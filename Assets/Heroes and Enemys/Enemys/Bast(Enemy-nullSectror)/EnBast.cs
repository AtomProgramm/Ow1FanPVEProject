using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

// solve/choose - 1 need amo counting and reload system?   2 how impl, when and where need get cover ?(class ShelterToMobs)   3 Is it necessary and what behavior will the state of the minigun turret have?
[RequireComponent(typeof(ProjectileFactory))]
public class EnBast : Enemy
{
    [Header("Choice target settings")]
    public float canSeeRange = 200;
    public float seeAngle = 360;
    public bool canSeeThrowWalls;
    public bool getToTargetWhoHit;
    public bool momentumForgetTheTarget;
    public bool haveTimeToForget;
    public float timeToForget;


    [Header("Shooting system")]
    public float shootingRange;
    public GameObject prefabOfBullet;
    public ProjectileFactory projectileFactory;
    public GameObject pointToSpawnBullet;
    public float timeBetweenShoot = 0.5f;
    private float timerBetweenShoot = -1;
    public Transform boneTarget ;
    
    
    [Header("Effects")]
    public GameObject deadEffect;
    public GameObject shootEffect;




    private float timerToForgetting;

    private Transform nowPlTarget = null;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Collider thisCollider;
    private Animator nowAnimator;






    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        thisCollider = GetComponent<Collider>();
        nowAnimator = GetComponent<Animator>();
        projectileFactory = GetComponent<ProjectileFactory>();
        projectileFactory.bulletPrefab = prefabOfBullet;
        projectileFactory.owner = this;

        
        if(MissionController.instance != null) MissionController.instance.enemyOnMission.Add(this);
        timerToForgetting = timeToForget;
        regSelfSummon();
        initOnStart();
    }

    void FixedUpdate()
    {
        var tmpPlTarget = findPlayerTarget();
        if((tmpPlTarget == null) && (nowPlTarget != null)){
            if(momentumForgetTheTarget){
                nowPlTarget = null;
            }else if(haveTimeToForget){
                if(timerToForgetting < 0){
                    timerToForgetting = timeToForget;
                    nowPlTarget = null;
                }else{
                    timerToForgetting = timerToForgetting - Time.fixedDeltaTime;
                }
            }

        }else{
            nowPlTarget = tmpPlTarget;
        }
    }

    void Update()
    {
        if(nowPlTarget != null){
            boneTarget.transform.position  = nowPlTarget.transform.position;
            navAgent.SetDestination(nowPlTarget.transform.position);
            nowAnimator.SetBool("walk", true);
            navAgent.isStopped = false;
            if(Vector3.Distance(transform.position, nowPlTarget.transform.position) < shootingRange){
                navAgent.SetDestination(nowPlTarget.transform.position);
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
    void processShooting(){
        if(timerBetweenShoot <= 0){
            var tmp = pointToSpawnBullet.transform.rotation;
            pointToSpawnBullet.transform.LookAt(nowPlTarget);
            pointToSpawnBullet.transform.rotation = Quaternion.Slerp(tmp, pointToSpawnBullet.transform.rotation, 0.5f); 
            projectileFactory.doInst(pointToSpawnBullet.transform.position, pointToSpawnBullet.transform.rotation);
            Instantiate(shootEffect, pointToSpawnBullet.transform);
            timerBetweenShoot = timeBetweenShoot;
        }
        timerBetweenShoot = timerBetweenShoot - Time.deltaTime;
    }
    Transform findPlayerTarget(){
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
            nowPlTarget = damageIn.owner.transform;
        }
    }
    public override float calculateTheAmountOfHeal(float healIn)
    {
        return healIn;
    }
    public override void playEffectsOnHeal(float healIn){}
    
}
