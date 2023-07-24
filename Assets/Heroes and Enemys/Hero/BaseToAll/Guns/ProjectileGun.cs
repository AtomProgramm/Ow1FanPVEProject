using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileFactory))]
public class ProjectileGun : MonoBehaviour, Gun
{
    
    private float timerBetweenShoot;
    public int amoNow;
    private Camera fpsCam;


    public HittableEntity ownerGun;
    public float timeBetweenShootSize;
    public float timeToReloading;
    public int amo;
    public GameObject positionToSpawnBullet;
    public GameObject bulletPrefab;
    public ProjectileFactory projectileFactory;
    public Animator animator;
    [Space(10)]
    public UIGuns uiOfGun;



    public void altShoot()
    {
        //noHaveAltShoot
    }

    public void shoot()
    {
        projectileFactory.doInst(positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
        StatsController.inst.lastMatchShoots = StatsController.inst.lastMatchShoots + 1;
        StatsController.inst.saveValues();
    }
    public void reload()
    {
        IEnumerator reloadSetOnTime(){
            yield return new WaitForSeconds(timeToReloading);
            amoNow = amo;
        }
        StartCoroutine(reloadSetOnTime());
    }

    void Start()
    {
        amoNow = amo;
        timerBetweenShoot = timeBetweenShootSize;
        fpsCam = Camera.current;
        projectileFactory = GetComponent<ProjectileFactory>();
        projectileFactory.bulletPrefab = bulletPrefab;
        projectileFactory.owner = ownerGun;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)){
            timerBetweenShoot = timerBetweenShoot - Time.deltaTime;
            if(timerBetweenShoot < 0){
                if( (amoNow > 0) ){
                    shoot();
                    animator.SetBool("Shoot", true);
                    animator.SetBool("Reload", false);
                    amoNow = amoNow - 1;
                }else{
                    animator.SetBool("Shoot", false);
                }
                timerBetweenShoot = timeBetweenShootSize;
            }
        }else{
            animator.SetBool("Shoot", false);
        }
        if(Input.GetKey(KeyCode.R)){
            if(amoNow < amo){
                reload();
                animator.SetBool("Reload", true);
            }
        }
        
        uiOfGun.setAmoNow(amoNow, amo);
    }
}
