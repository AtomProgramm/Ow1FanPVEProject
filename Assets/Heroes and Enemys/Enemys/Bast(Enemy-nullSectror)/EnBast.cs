using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnBast : MonoBehaviour, Enemy
{
    [Space(5)]
    public float canSeeRange = 200;
    public float seeAngle;
    public float shootingRange;
    public Boolean canSeeThrowWalls;
    [Space(5)]
    public float health = 100;
    [Space(10)]
    public List<FirstPersonController> allPlayers;
    public FirstPersonController nowTarget;
    public UnityEngine.AI.NavMeshAgent navAgent;
    public Collider nowCollider;
    public Animator nowAnimator;

    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nowCollider = GetComponent<Collider>();
        nowAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, nowTarget.transform.position) < canSeeRange){
            navAgent.SetDestination(nowTarget.transform.position);
            nowAnimator.SetBool("walk", true);
            if(Vector3.Distance(transform.position, nowTarget.transform.position) < shootingRange){
                navAgent.SetDestination(nowTarget.transform.position);
                nowAnimator.SetBool("shotting", true);
            }else{
                nowAnimator.SetBool("shotting", false);
            }
        }else{
            nowAnimator.SetBool("walk", false);
                navAgent.SetDestination(transform.position);
        }
    }
}
