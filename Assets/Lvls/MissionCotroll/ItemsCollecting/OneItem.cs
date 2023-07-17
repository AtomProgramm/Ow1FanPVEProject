using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OneItem : MonoBehaviour
{
    public CollectableGroup gr;
    public List<Collider> inTrigger = new List<Collider>();

    #if UNITY_EDITOR
        [Header("InEditor")]
        public Color toItemGroup = Color.black;
    #endif



    void Awake()
    {
        gr.regNeedCollect();   
    }


    void Start(){}


    void OnTriggerEnter(Collider other)
    {
        inTrigger.Add(other);
        if(other.GetComponent<Player>() != null){
            gr.regCollect();
            Destroy(gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {        
        inTrigger.Remove(other);
    }

    

    #if UNITY_EDITOR
        void Update()
        {
            OnInspectorUpdate();
        }
        private void OnInspectorUpdate() {

            Debug.DrawLine(transform.position, gr.transform.position, toItemGroup);
        }
    #endif
}
