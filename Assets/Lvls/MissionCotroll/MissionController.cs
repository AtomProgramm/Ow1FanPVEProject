using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MissionController : MonoBehaviour
{
    public static MissionController instance;

    public List<GameObject> playersStartPositions = new List<GameObject>();
    public List<Player> playersPrefabs = new List<Player>();


    [Space(5)]
    public List<Player> playersOnMission = new List<Player>();
    public List<Enemy> enemyOnMission = new List<Enemy>(); //tmp?


    void Start(){ 
        instance = this;
        for(int ind=0; ind < playersPrefabs.Count; ind = ind + 1){
            playersOnMission.Add(Instantiate(playersPrefabs[ind],playersStartPositions[ind].transform.position, playersStartPositions[ind].transform.rotation).GetComponent<Player>());
        }
        StatsController.inst.nullValues();
    }
    void Update(){
        var isNowDefeat = false;
        foreach(var plNow in playersOnMission){
            if(! plNow.injured){
                isNowDefeat = false;
                break;
            }else{
                isNowDefeat = true;
            }
        }
        if(isNowDefeat){
            defeatMission();
        }
    }

    
    public void wonMission(){
        StatsController.inst.lastMatchResultOfMatch = 1;
        foreach(var plNow in playersOnMission){
            plNow.playWon();
        }
    }
    public void defeatMission(){
        StatsController.inst.lastMatchResultOfMatch = -1;
        foreach(var plNow in playersOnMission){
            plNow.playDefeat();
        }
    }

}