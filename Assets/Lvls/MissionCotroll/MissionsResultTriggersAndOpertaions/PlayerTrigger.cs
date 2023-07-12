using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    public Boolean needAllPlayersCondition = false;
    public UnityEvent doOnConditions;

    private List<Player> allPlayersNowInTrigger = new List<Player>();
    // if any player
    // if all players
    // invoke if
    // can do on player in ?
    // if part of all players ?
    

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
        allPlayersNowInTrigger.Remove(other.gameObject.GetComponent<Player>());
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
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, Color.green);
            }
        }
    #endif
}
