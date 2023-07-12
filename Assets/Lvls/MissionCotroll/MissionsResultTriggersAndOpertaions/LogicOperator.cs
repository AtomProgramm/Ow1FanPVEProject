using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class LogicOperator : MonoBehaviour
{
    [Serializable]
    public enum  Operator{
        andOperator,
        orOperator,
        xorOperator

    }
    public Boolean stateA;
    public Boolean stateB;
    public UnityEvent onPositive;
    public UnityEvent onNegative;


    [SerializeField]
    public Operator nowOperator;

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Boolean showLineToNegativeCondition = false;
        public Color colorPositiveCondition = Color.green;
        public Color colorNegativeCondition = Color.red;
    #endif


    void Update(){
        Boolean resultOfOperationStates = stateA;
        if(nowOperator == Operator.andOperator){
            resultOfOperationStates = stateA && stateB;
        }else if(nowOperator == Operator.orOperator){
            resultOfOperationStates = stateA || stateB;
        }else if(nowOperator == Operator.xorOperator){
            resultOfOperationStates = stateA ^ stateB;
        }



        if(resultOfOperationStates){
            onPositive.Invoke();
        }else{
            onNegative.Invoke();
        }
        #if UNITY_EDITOR
            OnInspectorUpdate();
        #endif
    }


    public void setStateToPositiveA(){
        stateA = true;
    }
    public void setStateToNegativeA(){
        stateA = false;
    }


    public void setStateToPositiveB(){
        stateB = true;
    }
    public void setStateToNegativeB(){
        stateB = false;
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
