using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageGrabController : MonoBehaviour
{
    public KeyCode toGrab = KeyCode.E;
    public GameObject pointToHostage;




    [HideInInspector]
    public Player pl;
    [HideInInspector]
    public Hostage possibleToGrabHostage;
    [HideInInspector]
    public Hostage grabNow = null;
    [HideInInspector]
    public float oldSpeed;

    void Start(){
        pl = GetComponent<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(toGrab)){
            if(grabNow == null){
                if(possibleToGrabHostage != null){
                    possibleToGrabHostage.doGrab(this);
                }
            }else{
                grabNow.doUnGrab(this);
            }
        }
    }
}
