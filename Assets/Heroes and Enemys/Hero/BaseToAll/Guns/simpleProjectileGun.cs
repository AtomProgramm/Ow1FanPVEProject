using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleProjectileGun : MonoBehaviour, Gun
{
    public float timerBetweenShootSize;
    public float timerBetweenShoot;
    public GameObject positionToSpawnBullet;
    public GameObject bulletPrefab;
    public void altShoot()
    {
        throw new System.NotImplementedException();
    }

    public void shoot()
    {
        // throw new System.NotImplementedException();
        Instantiate(bulletPrefab,positionToSpawnBullet.transform.position, positionToSpawnBullet.transform.rotation);
    }

    void Start(){}

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)){
            timerBetweenShoot = timerBetweenShoot - Time.deltaTime;
            if(timerBetweenShoot < 0){
                shoot();
                timerBetweenShoot = timerBetweenShootSize;
            }
        }
        
    }
}
