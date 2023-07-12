using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerTrigger : MonoBehaviour
{
    public Boolean needAllPlayersCondition = false;
    public UnityEvent doOnConditions;
    public UnityEvent notConditions;


    #if UNITY_EDITOR
        [Header("InEditor")]
        public Boolean showLineToNegativeCondition = false;
        public Color colorPositiveCondition = Color.green;
        public Color colorNegativeCondition = Color.red;
    #endif



    private List<Player> allPlayersNowInTrigger = new List<Player>();
    

    void Start() {
        GetComponent<Collider>().isTrigger = true;
    }


    void OnTriggerEnter(Collider other)
    {
        var playerOfEnter = other.gameObject.GetComponent<Player>();
        if(playerOfEnter != null){
            if(! needAllPlayersCondition) {
                doOnConditions.Invoke();
                return;
            }
            allPlayersNowInTrigger.Add(playerOfEnter);
            if(needAllPlayersCondition && MissionController.instance.playersOnMission.TrueForAll(pl => allPlayersNowInTrigger.Contains(pl))){
                doOnConditions.Invoke();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        var exitPl = other.gameObject.GetComponent<Player>();
        if(exitPl != null){
            if(! needAllPlayersCondition) {
                notConditions.Invoke();
                return;
            }
            allPlayersNowInTrigger.Remove(exitPl);
            notConditions.Invoke();
        }
    }
    // void void OnCollisionStay(Collision other)
    // {
    //     other.collider.gameObject
    // }





    #if UNITY_EDITOR
        void Update()
        {
            OnInspectorUpdate();
        }
        private void OnInspectorUpdate() {
            for(var ind = 0; ind < doOnConditions.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = doOnConditions.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorPositiveCondition);



            }
            if(showLineToNegativeCondition){
                for(var ind = 0; ind < notConditions.GetPersistentEventCount(); ind = ind + 1 )
                {
                    var nowTarget = notConditions.GetPersistentTarget(ind);
                    Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorNegativeCondition);
                }
            }
                
                
                
        }
    #endif
}
