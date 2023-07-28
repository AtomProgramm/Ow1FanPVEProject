using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerativeBurst : Ability
{
    public HittableEntity owner;
    public float sizeOfInstantHealHeal;
    public float sizeOfHealInTime;
    public float timeToHeal;
    public float radiusHeal;


    IEnumerator healingProcess(){
        List<HittableEntity> toHeal = new List<HittableEntity>();
        var tmpInRadius = Physics.OverlapSphere(transform.position, radiusHeal);
        foreach(var nowCollider in tmpInRadius){
            var nowColliderHittable = nowCollider.GetComponent<HittableEntity>();
            if(nowColliderHittable != null){
                if(nowColliderHittable.typeOfHittableEntity == HittableEntity.TypeOfHittableEntity.player){
                    toHeal.Add(nowColliderHittable);
                    nowColliderHittable.tookHeal(sizeOfInstantHealHeal);
                }
            }
        }
        owner.tookHeal(sizeOfInstantHealHeal);

        float timerHealing = 0;
        while(timerHealing < timeToHeal){
            timerHealing = timerHealing + Time.deltaTime;
            var nowFrameToHeal = (sizeOfHealInTime / timeToHeal) * Time.deltaTime;
            owner.tookHeal(nowFrameToHeal);
            foreach(var nowHeal in toHeal){
                nowHeal.tookHeal(nowFrameToHeal);
            }
            yield return null;
        }
    }

    public override void execute()
    {
        setNowCoolDown();
        StartCoroutine(healingProcess());
    }
    


    void Start(){
        owner = GetComponent<HittableEntity>();
    }
    void Update(){
        if(! coolDownNow){
            uiSet.setValueOfThisUI(-1);
            if(Input.GetKey(KeyCode.LeftShift)){
                execute();
            }
        }else{
            uiSet.setValueOfThisUI(coolDownTimer);
        }
        countCoolDownIfItIs();
    }
}
