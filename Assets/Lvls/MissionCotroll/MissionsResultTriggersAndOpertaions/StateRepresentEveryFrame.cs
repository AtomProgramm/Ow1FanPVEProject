using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class StateRepresentEveryFrame : MonoBehaviour
{
    public Boolean stateNow;
    public UnityEvent onPositive;
    public UnityEvent onNegative;

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Boolean showLineToNegativeCondition = false;
        public Color colorPositiveCondition = Color.green;
        public Color colorNegativeCondition = Color.red;
    #endif


    void Start(){}
    void Update(){
        if(stateNow){
            onPositive.Invoke();
        }else{
            onNegative.Invoke();
        }
        #if UNITY_EDITOR
            OnInspectorUpdate();
        #endif
    }


    public void setStateToPositive(){
        stateNow = true;
    }
    public void setStateToNegative(){
        stateNow = false;
    }


    #if UNITY_EDITOR
        private void OnInspectorUpdate() {
            for(var ind = 0; ind < onPositive.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = onPositive.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorPositiveCondition);


            }
            if(showLineToNegativeCondition){
                for(var ind = 0; ind < onNegative.GetPersistentEventCount(); ind = ind + 1 )
                {
                    var nowTarget = onNegative.GetPersistentTarget(ind);
                    Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorNegativeCondition);
                }
            }
        }
    #endif
}
