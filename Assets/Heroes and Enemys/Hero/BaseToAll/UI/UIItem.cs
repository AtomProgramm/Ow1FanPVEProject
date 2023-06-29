using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIItem : MonoBehaviour
{
    public Vector2 positionOnScreen;
    
    void Start(){}
    void Update(){}


    abstract public void setValueOfThisUI(float val);
}
