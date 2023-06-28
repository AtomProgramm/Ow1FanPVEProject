using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : MonoBehaviour, Gun
{
    
    private float timerBetweenShoot;
    public int amoNow;
    private Camera fpsCam;


    public HeroBehaviors ownerGun;
    public float timeBetweenShootSize;
    public float timeToReloading;
    public int amo;
    public GameObject positionToSpawnBullet;
    public GameObject bulletPrefab;
    public Animator animator;
    public void altShoot()
    {
        //noHaveAltShoot
    }

    public void shoot()
    {
       var tmpBulletInstance = Instantiate(bulletPrefab, positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
       tmpBulletInstance.GetComponent<Projectile>().owner = ownerGun;
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
        
    }
}
