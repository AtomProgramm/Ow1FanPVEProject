using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelicDescent : Ability
{
    private Rigidbody rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // void Update() {}
    void Update() {
        if(Input.GetKey(SettingsContainer.instanceOfSettingContainer.jump)){
            execute();
        }
    }

    public override void execute()
    {
        // print(rigidbody.velocity.y);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Max(-2, rigidbody.velocity.y), rigidbody.velocity.z); 
    }
}
