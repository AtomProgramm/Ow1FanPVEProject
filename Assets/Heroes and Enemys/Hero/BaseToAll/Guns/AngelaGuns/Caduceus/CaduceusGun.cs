using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaduceusGun : MonoBehaviour, Gun
{
    public float maxDist;
    public float maxAngleToCaptTarget;
    [Space(2)]
    public float timeToDeactivate;
    public Boolean isUltimateNow;


    [Space(10)]
    public float healPerSecond;
    public float buffPart;
    


    [Space(10)]
    public GameObject pointOfStartTracer;
    public GameObject secondPointToTakeDirection;
    [Space(2)]
    public Color colorHeal;
    public Color colorBuff;
    [Space(2)]
    public List<LineRenderer> tracers;
    public List<float> sizeOfOffset;
    [Space(2)]
    public Animator animator;



    private Player target;
    private Player thisPlayer;
    private bool nowBuffNoHeal;
    private float timerToDeactivate;


    public void updateTrails(){
        if(target != null){
            foreach(var tr in tracers){
                tr.gameObject.SetActive(true);
                if(nowBuffNoHeal){
                    tr.startColor = colorBuff;
                    tr.endColor = colorBuff;
                }else{
                    tr.startColor = colorHeal;
                    tr.endColor = colorHeal;
                }

                tr.SetPosition(0,                       pointOfStartTracer.transform.position);
                tr.SetPosition(tr.positionCount-1,      target.transform.position);
            }
        //     // Vector3 straightFromCaduceus = pointOfStartTracer.transform.forward;
        //     Vector3 straightFromCaduceus = (secondPointToTakeDirection.transform.position - pointOfStartTracer.transform.position).normalized;
        //     Vector3 rightToTarget =  target.transform.position - pointOfStartTracer.transform.position;
        //     float distToTarget = Vector3.Distance(target.transform.position, pointOfStartTracer.transform.position);
        //     float distSteps = distToTarget / tracers[0].positionCount;
        //     for(var i = 1; i < (tracers[0].positionCount -1); i = i + 1){
        //         Vector3 tmpToTarg = (rightToTarget  / tracers[0].positionCount)  ;
        //         float targPrat = i;
        //         Vector3 tmpStraight = (straightFromCaduceus) ;
        //         float strPart = Math.Max(0, ((tracers[0].positionCount / 2)  - i));
        //         Vector3 nowPos = pointOfStartTracer.transform.position + (tmpToTarg * i * targPrat) + (tmpStraight * distSteps * i * strPart) ;
        //         rightToTarget =  target.transform.position - nowPos;
        //         tracers[0].SetPosition(i, nowPos);
        //         // tracers[0].SetPosition(i, pointOfStartTracer.transform.position + (tmpToTarg * i)) ;
        //         // tracers[0].SetPosition(i, pointOfStartTracer.transform.position +  (tmpStraight * distSteps * i));
        //         // tracers[0].SetPosition(i, pointOfStartTracer.transform.position +  (straightFromCaduceus * distSteps / i )  );

        //         // for(var triInd = 1; triInd < (tracers.Count -1); triInd = triInd + 1){
        //         //     Vector3 p = tracers[triInd].GetPosition(i);
        //         //     nowPos = (p + nowPos) / 2;
        //         //     tracers[triInd].SetPosition(i , nowPos);
        //         // }
        //     }
        // }
            // Vector3 straightFromCaduceus = pointOfStartTracer.transform.forward;
            Vector3 straightFromCaduceus = (secondPointToTakeDirection.transform.position - pointOfStartTracer.transform.position).normalized;
            Vector3 rightToTarget =  (target.transform.position - pointOfStartTracer.transform.position).normalized;
            // Vector3 rightToTarget  = straightFromCaduceus;
            float distToTarget = Vector3.Distance(target.transform.position, pointOfStartTracer.transform.position);
            float distSteps = distToTarget / tracers[0].positionCount;
            for(var i = 1; i < (tracers[0].positionCount -1); i = i + 1){
                Vector3 nowPos = pointOfStartTracer.transform.position + (((rightToTarget + straightFromCaduceus) / 2) * distSteps * i );
                rightToTarget =  (target.transform.position - nowPos).normalized;
                // straightFromCaduceus = ((straightFromCaduceus + rightToTarget ) / 2).normalized;
                tracers[0].SetPosition(i, nowPos);
                // tracers[0].SetWidth

                for(var triInd = 1; triInd < tracers.Count; triInd = triInd + 1){
                    Vector3 p = tracers[triInd].GetPosition(i);
                    float d = Math.Abs(p.x - nowPos.x) +  Math.Abs(p.y - nowPos.y) +  Math.Abs(p.z - nowPos.z) + 1;
                    nowPos = Vector3.Lerp(p,nowPos,Time.deltaTime * 10 * d);
                    tracers[triInd].SetPosition(i , nowPos);
                }
            }
        }
    }
    public void deactivateTrails(){
        foreach(var tr in tracers){
            tr.gameObject.SetActive(false);
            tr.useWorldSpace = true;
        }
    }

    public bool isCanSeePlayer(Player pl, Transform p = null){
        if (p == null) p = transform;
        RaycastHit  hit;
        Vector3 dir = pl.transform.position - p.position;
        Vector3 from = p.position;
        Debug.DrawRay(from, dir,Color.blue,2);
        if(Physics.Raycast(from, dir, out hit)){ 
            return hit.transform.GetComponent<Player>() != null;
        }else{
            return false;
        }
    }


    public void altShoot()
    {
        nowBuffNoHeal = true;
        updateTrails();//throw new System.NotImplementedException();
        animator.SetBool("Shoot", true);
        target.damageModifier = 1f + buffPart;
    }

    public void shoot()
    {
        nowBuffNoHeal = false;
        updateTrails();//throw new System.NotImplementedException();
        animator.SetBool("Shoot", true);
        target.tookHeal(healPerSecond * Time.deltaTime);
    }



    void Start()
    {
        thisPlayer = gameObject.GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        foreach(var tr in tracers){
            tr.positionCount = 24;
            // tr.positionCount = 8;
            // tr.positionCount = 2;
        }
    }

    void Update()
    {
        
        var tmpCan = MissionController.instance.playersOnMission.FindAll( item => (Vector3.Angle((item.transform.position - transform.position), transform.forward) < maxAngleToCaptTarget));
        tmpCan.Sort((it1, it2)=>Vector3.Angle((it1.transform.position - pointOfStartTracer.transform.position), transform.forward).CompareTo(
                                Vector3.Angle((it2.transform.position - pointOfStartTracer.transform.position), transform.forward)));
        tmpCan.Remove(thisPlayer);
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)){
            if(target != null){
                if( ! isCanSeePlayer(target, pointOfStartTracer.transform)){
                    timerToDeactivate = timerToDeactivate - Time.deltaTime;
                    if(timerToDeactivate < 0){
                        target = null;
                        timerToDeactivate = timeToDeactivate; 
                        deactivateTrails();
                    }
                }else{
                    timerToDeactivate = timeToDeactivate;
                }
                if(Input.GetKey(KeyCode.Mouse0)){ shoot();}else{altShoot();}
            }


            if(target == null){
                foreach(var nowPlayer in tmpCan){
                    if(Vector3.Distance(nowPlayer.transform.position, transform.position) > maxDist){
                        continue;
                    }else{
                        if(isCanSeePlayer(nowPlayer, pointOfStartTracer.transform)){
                            target = nowPlayer;
                            nowBuffNoHeal = false;
                            if(Input.GetKey(KeyCode.Mouse0)){ shoot();}else{altShoot();}
                            break;
                        }
                    }
                }
            }
            nowBuffNoHeal = false;
        }else{
            deactivateTrails();
            animator.SetBool("Shoot", false);
            if(target != null) target.damageModifier = 1f;
            target = null;
        }
        
    }
}
