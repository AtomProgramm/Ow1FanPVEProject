using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealBulletFactory))]
public class BaptisteGun : MonoBehaviour, Gun
{    
    public Player ownerGun;
    public Animator animator;
    public UIGuns uiOfGun;


    [Header("Primary fire")]
    public HittableEntity.damage damagePerShoot;
    public float timeBetweenRoundsSizePrimaryFire;
    public float timeBetweenShootsSizePrimaryFire;
    public int countShootInRound;
    public int amoPrimaryFireMax;
    public float ultCharge;
    public GameObject effectPrim;


    [Header("Alt fire")]
    public float timeBetweenShootsSizeAltFire;
    public int amoAltFireMax;
    public GameObject bulletPrefab;
    public GameObject effectAlt;
    public HealBulletFactory projectileFactory;


    [Header("General to types of fire")]
    public GameObject positionToSpawnBullet;
    public float timeToReloading;





    private bool isNowRoundShooting = false;
    private bool isNowReloading = false;
    private float timerBetweenPrRounds = 0;
    private float timerBetweenAltShoot = 0;
    private int amoPrimaryNow;
    private int amoAltNow;
    public Camera fpsCam;


    void Start() {
        projectileFactory.bulletPrefab = bulletPrefab;
        projectileFactory.owner  = ownerGun;
        isNowRoundShooting = false;
        amoPrimaryNow = amoPrimaryFireMax;
        amoAltNow = amoAltFireMax;
    }
    void Update() {
        uiOfGun.setAmoNow(amoPrimaryNow, amoPrimaryFireMax, false, amoAltNow, amoAltFireMax);


        if(Input.GetKey(KeyCode.Mouse0) && (!isNowReloading) && (! isNowRoundShooting) && (timerBetweenPrRounds <= 0)){
            shoot();
            timerBetweenPrRounds = timeBetweenRoundsSizePrimaryFire;
        }else if((timerBetweenPrRounds > 0) && (! isNowRoundShooting)){
            timerBetweenPrRounds = timerBetweenPrRounds - Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Mouse1) && (!isNowReloading) && (timerBetweenAltShoot <= 0)){
            altShoot();
            timerBetweenAltShoot = timeBetweenShootsSizeAltFire;
        }else if((timerBetweenAltShoot > 0) && (!isNowReloading)){
            timerBetweenAltShoot = timerBetweenAltShoot - Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.R) && (!isNowReloading) && ((amoPrimaryNow < amoPrimaryFireMax) || (amoAltNow < amoAltFireMax))){
            StartCoroutine(Reloading());
        }
    }


    IEnumerator primaryShootingRound(){
        isNowRoundShooting = true;
        for(int nShootInR = 0; nShootInR <= countShootInRound;nShootInR = nShootInR + 1){
            if(amoPrimaryNow > 0){
                amoPrimaryNow = amoPrimaryNow - 1;
                Instantiate(effectPrim, positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
                RaycastHit hit;
                Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                // Debug.DrawRay(ray.origin,ray.direction * 1000,Color.cyan,2);
                if(Physics.Raycast(ray, out hit, 1000)){
                // if(Physics.Raycast(fpsCam.ScreenPointToRay(Vector3.zero), out hit)){
                    var toHit = hit.collider.gameObject.GetComponent<HittableEntity>();
                    if(toHit != null){
                        if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.enemy) && damagePerShoot.canDamageOnly.enemy){
                            toHit.tookDamage(damagePerShoot);  
                            continue;
                        }
                        if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.other) && damagePerShoot.canDamageOnly.other){
                            toHit.tookDamage(damagePerShoot);  
                            continue;
                        }
                        if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player) && damagePerShoot.canDamageOnly.player){
                            toHit.tookDamage(damagePerShoot);  
                            continue;
                        }
                        ownerGun.hero.chargeUltimate(ultCharge);
                    }
                }
            }
            yield return new WaitForSeconds(timeBetweenShootsSizePrimaryFire);
        }
        isNowRoundShooting = false;
    }
    IEnumerator Reloading(){
        isNowReloading = true;
        yield return new WaitForSeconds(timeToReloading);
        amoPrimaryNow = amoPrimaryFireMax;
        amoAltNow = amoAltFireMax;
        isNowReloading = false;
    }

    public void shoot()
    {
        if(amoPrimaryNow > countShootInRound){
            StartCoroutine(primaryShootingRound());
        }
    }

    public void altShoot()
    {
        if(amoAltNow > 0){
            Instantiate(effectAlt, positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
            projectileFactory.doInst(positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
            amoAltNow = amoAltNow  - 1;
        }
    }
}
