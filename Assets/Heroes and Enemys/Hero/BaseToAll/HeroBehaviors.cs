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
    public float speed;


    
    [Space(20)]
    public List<GameObject> gunsObjects;
    public List<Gun> scriptOfGunPrefab;
    public int nowWeaponIndex;


    [Space(20)]
    public UltimateAbility ultimate;
    public Ability abilityA;
    public Ability abilityB;
    public Ability abilityC;
    public List<Ability> passiveAbilities;


    [Space(20)]
    public UIGuns uiOfGuns;


    [Space(20)]
    public float maxHealth;



    void Start() {}
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
        foreach(var i in gunsObjects){
            i.SetActive(false);
        }
        gunsObjects[Math.Abs(nowWeaponIndex) % gunsObjects.Count].SetActive(true);
        uiOfGuns.setNowGun(Math.Abs(nowWeaponIndex));
    }

    public void chargeUltimate(float size){
        ultimate.chargeUltimate(size);
    }
}
