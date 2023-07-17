using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class TimerExecution : MonoBehaviour
{
    public bool loop = false;
    public float time = 20;
    [Header("Actions")]
    public UnityEvent OnTimeExit;

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color colorEventDo = Color.green;
    #endif

    private float timer;
    
    void Start()
    {
        timer = time;
    }

    void Update()
    {
        #if UNITY_EDITOR
            OnInspectorUpdate();
            if(!Application.isPlaying ){
            // if(Application.isEditor){
                return;
            }
        #endif
        timer = timer - Time.deltaTime;
        // print(timer);
        if(timer < 0){
            OnTimeExit.Invoke();
            if(loop){
                timer = time;
            }

        }
    }


    #if UNITY_EDITOR
        private void OnInspectorUpdate() {
            for(var ind = 0; ind < OnTimeExit.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnTimeExit.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorEventDo);
            }      
        }
    #endif
}
