using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(FirstPersonController), typeof(HeroBehaviors))]
public class AngelaUltimate : UltimateAbility
{
    public float mouseSensitivity;
    public float speedOfFlying;
    public float timeToUlt;


    private Rigidbody rb;
    private FirstPersonController fpsController;
    public HeroBehaviors heroB;
    private Camera playerCamera;

    private float timerToUlt = -1;

    private float yaw;
    private float pitch;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fpsController = GetComponent<FirstPersonController>();
        heroB = GetComponent<HeroBehaviors>();
        playerCamera = Camera.main;
        // playerCamera = Camera.current;
        
    }

    void Update()
    {
        uiSet.setValueOfThisUI(nowUltimateCharge);
        if(Input.GetKey(KeyCode.Q)){
            ultimateExecute();
        }
        
    }

    public override void execute(){
        if(timerToUlt <= 0){
            timerToUlt = timeToUlt;
            heroB.scriptOfGunPrefab = new List<Gun>();
            foreach(GameObject g in heroB.gunsObjects){
                // heroB.scriptOfGunPrefab.Add(g.GetComponent<Gun>());
                heroB.scriptOfGunPrefab.Add(g.GetComponentInChildren<Gun>());
            } 
            IEnumerator doUltimate(){
                rb.useGravity = false;
                fpsController.enabled = false;
                foreach(Gun g in heroB.scriptOfGunPrefab){
                    if(g == null){
                        continue;
                    }
                    if(g.GetType() == typeof(CaduceusGun)){
                        ((CaduceusGun)g).isUltimateNow = true;

                    }else if(g.GetType() == typeof(ProjectileGun)){
                        ((ProjectileGun)g).amoNow = int.MaxValue;

                    }
                } 
                while(timerToUlt > 0 ){
                    timerToUlt = timerToUlt - Time.deltaTime;    

                    void subMethodCameraMoving(){
                        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

                        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

                        // pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

                        transform.localEulerAngles = new Vector3(0, yaw, 0);
                        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
                    } 

                    void subMethodBodyMoving(){
                        float upVelocityComponent = 0;
                        if(Input.GetKey(KeyCode.Space)){
                            upVelocityComponent = 1;
                        }

                        Vector3 verticalVelocityComponent = new Vector3(0, upVelocityComponent, 0);

                        rb.velocity = ((playerCamera.transform.forward * Input.GetAxis("Vertical")) + (playerCamera.transform.right * Input.GetAxis("Horizontal")) + verticalVelocityComponent).normalized * speedOfFlying ;

                        // rb.AddForce((((playerCamera.transform.forward * Input.GetAxis("Vertical")) + (playerCamera.transform.right * Input.GetAxis("Horizontal")) + verticalVelocityComponent).normalized * speedOfFlying ) - rb.velocity, ForceMode.VelocityChange);
                    } 
                    subMethodCameraMoving();
                    subMethodBodyMoving();
                    // foreach(Gun g in heroB.scriptOfGunPrefab){
                    foreach(Gun g in heroB.scriptOfGunPrefab){
                        if(g == null){
                            continue;
                        }
                         if(g.GetType() == typeof(ProjectileGun)){
                            ((ProjectileGun)g).amoNow = int.MaxValue;
                            break;
                        }
                    } 
                    yield return null;
                }
                foreach(Gun g in heroB.scriptOfGunPrefab){
                    if(g == null){
                        continue;
                    }
                    if(g.GetType() == typeof(CaduceusGun)){
                        ((CaduceusGun)g).isUltimateNow = false;

                    }else if(g.GetType() == typeof(ProjectileGun)){
                        ((ProjectileGun)g).amoNow = ((ProjectileGun)g).amo;
                    }
                }
                rb.useGravity = true;
                fpsController.enabled = true;
            }
            StartCoroutine(doUltimate());
        }
    }

}
