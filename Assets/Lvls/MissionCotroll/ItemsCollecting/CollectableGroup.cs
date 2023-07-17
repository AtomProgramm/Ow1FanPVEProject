using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class CollectableGroup : MonoBehaviour
{
    [Header("Actions")]
    public UnityEvent OnAllCollected;
    public UnityEvent OnOneCollected;


    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color doOnCollect = Color.green;
    #endif

    private int needToCollect = 0;
    private int collected = 0;



    void Start(){}







    public void regCollect(){
        collected = collected + 1;
        OnOneCollected.Invoke();
        if(collected >= needToCollect){
            OnAllCollected.Invoke();
        }
    }
    public void regNeedCollect(){
        needToCollect = needToCollect + 1;
    }



    #if UNITY_EDITOR
        void Update()
        {
            OnInspectorUpdate();
        }
        private void OnInspectorUpdate() {

            for(var ind = 0; ind < OnAllCollected.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnAllCollected.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, doOnCollect);
            }   
            for(var ind = 0; ind < OnOneCollected.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnOneCollected.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, doOnCollect);
            }    
        }
    #endif
}
