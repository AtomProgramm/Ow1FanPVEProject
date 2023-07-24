using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public GameObject bulletPrefab;
    public HittableEntity owner;
    public int maxCountInstances = 50;
    private List<Projectile> alive = new List<Projectile>();
    private List<Projectile> destroyed = new List<Projectile>();
    private int nowCountAllInst = 0;

    void Start()
    {
        nowCountAllInst = alive.Count + destroyed.Count;
    }
    
    public void destroyBullet(Projectile toDestroy){
        toDestroy.gameObject.SetActive(false);
        destroyed.Add(toDestroy); 
        alive.Remove(toDestroy);
    }

    public void doInst(Vector3 pos, Quaternion rotation){
        if(nowCountAllInst < maxCountInstances){
            var tmpGameObject = Instantiate(bulletPrefab,pos,rotation);
            var tmpProjectile = tmpGameObject.GetComponent<Projectile>();
            alive.Add(tmpProjectile);
            tmpProjectile.doOnHit = destroyBullet;
            tmpProjectile.damage.owner = owner;
            tmpProjectile.destroySelfAfterHit = false;
            tmpProjectile.destroySelfAfterTimeExit = false;
            nowCountAllInst = nowCountAllInst + 1;
        }else{
            if(destroyed.Count > 0){
                var toReSpawn = destroyed[0];
                destroyed.Remove(toReSpawn);
                toReSpawn.transform.position = pos;
                toReSpawn.transform.rotation = rotation;
                toReSpawn.gameObject.SetActive(true);
                alive.Add(toReSpawn);
            }else{
                destroyBullet(alive[0]);
                doInst(pos, rotation);
            }
        }

    }
    public void destroyFactory(){
        foreach(var i in alive){
            Destroy(i.gameObject);
        }
        foreach(var i in destroyed){
            Destroy(i.gameObject);
        }
    }
}
