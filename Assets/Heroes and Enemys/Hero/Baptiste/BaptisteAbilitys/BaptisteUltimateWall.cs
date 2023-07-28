using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaptisteUltimateWall : MonoBehaviour
{
    public float timeAlive = 10f;
    public float partDamageUp = 1f;

    Collider myCollider;
    public bool isActive = false;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myCollider.enabled = false;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            setWall();
        }
    }




    public void setWall(){
        myCollider.enabled = true;
        myCollider.isTrigger = true;
        Destroy(gameObject, timeAlive);
        isActive = true;
    }


    void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<Projectile>();
        if(t !=null){
            t.damage.damageSize = t.damage.damageSize + (t.damage.damageSize * partDamageUp);
        }
    }



 // !! i try to do update damage raycast on hittableEntity  methods.  Now I will somehow add something to at least resemble the functionality, and after that I will have to rewrite the project almost completely (If I really am going to fraternize for this project - if it seems necessary and possible)
    // public override void initOnStart()
    // {}

    // public override float calculateTheAmountOfDamage(damage damageIn)
    // {
    //     return 0;
    // }

    

    // public override float calculateTheAmountOfHeal(float healIn)
    // {
    //     return 0;
    // }
    // public override void playEffectsOnHeal(float healIn){}
    // public override void playInjure(){}
}
