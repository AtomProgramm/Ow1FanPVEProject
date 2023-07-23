using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HostageGrabController))] 
[RequireComponent(typeof(Collider))] 
public class Player : HittableEntity
{

    // not work :(
        // [HideInInspector]
        // public new float maxHp = 200; // override only to hide in inspector
    [Space(5)]
    public HeroBehaviors hero; 

    [Space(5)]
    public float damageModifier = 1f;
    public FirstPersonController FPSController = null;


    [Header("UI")]
    public UIHeroState uiHealth; // *uiOf*? tmp only hp bar
    public Canvas uiCanvas;
    // [Header("UI animation")]
    public CanvasFrameAnimation plDefeat;
    public CanvasFrameAnimation plVictory;
    



    void Start()
    {
        typeOfHittableEntity = HittableEntity.TypeOfHittableEntity.player;
        initOnStart();
        FPSController = GetComponent<FirstPersonController>();
        uiCanvas = GetComponentInChildren<Canvas>();
        // if(MissionController.instance.playersOnMission == null)MissionController.instance.playersOnMission = new List<Player>();
        // if(!MissionController.instance.playersOnMission.Contains(this)) MissionController.instance.playersOnMission.Add(this);
    }
    void Update() {
        uiHealth.setHealth(hp, maxHp);    
    }





    public void playWon(){
        void won(){
            // change scene and other...
            SceneManager.LoadScene("MissionResult", LoadSceneMode.Single);//tmp to test
        }    
        plVictory.doOnEndAnimation += won;  
        plVictory.tryStartAnimation();
    }
    public void playDefeat(){ 
        void defeat(){
            // change scene and other...
            SceneManager.LoadScene("MissionResult", LoadSceneMode.Single);//tmp to test
        }  
        plDefeat.doOnEndAnimation += defeat;  
        plDefeat.tryStartAnimation();
    }





    public override void initOnStart()
    {
        hp = hero.maxHealth;
        maxHp = hero.maxHealth;
    }

    public override float calculateTheAmountOfDamage(damage damageIn)
    {
        return damageIn.damageSize;
    }

    public override void playEffectsOnDamage(damage damageIn) {}




    public override float calculateTheAmountOfHeal(float healIn)
    {
        return healIn;
    }

    public override void playEffectsOnHeal(float healIn){}

    public override void playInjure()
    {   if(!injured){
            StatsController.inst.lastMatchDeath = StatsController.inst.lastMatchDeath + 1;
            StatsController.inst.saveValues();
            injured = true;
            FPSController.enabled = false;
        }
    }


    public void doPerHitEnemy(damage hitBy){
        StatsController.inst.lastMatchHits = StatsController.inst.lastMatchHits + 1;
        StatsController.inst.lastMatchDamage = StatsController.inst.lastMatchDamage + hitBy.damageSize;
        StatsController.inst.saveValues();
        hero.chargeUltimate(hero.chargeUltimatePerHit);
    }
}
