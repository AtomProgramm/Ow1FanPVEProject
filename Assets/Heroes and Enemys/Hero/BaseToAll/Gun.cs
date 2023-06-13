using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject shootEffectPrefab;
    public GameObject altShootEffectPrefab;

    public Animator animatorOfGun;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
           shoot();
        }   
        if(Input.GetKeyDown(KeyCode.Mouse1)){
           altShoot();
        }   
    }

    public void shoot(){
        Debug.Log("shoot");
        
    }
    public void altShoot(){
        Debug.Log("alt shoot");
    }
}
