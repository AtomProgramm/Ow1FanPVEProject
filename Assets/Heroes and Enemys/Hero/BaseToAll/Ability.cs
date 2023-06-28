using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public GameObject uiSet;
    [Space(10)]
    public int countToUse = 1;
    public float coolDown;


    protected float coolDownTimer;
    protected bool coolDownNow;



    public abstract void execute();


    void Start(){}
    void Update(){}
}
