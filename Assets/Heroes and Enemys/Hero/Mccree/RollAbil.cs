using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAbil : Ability
{
    public Gun gun;
    Player owner;
    public override void execute()
    {                
        StartCoroutine(doRoll());
    }

    void Start() {
        owner = GetComponent<Player>();
    }

    IEnumerator doRoll(){
        var timer = 0f;
        while(timer < 0.5f){
            timer  = timer + Time.deltaTime;
            transform.position = transform.position + (transform.forward * Time.deltaTime);
            // gun.am
            yield return null;
        }
    }
    void Update() {
        if(! coolDownNow){
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                execute();
            }
        }else{
            countCoolDownIfItIs();
        }
    }
}
