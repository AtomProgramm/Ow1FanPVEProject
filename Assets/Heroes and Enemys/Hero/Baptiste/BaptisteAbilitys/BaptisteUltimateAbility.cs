using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaptisteUltimateAbility : UltimateAbility
{
    public Player owner;
    BaptisteUltimateWall baptisteUltimateWall;
    public GameObject prefabBaptisteUltimateWall;
    public override void execute()
    {
       IEnumerator posOfWall(){
            while(!baptisteUltimateWall.isActive){
                RaycastHit hit;
                Ray ray = owner.FPSController.playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                if(Physics.Raycast(ray, out hit, 1000)){
                    baptisteUltimateWall.transform.position = hit.point;
                }
                yield return null;
            }
            baptisteUltimateWall.transform.parent  = null;
       }
       var tmp = Instantiate(prefabBaptisteUltimateWall, transform);
       baptisteUltimateWall = tmp.GetComponent<BaptisteUltimateWall>();
       StartCoroutine(posOfWall());
    }


    void Start()
    {
        
    }

    void Update()
    {
        uiSet.setValueOfThisUI(nowUltimateCharge);
        print(nowUltimateCharge);
        if(Input.GetKeyDown(KeyCode.Q)){
            ultimateExecute();
        }   
    }
}
