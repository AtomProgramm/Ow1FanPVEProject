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
        if(player.hero.hp != lastHp){
            lastHp = player.hero.hp; 
            sliderHp.value = player.hero.hp / player.hero.maxHealth;
        }
    }
}
