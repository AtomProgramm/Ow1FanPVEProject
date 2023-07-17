using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class EnemyStateObserver : MonoBehaviour
{
    public static List<EnemyStateObserver> instances;
    public List<GameObject> allEnemy;
    

    public int toCountDecreasingEnemy = 0;
    [Header("Actions")]
    public UnityEvent OnGainMinCount;
    public UnityEvent OnNotGainMinCount;

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color colorOnGain = Color.green;
        public Color colorOnNotGain = Color.red;
    #endif
    void Start()
    {
        #if UNITY_EDITOR
            OnInspectorUpdate();
            if(!Application.isPlaying ){
                return;
            }
        #endif
        if(instances == null) instances = new List<EnemyStateObserver>();
        instances.Add(this);
    }

    void Update()
    {
        #if UNITY_EDITOR
            OnInspectorUpdate();
            if(!Application.isPlaying ){
                return;
            }
        #endif
        if(allEnemy.Count <= toCountDecreasingEnemy){
            OnGainMinCount.Invoke();
        }else{
            OnNotGainMinCount.Invoke();
        }
    }
    #if UNITY_EDITOR
        private void OnInspectorUpdate(){
            for(var ind = 0; ind < OnGainMinCount.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnGainMinCount.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorOnGain);
            }
            for(var ind = 0; ind < OnNotGainMinCount.GetPersistentEventCount(); ind = ind + 1 )
            {
                var nowTarget = OnNotGainMinCount.GetPersistentTarget(ind);
                Debug.DrawLine(transform.position, ((MonoBehaviour)nowTarget).transform.position, colorOnNotGain);
            }
        }
    #endif
}
