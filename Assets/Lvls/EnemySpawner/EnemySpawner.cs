using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float difficultyNow = 0;

    [Header("Independent spawn")]
    public bool doIndependent = true; 
    public float nowTimer = -1;
    public int indexNowStep = 0;
    public bool loopSpawnList = false;
    public List<StepSpawn> spawnSteps;



    void Start()
    {
        nowTimer = spawnSteps[indexNowStep].timerBefore;
    }

    void Update()
    {
        if(!loopSpawnList){
            if(indexNowStep >= spawnSteps.Count){
                doIndependent = false;
            }
        }

        if(doIndependent){
            nowTimer = nowTimer - Time.deltaTime;
            if(nowTimer < 0){
                SpawnNowStepAndForceStartNewStep();
            } 
        }
        
    }

    private void spawnOneContainer(OneTypeContainer toSpawn, List<EnemySpawnPoint> points){
        bool canSpawn = true;
        canSpawn = canSpawn && (toSpawn.minDifficulty < difficultyNow);
        canSpawn = canSpawn && ((toSpawn.maxDifficulty < 0) || (toSpawn.maxDifficulty > difficultyNow));
        if(canSpawn){
            int rCountToSpawn = toSpawn.countToSpawn + (Mathf.RoundToInt(toSpawn.addCountPerOneDifficulty * Mathf.Max(0, (difficultyNow - toSpawn.minDifficulty))));
            foreach(var p in points){
                if(rCountToSpawn <= 0){
                    break;
                }
                rCountToSpawn = rCountToSpawn - p.trySpawnSomeEnemy(toSpawn.prefabOfEnemyType, toSpawn.countToSpawn);
            }
        }

    }

    [SerializableAttribute]
    public class OneTypeContainer{
        [SerializeField]
        public GameObject prefabOfEnemyType;
        public int countToSpawn = 1;
        [SerializeField]
        public float minDifficulty = -1;
        [SerializeField]
        public float maxDifficulty = -1;
        [SerializeField]
        public float addCountPerOneDifficulty = 0;
    }

    [SerializableAttribute]
    public class StepSpawn{
        [SerializeField]
        public List<OneTypeContainer> types;
        [SerializeField]
        public List<EnemySpawnPoint> points;
        [SerializeField]
        public float timerBefore = 1;
    }




    public void startIndependent(){
        doIndependent = true;
    }
    public void onlySpawnNowStep(){
        foreach(var typeInStep in spawnSteps[indexNowStep].types){
            spawnOneContainer(typeInStep, spawnSteps[indexNowStep].points);
        }
    }
    public void forceStartNewStep(){
        indexNowStep  = indexNowStep + 1;
        indexNowStep = indexNowStep % spawnSteps.Count;
        nowTimer = spawnSteps[indexNowStep].timerBefore;
    }
    public void SpawnNowStepAndForceStartNewStep(){
        onlySpawnNowStep();
        forceStartNewStep();
    }

    public void increaseDifficultly(float toAdd = 1){
        difficultyNow  = difficultyNow + toAdd;
    }



    // editor interface (show to points coupling)
}
