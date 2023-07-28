using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaccreGun :MonoBehaviour,  Gun 
{
    public GameObject effectPrim;
    public GameObject positionToSpawnEffect;
    public HittableEntity.damage damagePerShoot;
    public Camera fpsCam;
    public Player owner;
    public float ultCharge;
    public int maxAmo;
    public int amo;
    public float betwenShootSize = 0.5f;
    public float sizeTToReload = 2f;
    private float timerShoot;
    private float timerReload;
    IEnumerator nowReloading = null;
    public void altShoot()
    {
        // throw new System.NotImplementedException();
    }

    public void shoot()
    {
        amo = amo - 1;
        Instantiate(effectPrim, positionToSpawnEffect.transform.position, positionToSpawnEffect.transform.rotation);
        RaycastHit hit;
        Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin,ray.direction * 1000,Color.cyan,2);
        if(Physics.Raycast(ray, out hit, 1000)){
            var toHit = hit.collider.gameObject.GetComponent<HittableEntity>();
            print(toHit);
            if(toHit != null){
            print(toHit.transform.name);
                if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.enemy) && damagePerShoot.canDamageOnly.enemy){
                    toHit.tookDamage(damagePerShoot);  
                }
                if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.other) && damagePerShoot.canDamageOnly.other){
                    toHit.tookDamage(damagePerShoot);  
                }
                if((toHit.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player) && damagePerShoot.canDamageOnly.player){
                    toHit.tookDamage(damagePerShoot);  
                }
                owner.hero.chargeUltimate(ultCharge);
            }
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && (amo > 0)){
            if(timerShoot < 0){
                shoot();
            }else{
                timerShoot = timerShoot - Time.deltaTime;
            }
        }
        if(Input.GetKey(KeyCode.R) && (amo < maxAmo) && (nowReloading == null)){
            reload();
        }
    }
    IEnumerator reloading(){
        yield return new WaitForSeconds(sizeTToReload);
        timerReload = sizeTToReload ;
        amo = maxAmo;
        nowReloading = null;
    }
    public void reload(){
       nowReloading =  reloading();
       StartCoroutine(nowReloading);
    }
}
