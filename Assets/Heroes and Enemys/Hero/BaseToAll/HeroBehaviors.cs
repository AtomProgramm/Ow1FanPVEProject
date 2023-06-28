using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroBehaviors : MonoBehaviour
{
    [Space(5)]
    public Boolean itPlayable;



    [Space(20)]
    public Vector3 size;

    
    [Space(20)]
    public float speed;


    
    [Space(20)]
    public List<GameObject> gunPrefab;
    public List<Gun> scriptOfGunPrefab;
    public int nowWeaponIndex;


    [Space(20)]
    public UltimateAbility ultimate;
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




    void Start()  { }
    void Update() { 
        if(itPlayable){
            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                nowWeaponIndex = nowWeaponIndex + 1;
                changeWeapon();
            }else if(Input.GetAxis("Mouse ScrollWheel") < 0){
                nowWeaponIndex = nowWeaponIndex -1;
                changeWeapon();
            }
        }
    }

    void changeWeapon(){
        foreach(var i in gunPrefab){
            i.SetActive(false);
        }
        gunPrefab[Math.Abs(nowWeaponIndex) % gunPrefab.Count].SetActive(true);
    }

    public void chargeUltimate(float size){
        ultimate.chargeUltimate(size);
    }
}
