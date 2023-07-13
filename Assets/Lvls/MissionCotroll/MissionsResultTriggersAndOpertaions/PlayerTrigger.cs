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
    // public Boolean enemyStopCondition = false;  to other trigger?
    public Boolean enemyStopCondition = false;
    public UnityEvent doOnConditions;
    public UnityEvent notConditions;


    #if UNITY_EDITOR
        [Header("InEditor")]
        public Boolean showLineToNegativeCondition = false;
        public Color colorPositiveCondition = Color.green;
        public Color colorNegativeCondition = Color.red;
    #endif



    private List<Player> allPlayersNowInTrigger = new List<Player>();
    private List<Enemy> allEnemiesNowInTrigger = new List<Enemy>();
    

    void Start() {
        GetComponent<Collider>().isTrigger = true;
    }

    public int getNowCountPlayerInTrigger(){
        return allPlayersNowInTrigger.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if(enemyStopCondition){
            var enemyOfEnter = other.gameObject.GetComponent<Enemy>();
            if(enemyOfEnter != null) allEnemiesNowInTrigger.Add(enemyOfEnter);
            if(allEnemiesNowInTrigger.Count != 0) {
                notConditions.Invoke();
                return;
            }
        }
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
    void OnTriggerStay(Collider other)
    {
        // allEnemiesNowInTrigger.RemoveAll(s => s == null);
        // allPlayersNowInTrigger.RemoveAll(s => s == null);
        foreach(var i in allEnemiesNowInTrigger){
            try{
                if(((MonoBehaviour)i).transform == null){
                    allEnemiesNowInTrigger.Remove(i);
                    return;
                }
            }
            catch (Exception e)
            {
                allEnemiesNowInTrigger.Remove(i);
                return;
            }
        }
        foreach(var i in allPlayersNowInTrigger){
            try{
                if(((MonoBehaviour)i).transform == null){
                    allPlayersNowInTrigger.Remove(i);
                    return;
                }
            }
            catch (Exception e)
            {
                allPlayersNowInTrigger.Remove(i);
                return;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        var exitPl = other.gameObject.GetComponent<Player>();
        if(enemyStopCondition){
            var enemyOfExit = other.gameObject.GetComponent<Enemy>();
            if(enemyOfExit != null) allEnemiesNowInTrigger.Remove(enemyOfExit);
        }
        if(exitPl != null){
            if(! needAllPlayersCondition  ||  ((allEnemiesNowInTrigger.Count == 0) || (! enemyStopCondition))) {
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
