using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultMatchShow : MonoBehaviour
{
    public float timeToTextAnimation = 1f; 

    public TMP_Text  resultOfMath;
    public TMP_Text  matchTime;
    public TMP_Text  Death;
    public TMP_Text  Damage;
    public TMP_Text  Kills;
    public TMP_Text  Aim;
    public TMP_Text  Waves;
    public TMP_Text  Score;
    

    void Start() {
        if(StatsController.inst.lastMatchResultOfMatch == 1){
            resultOfMath.text = "Victory";
        }
        if(StatsController.inst.lastMatchResultOfMatch == -1){
            resultOfMath.text = "Defeat";
        }
        if(StatsController.inst.lastMatchWaves > 0){
            resultOfMath.transform.gameObject.SetActive(false);
            Waves.transform.gameObject.SetActive(true);
        }else{
            resultOfMath.transform.gameObject.SetActive(true);
            Waves.transform.gameObject.SetActive(false);
        }
        StartCoroutine(mathTimeAnimation());
    }

    void Update() {}

    IEnumerator  mathTimeAnimation(){
        float nowAnimationVal = 0;
        float stepOnSec = StatsController.inst.lastMatchTime / timeToTextAnimation;
        while(nowAnimationVal < StatsController.inst.lastMatchTime){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            int seconds = ((int)nowAnimationVal) % 60;
            int minutes = ((int)nowAnimationVal) / 60;
            matchTime.text = "Match time - " + minutes.ToString() + ":" + seconds.ToString();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(deathAnimation());
    }
    IEnumerator  deathAnimation(){
        float nowAnimationVal = 0;
        float stepOnSec = StatsController.inst.lastMatchDeath / timeToTextAnimation;
        while(nowAnimationVal < StatsController.inst.lastMatchDeath){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            Death.text = "Death: "+((int)nowAnimationVal).ToString();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(damageAnimation());
    }
    IEnumerator  damageAnimation(){
        float nowAnimationVal = 0;
        float stepOnSec = StatsController.inst.lastMatchDamage / timeToTextAnimation;
        while(nowAnimationVal < StatsController.inst.lastMatchDamage){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            Damage.text = "Damage: "+((int)nowAnimationVal).ToString();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(killsAnimation());
    }
    IEnumerator  killsAnimation(){
        float nowAnimationVal = 0;
        float stepOnSec = StatsController.inst.lastMatchKills / timeToTextAnimation;
        while(nowAnimationVal < StatsController.inst.lastMatchKills){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            Kills.text = "Kills: "+((int)nowAnimationVal).ToString();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(aimAnimation());
    }
    IEnumerator  aimAnimation(){
        float rVal = ((float)StatsController.inst.lastMatchHits) / ((float)StatsController.inst.lastMatchShoots);
        float nowAnimationVal = 0;
        float stepOnSec = rVal / timeToTextAnimation;
        while(nowAnimationVal < rVal){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            Aim.text = "AimRate: "+ nowAnimationVal.ToString()[0]+ nowAnimationVal.ToString()[1]+ nowAnimationVal.ToString()[2];
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(wavesAnimation());
    }
    IEnumerator  wavesAnimation(){
        float nowAnimationVal = 0;
        if( StatsController.inst.lastMatchWaves > 0){
            float stepOnSec = StatsController.inst.lastMatchWaves / timeToTextAnimation;
            while(nowAnimationVal < StatsController.inst.lastMatchWaves){
                nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
                Waves.text = "Waves: "+((int)nowAnimationVal).ToString();
                yield return new WaitForEndOfFrame();
            }
        }else{
            Waves.transform.gameObject.SetActive(false);
        }
        StartCoroutine(scAnimation());
    }
    IEnumerator  scAnimation(){
        float nowAnimationVal = 0;
        // tmp
        if(StatsController.inst.lastMatchWaves > 0){
            float aim = ((float)StatsController.inst.lastMatchHits) / ((float)StatsController.inst.lastMatchShoots);
            StatsController.inst.lastMatchScore = Mathf.RoundToInt(StatsController.inst.lastMatchWaves * ((StatsController.inst.lastMatchDamage / 100f) * aim));
        }
        float stepOnSec = StatsController.inst.lastMatchScore / timeToTextAnimation;
        while(nowAnimationVal < StatsController.inst.lastMatchScore){
            nowAnimationVal = nowAnimationVal + (Time.deltaTime * stepOnSec);
            Score.text = "Score: "+((int)nowAnimationVal).ToString();
            yield return new WaitForEndOfFrame();
        }
    }
}
