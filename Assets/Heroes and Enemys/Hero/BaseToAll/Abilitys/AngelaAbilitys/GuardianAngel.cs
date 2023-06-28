using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAngel : Ability
{
    public float maxAngle;
    public float maxDist;
    public float minDist;
    public Camera camera;
    public Player thisPlayer;
    private Rigidbody rigidbody;
    [Space(5)]
    public float energyMax;
    [Space(5)]
    public float speed = 17;


    private float energyTimer;
    // private float energyTimer;
    
    public override void execute()
    {
        if(coolDownNow){
            return;
        }else{
            coolDownNow = true;
            var tmpCan = MissionController.instance.playersOnMission.FindAll( item => (Vector3.Angle((item.transform.position - transform.position), camera.transform.forward) < maxAngle));
            tmpCan = MissionController.instance.playersOnMission.FindAll( item => (Vector3.Distance(item.transform.position, transform.position) < maxDist));
            tmpCan.Sort((it1, it2)=>Vector3.Angle((it1.transform.position - transform.position), camera.transform.forward).CompareTo(
                                    Vector3.Angle((it2.transform.position - transform.position), camera.transform.forward)));
            tmpCan.Remove(thisPlayer);
            

            IEnumerator moveProcess(Vector3 to){
                while(energyTimer < energyMax){
                    rigidbody.velocity = (to - transform.position).normalized * speed + (Vector3.up * 0.1f);
                    // rigidbody.AddForce((to - transform.position).normalized * speed + (Vector3.up * 0.1f) - rigidbody.velocity, ForceMode.VelocityChange);
                    energyTimer = energyTimer + Time.deltaTime;
                    if(Vector3.Distance(to, transform.position) <= minDist){
                        break;
                    }
                    // if(Input.GetKey(SettingsContainer.instanceOfSettingContainer.jump)){
                    if(Input.GetKey(KeyCode.Space)){
                        rigidbody.velocity = (to - transform.position).normalized * speed + Vector3.up;
                        // rigidbody.AddForce((to - transform.position).normalized * speed + Vector3.up - rigidbody.velocity, ForceMode.VelocityChange);
                        energyTimer = 0;
                        break;
                    }
                    // if(Input.GetKey(SettingsContainer.instanceOfSettingContainer.ability1)){
                    if(Input.GetKey(KeyCode.LeftControl)){
                        energyTimer = 0;
                        rigidbody.velocity = new Vector3(0, speed,0);
                        // rigidbody.AddForce(new Vector3(0, speed,0)  - rigidbody.velocity, ForceMode.VelocityChange);

                        break;
                    }
                    yield return null;
                }
                if(energyTimer > energyMax){
                    rigidbody.velocity = Vector3.zero;
                }
                energyTimer = 0;
            }
            foreach(var nowPlayer in tmpCan){
                if(Vector3.Distance(nowPlayer.transform.position, transform.position) > maxDist){
                    continue;
                }else{
                    if(isCanSeePlayer(nowPlayer, transform)){
                        StartCoroutine(moveProcess(nowPlayer.transform.position));
                        break;
                    }
                }
            }
        }
        
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // print(rigidbody);
        camera = Camera.main;
        // print(camera);
        // print("asdasdadasd");
    }

    void Update()
    {
        if(coolDownNow){
            coolDownTimer = coolDownTimer - Time.deltaTime;
        }
        if(coolDownTimer < 0){
            coolDownNow = false;
            coolDownTimer = coolDown;
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            execute();
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
}
