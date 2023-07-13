using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class CapturablePoint : MonoBehaviour
{
    [Header("Capture values")]
    [Range(0,100f)]
    public float nowCapturedSize = 0f;
    public float speedCapture = 0.5f;
    [Space(2)]
    public float SpeedAdditionPerPlayer = 0;
    public float SpeedMultiplyPerPlayer = 0;
    public int maxCountPlayerToIncreaseSpeed = 0;
    [Space(2)]
    public float lessingCapturedSpeed = 0.2f;
    public float timeToStartLessing = 10;
    private float timerStartLessing = 0; //-




    [Header("Other coupling")]
    public PlayerTrigger triggerOfThisPoint;


    [Header("Actions")]
    public UnityEvent OnCaptureEnd;
    public UnityEvent OnLessingCapture;
    public UnityEvent OnProcessCapturing;
    

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color colorEventDo = Color.green;
    #endif



    public void capturingCounting(){
        OnProcessCapturing.Invoke();
        timerStartLessing = timeToStartLessing;
        if(nowCapturedSize >= 100f){
            OnCaptureEnd.Invoke();
        }
        nowCapturedSize = nowCapturedSize + (Time.deltaTime * speedCapture);
        nowCapturedSize = nowCapturedSize + (((float)triggerOfThisPoint.getNowCountPlayerInTrigger()) * SpeedAdditionPerPlayer);
        nowCapturedSize = nowCapturedSize + (((float)triggerOfThisPoint.getNowCountPlayerInTrigger()) * SpeedMultiplyPerPlayer * nowCapturedSize);
    }
    public void captureLessCounting(){
        if((nowCapturedSize < 100f) && (timerStartLessing <= 0)){
            nowCapturedSize = Mathf.Max(nowCapturedSize - (Time.deltaTime * lessingCapturedSpeed), 0);
            OnLessingCapture.Invoke();
        }else{
            timerStartLessing = timerStartLessing - Time.deltaTime;
        }
    }


    void Start(){}  
    #if UNITY_EDITOR
        void Update()
        {
            OnInspectorUpdate();
        }
        private void OnInspectorUpdate() {
            for(var ind = 0; ind < OnCaptureEnd.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnCaptureEnd.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventDo);
            }   
            for(var ind = 0; ind < OnLessingCapture.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnLessingCapture.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventDo);
            }   
            for(var ind = 0; ind < OnProcessCapturing.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnProcessCapturing.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventDo);
            }    
        }
    #endif
}
