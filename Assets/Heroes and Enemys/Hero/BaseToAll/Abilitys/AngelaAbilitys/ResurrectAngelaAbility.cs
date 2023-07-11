using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectAngelaAbility : Ability
{
    public float maxDist = 0.5f;
    public float timeToResurrect = 2f;
    public float moveSpeedOnResurrect = 0.1f;

    private Player targetToResurrect;






    void Start(){}
    void Update(){
        countCoolDownIfItIs();
        if(Input.GetKeyDown(KeyCode.E)){
            execute();
        }
        if(coolDownNow){
            uiSet.setValueOfThisUI(coolDownTimer);
        }else{
            uiSet.setValueOfThisUI(-1);
        }
    }
    public override void execute()
    {
        if(coolDownNow) return;
        var rowOfPossible = getPosableTargets();
        if(rowOfPossible.Count > 0){
            setNowCoolDown();
            
            var me = GetComponent<Player>();
            float speedBefore = me.FPSController.walkSpeed;
            me.FPSController.walkSpeed = moveSpeedOnResurrect;

            var nowPlayerToResurrect = rowOfPossible[0];
            var rplCollider = nowPlayerToResurrect.GetComponent<Collider>(); 
            rplCollider.enabled = false; // to stop took damage
            var rplRenderer = nowPlayerToResurrect.GetComponent<MeshRenderer>(); 
            rplRenderer.enabled = true;
            var endColor = rplRenderer.material.color;
            var stColor = new Color(rplRenderer.material.color.r, rplRenderer.material.color.g, 0f, 1f);
            rplRenderer.material.color = stColor;
            nowPlayerToResurrect.FPSController.enabled = false; // to stop movement

            float resurrectTimer = timeToResurrect;

            IEnumerator recurAnim(){
                while(true){
                    resurrectTimer = resurrectTimer - Time.deltaTime;
                    if(resurrectTimer < 0 ) break;
                    rplRenderer.material.color = Color.Lerp(stColor, endColor, 1 - (resurrectTimer / timeToResurrect));
                    yield return new WaitForEndOfFrame(); 
                }

                yield return new WaitForEndOfFrame(); 
                me.FPSController.walkSpeed = speedBefore;
                rplCollider.enabled = true;
                nowPlayerToResurrect.FPSController.enabled = true; 
                rplRenderer.material.color = endColor;

                nowPlayerToResurrect.hero.hp = nowPlayerToResurrect.hero.maxHealth; 
                nowPlayerToResurrect.injured = false;
            }
            StartCoroutine(recurAnim());
        }  
    }
    private void FixedUpdate() {
        //todo: set ui on can/can't resurrect someone
        // var tmpRow = getPosableTargets();
        // if(tmpRow.Count > 0){
        //     targetToResurrect = tmpRow[0];
        // }else{
        //     targetToResurrect = null;
        // }
    }

    public List<Player> getPosableTargets(){
        // print("not filtred and sorted");
        // print(MissionController.instance.playersOnMission.Count);
        var r = MissionController.instance.playersOnMission.FindAll(item => item.injured);
        // print("injured");
        // print(r.Count);
        r = r.FindAll(item => (Vector3.Distance(item.transform.position, transform.position) < maxDist));
        // print("maxDist");
        // print(r.Count);
        r.Sort((it1, it2)=>Vector3.Distance(it1.transform.position, transform.position).CompareTo(Vector3.Distance(it2.transform.position, transform.position)));
        // print("sorted");
        // print(r.Count);
        r.Remove(GetComponent<Player>());
        // print("not me");
        // print(r.Count);
        return r;
    }
}
