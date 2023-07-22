using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class Hostage : HittableEntity
{
    // [Header("Main")]
    // public new float maxHp = 200;
    [Header("Effect")]
    public float newSpeed;
    [Header("Actions")]
    public UnityEvent OnGrab;
    public UnityEvent OnDeGrab;
    public UnityEvent OnInjured;

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color colorEventGrab = Color.green;
        public Color colorEventUnGrab = Color.green;
        public Color colorEventInjure = Color.green;
    #endif


    void Start(){}



    public void doGrab(HostageGrabController hc){
        OnGrab.Invoke();


        hc.grabNow = this;

        hc.oldSpeed = hc.pl.FPSController.walkSpeed;
        hc.pl.FPSController.walkSpeed = newSpeed;

        transform.position = hc.pointToHostage.transform.position;
        transform.parent = hc.pointToHostage.transform;
        
    }
    public void doUnGrab(HostageGrabController hc){
        OnDeGrab.Invoke();


        hc.grabNow = null;

        hc.pl.FPSController.walkSpeed = hc.oldSpeed;

        transform.parent = null;
    }




    private void OnTriggerEnter(Collider other) {
        var tmp = other.GetComponent<HostageGrabController>();
        if(tmp != null){
            if(tmp.grabNow == null){
                tmp.possibleToGrabHostage = this;
            }
        }   
    }
    private void OnTriggerExit(Collider other) {
        var tmp = other.GetComponent<HostageGrabController>();
        if(tmp != null){
            tmp.possibleToGrabHostage = null;
        }   
    }


    public override void playInjure(){ 
        injured = true;
        OnInjured.Invoke();
    }

    public override void initOnStart(){}

    public override float calculateTheAmountOfDamage(damage damageIn)
    {
        return damageIn.damageSize;
    }

    public override void playEffectsOnDamage(damage damageIn){}

    public override float calculateTheAmountOfHeal(float healIn)
    {
        return healIn;
    }

    public override void playEffectsOnHeal(float healIn)
    {}


    #if UNITY_EDITOR
        void Update()
        {
            OnInspectorUpdate();
        }
        private void OnInspectorUpdate(){
            for(var ind = 0; ind < OnGrab.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnGrab.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventGrab);
            }   
            for(var ind = 0; ind < OnDeGrab.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnDeGrab.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventUnGrab);
            }   
            for(var ind = 0; ind < OnInjured.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnInjured.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventInjure);
            }    
        }

    #endif
}
