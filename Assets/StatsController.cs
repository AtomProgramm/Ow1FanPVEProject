using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController inst;

    // public MissionController missionController;



    public int totalScorePoints;


    public int lastMatchScore;
    public float lastMatchTime;
    // public int lastMatchKills;
    // public int lastMatchDeath;
    // // public float lastMatchAim;
    // // public int lastMatchDamage;
    public int lastMatchResultOfMatch;
    public int lastMatchWaves;

    public HeroBehaviors lastMatchHeroOf;
    public HeroBehaviors lastMatchHeroesOfCommands;



    void Start(){
        inst = this;
    }
    void Update(){
        if(MissionController.instance != null){

        }
    }



    public void saveValues(){
        PlayerPrefs.SetInt(ConstantsContainer.keyTotalScorePoints,                 totalScorePoints          );
             
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchScore,                   lastMatchScore            );
        PlayerPrefs.SetFloat(ConstantsContainer.keyLastMatchTime,                  lastMatchTime             );
        // PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchKills,                   lastMatchKills            );
        // PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchDeath,                   lastMatchDeath            );
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchResultOfMatch,           lastMatchResultOfMatch    );
        PlayerPrefs.SetInt(ConstantsContainer.keyLastMatchWaves,                   lastMatchWaves            );
    }

    public void loadValues(){
        totalScorePoints =       PlayerPrefs.GetInt(ConstantsContainer.keyTotalScorePoints,                 0);

        lastMatchScore =         PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchScore,                   0);
        lastMatchTime =          PlayerPrefs.GetFloat(ConstantsContainer.keyLastMatchTime,                  0);
        // lastMatchKills =         PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchKills,                   0);
        // lastMatchDeath =         PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchDeath,                   0);
        lastMatchResultOfMatch = PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchResultOfMatch,           0);
        lastMatchWaves =         PlayerPrefs.GetInt(ConstantsContainer.keyLastMatchWaves,                   0);
    }
}