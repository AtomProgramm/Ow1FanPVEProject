using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExoBoots : Ability
{
    [Header("Charge")]
    public float speedCharge = 2;
    public float timeToDeCharge = 4;
    [Header("Jump")]
    public float maxSpeed = 17.44f;
    public float maxHeight = 9.1f;
    [Header("UI")]
    public Slider chargeUI; 


    private float chargeValueNow;
    private float timerResetCharge;
    private bool nowJumping = false;


    IEnumerator jumping(){
        chargeUI.gameObject.SetActive(false);
        float startJumpPoint = transform.position.y;
        float nowJumpOn = startJumpPoint;
        float jumpEndOn = nowJumpOn + Mathf.Lerp(0, maxHeight, chargeValueNow);
        float jumpSpeed = nowJumpOn + Mathf.Lerp(0, maxSpeed, chargeValueNow);
        nowJumping = true;
        while(transform.position.y < (startJumpPoint + jumpEndOn)){
            transform.position = new Vector3(transform.position.x, startJumpPoint + nowJumpOn, transform.position.z);
            nowJumpOn = nowJumpOn + Time.deltaTime + jumpSpeed;
            yield return null;
        }
        chargeValueNow = 0;
        nowJumping = false;
    }

    void Start(){}

    void Update()
    {
        if(!nowJumping){
            if(timerResetCharge <= 0){
                chargeValueNow = 0;
                chargeUI.gameObject.SetActive(false);
            }
            if(Input.GetKey(KeyCode.LeftControl)){
                chargeUI.gameObject.SetActive(true);
                chargeUI.value = chargeValueNow;
                chargeValueNow = Math.Min(chargeValueNow + (Time.deltaTime * speedCharge), 1f);
                timerResetCharge = timeToDeCharge;
            }else{
                timerResetCharge = timerResetCharge - Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.Space)){
                if(chargeValueNow > 0){
                    StartCoroutine(jumping());
                }
            }
        }
    }







    public override void execute()   {  
        // throw new System.NotImplementedException();
    }
}
