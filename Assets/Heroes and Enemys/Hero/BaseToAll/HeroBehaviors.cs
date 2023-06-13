using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroBehaviors : MonoBehaviour
{
    [Space(5)]
    public Vector3 size;

    
    [Space(20)]
    public float speed;


    
    [Space(20)]
    public GameObject gunPrefab;
    public Gun scriptOfGunPrefab;


    [Space(20)]
    public Ability ultimate;
    [Space(8)]
    public Ability abilityA;
    [Space(5)]
    public Ability abilityB;
    [Space(5)]
    public Ability abilityC;
    [Space(5)]
    public Ability abilityD;


    [Space(20)]
    public GameObject uiOf;


    [Space(20)]
    public float maxHealth;



    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
