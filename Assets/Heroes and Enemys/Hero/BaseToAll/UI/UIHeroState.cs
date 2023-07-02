using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHeroState : MonoBehaviour
{
    public Sprite spriteOfHero;
    public Image iconHero;
    public Slider hpBar;
    // public 
    // private float valHpNe to animation ch ...
    
    
    void Start() {
        iconHero.sprite = spriteOfHero;
     }
    void Update() { }

    public void setHealth(float hpNew, float hpMax = 200){
        hpBar.value = hpNew / hpMax;
    }
}
