using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController inst;


    public int totalScorePoints;


    public int lastMatchScore;
    public float lastMatchTime;
    public int lastMatchKills;
    public int lastMatchDeath;
    public int lastMatchHits;
    public int lastMatchShoots;
    public float lastMatchDamage;
    public int lastMatchResultOfMatch;
    public int lastMatchWaves;
    public Sprite lastMatchHeroOf;
    public List<Sprite> lastMatchHeroesOfCommands;



    void Start(){
        inst = this;
        loadValues();
    }
    // void Update(){
    //     if(MissionController.instance != null){

    //     }
    // }



    public void saveValues(){
        PlayerPrefs.SetInt(ConstantsContainer.keyTotalScorePoints,                 totalScorePoints          );
             

        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchScore,                  lastMatchScore);
        PlayerPrefs.SetFloat(ConstantsContainer.keyLastMatchTime,                 lastMatchTime);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchKills,                  lastMatchKills);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchDeath,                  lastMatchDeath);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchHits,                   lastMatchHits);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchShoots,                 lastMatchShoots);
        PlayerPrefs.SetFloat(ConstantsContainer.keyLastMatchDamage,               lastMatchDamage);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchResultOfMatch,          lastMatchResultOfMatch);
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchWaves,                  lastMatchWaves);
    }

    public void loadValues(){
        totalScorePoints =       PlayerPrefs.GetInt(ConstantsContainer.keyTotalScorePoints,                 0);


        lastMatchScore =                PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchScore,                  0);
        lastMatchTime =                 PlayerPrefs.GetFloat(ConstantsContainer.keyLastMatchTime,                 0);
        lastMatchKills =                PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchKills,                  0);
        lastMatchDeath =                PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchDeath,                  0);
        lastMatchHits =                 PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchHits,                   0);
        lastMatchShoots =               PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchShoots,                 0);
        lastMatchDamage =               PlayerPrefs.GetFloat(ConstantsContainer.keyLastMatchDamage,               0);
        lastMatchResultOfMatch =        PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchResultOfMatch,          0);
        lastMatchWaves =                PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchWaves,                  0);
    }

    public void nullValues(){
        totalScorePoints = 0;
        lastMatchScore = 0;
        lastMatchTime = 0;
        lastMatchKills = 0;
        lastMatchDeath = 0;
        lastMatchHits = 0;
        lastMatchShoots = 0;
        lastMatchDamage = 0;
        lastMatchResultOfMatch = 0;
        lastMatchWaves = 0;
    }
    public void countNewWave(){
        lastMatchWaves = lastMatchWaves + 1;
        saveValues();
    }
}