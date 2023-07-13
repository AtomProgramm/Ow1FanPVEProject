using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerMaterial : MonoBehaviour
{
    public Color colorA = Color.blue;
    public Color colorB = Color.red;
    private MeshRenderer r;
 
    void Start()
    {
        r = GetComponent<MeshRenderer>();
        setAColor();
    }


 void Update() {}

    public void setAColor(){
        r.material.color = colorA;
    }
    public void setBColor(){
        r.material.color = colorB;
    }
}
