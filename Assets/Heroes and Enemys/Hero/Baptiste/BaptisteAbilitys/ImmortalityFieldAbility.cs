using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalityFieldAbility : Ability
{
    public GameObject prefabOfField;
    public GameObject pointToSpawnField;

    public override void execute()
    {
        setNowCoolDown();
        Instantiate(prefabOfField, pointToSpawnField.transform.position, pointToSpawnField.transform.rotation);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(! coolDownNow){
            uiSet.setValueOfThisUI(-1);
            if(Input.GetKeyDown(KeyCode.E)){
                execute();
            }
        }else{
            uiSet.setValueOfThisUI(coolDownTimer);
            countCoolDownIfItIs();
        }
    }
}
