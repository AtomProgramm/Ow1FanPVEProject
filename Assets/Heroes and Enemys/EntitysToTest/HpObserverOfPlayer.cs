using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpObserverOfPlayer : MonoBehaviour
{
    public Player player;
    public Slider sliderHp;

    private float lastHp = -1;
    void Start()
    {
        
    }

    void Update()
    {
        if(player.hp != lastHp){
            lastHp = player.hp; 
            sliderHp.value = player.hp / player.hero.maxHealth;
        }
    }
}
