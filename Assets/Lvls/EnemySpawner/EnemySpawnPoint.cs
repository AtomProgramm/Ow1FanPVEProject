using System.Security.Authentication;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [DescriptionAttribute("if true spawns be only out of see any player")]
    public bool spawnOnlyOutOfSight = true;

    [DescriptionAttribute("Before spawn check is can physics spawn (is we have space to spawn - is't we have walls or other collider in spawn place)")]
    public bool processPhysicsPossibility = true;

    [DescriptionAttribute("Try to sleep next to the point if some condition does not allow")]
    public bool tryNearSpawn = true;
    public int maxNumberAttempts = 10;

    [DescriptionAttribute("if true, the enemies will spawn in the direction of the nearest player, if false, they will spawn with the same angle of rotation as the point.")]
    public bool spawnFacingToNearsPlayer = true;


    void Start(){}
    void Update(){}

    public bool isPlayersSeePoint(Vector3 pointToCheck){
        bool isSee = false;
        foreach(var pl in MissionController.instance.playersOnMission){
            float degreesCanSee = pl.FPSController.playerCamera.fieldOfView;
            if(Vector3.Angle(pl.transform.forward, pointToCheck - pl.transform.position) > degreesCanSee){
                continue;
            }else{
                Ray r = new Ray(pointToCheck, pl.transform.position - pointToCheck); 
                RaycastHit hit;
                if(Physics.Raycast(r,out hit)){
                    if(hit.collider.transform == pl.transform){
                        isSee = true;
                        break;
                    }
                }
            }
        }
        return isSee;
    }
    public bool isPlayersSeePoint(){
        return isPlayersSeePoint(transform.position);
    }





    public bool isPointPhysicsPossible(Vector3 pointToCheck, Vector3 toSize){
        // Debug.DrawRay(r);
        Ray r = new Ray(pointToCheck + new Vector3(toSize.x / 2, 0, 0), pointToCheck + new Vector3(toSize.x / -2, 0, 0)); 
        // Debug.DrawRay(pointToCheck + new Vector3(toSize.x / 2, 0, 0), pointToCheck + new Vector3(toSize.x / -2, 0, 0),Color.black, 10f);
        if(Physics.Raycast(r)) return false;
        r = new Ray(pointToCheck + new Vector3(0, toSize.y / 2, 0), pointToCheck + new Vector3(0, toSize.y / -2, 0)); 
        // Debug.DrawRay(pointToCheck + new Vector3(0, toSize.y / 2, 0), pointToCheck + new Vector3(0, toSize.y / -2, 0),Color.black, 10f);
        if(Physics.Raycast(r)) return false;
        r = new Ray(pointToCheck + new Vector3(0, 0, toSize.z / 2), pointToCheck + new Vector3(0, 0, toSize.z / -2)); 
        // Debug.DrawRay(pointToCheck + new Vector3(0, 0, toSize.z / 2), pointToCheck + new Vector3(0, 0, toSize.z / -2),Color.black, 10f);
        if(Physics.Raycast(r)) return false;
        return true;
    }
    public bool isPointPhysicsPossible(Vector3 pointToCheck){
        return isPointPhysicsPossible(pointToCheck, Vector3.one);
    }


    // private Vector3 getPossitionAcross(int counter){
    //     return
    // }
    public Vector3? tryGetNearPhysicsPositionToSpawn(Vector3 pointToCheckStart, Vector3 toSize){
        if(isPointPhysicsPossible(pointToCheckStart, toSize)){
            return pointToCheckStart;
        }
        Vector3 pointToCheck = pointToCheckStart;
        int attemptsCounter = maxNumberAttempts;
        int nowSquareSize = 1;
        int xCycleCounter = 0;
        while(attemptsCounter > 0){
            int xM = (attemptsCounter % (nowSquareSize * 2)) - nowSquareSize;
            if(xM == 0) xCycleCounter = xCycleCounter + 1;
            int yM = (xCycleCounter % (nowSquareSize * 2)) - nowSquareSize;
            pointToCheck = new Vector3(pointToCheckStart.x + (toSize.x * xM), pointToCheckStart.y, pointToCheckStart.z + (toSize.z * yM));

            if(isPointPhysicsPossible(pointToCheck, toSize)){
                return pointToCheck;
            }

            if(yM == 0) nowSquareSize = nowSquareSize + 1;
            attemptsCounter = attemptsCounter - 1;
        }
        return null;
    }
    public List<Vector3> tryGetNearPhysicsPositionsToSpawn(Vector3 pointToCheckStart, Vector3 toSize, int times = 1, int attemptsCount = 10){
        var r = new List<Vector3>();
        if(isPointPhysicsPossible(pointToCheckStart, toSize)){
            r.Add(pointToCheckStart);
            times = times - 1;
        }
        Vector3 pointToCheck = pointToCheckStart;
        int attemptsCounter = attemptsCount;
        int nowSquareSize = 1;
        int xCycleCounter = 0;
        while((attemptsCounter > 0) && (times > 0)){
            int xM = (attemptsCounter % (nowSquareSize * 2)) - nowSquareSize;
            if(xM == 0) xCycleCounter = xCycleCounter + 1;
            int yM = (xCycleCounter % (nowSquareSize * 2)) - nowSquareSize;
            pointToCheck = new Vector3(pointToCheckStart.x + (toSize.x * xM), pointToCheckStart.y, pointToCheckStart.z + (toSize.z * yM));

            if(isPointPhysicsPossible(pointToCheck, toSize)){
                r.Add(pointToCheckStart);
                times = times - 1;
            }else{
                attemptsCounter = attemptsCounter - 1;
            }

            if(yM == 0) nowSquareSize = nowSquareSize + 1;
        }
        return r;
    }



    public bool trySpawnEnemy(GameObject prefabOfEnemy){
        Vector3? pointToSpawnNow = transform.position;
        Quaternion facingToSpawnNow = transform.rotation;
        if(spawnOnlyOutOfSight){
            if( isPlayersSeePoint()){
                // if(tryNearSpawn){
                //     // // pointToSpawnNow = tryGetNearPositionToSpawn(transform.position, prefabOfEnemy.GetComponent<Enemy>())
                //     // pointToSpawnNow = tryGetNearPositionToSpawn((Vector3)pointToSpawnNow, Vector3.one);
                // }
                return false;
            }
        }
        if(processPhysicsPossibility){
            if(! isPointPhysicsPossible((Vector3)pointToSpawnNow)){
                if(tryNearSpawn){
                    pointToSpawnNow = tryGetNearPhysicsPositionToSpawn((Vector3)pointToSpawnNow, Vector3.one);
                    if(pointToSpawnNow == null){
                        return false;
                    }
                }else{
                    return false;
                }
            }
        }
        if(spawnFacingToNearsPlayer){
            var playersTmp = MissionController.instance.playersOnMission;
            playersTmp.Sort((a, b) => Vector3.Distance(a.transform.position, (Vector3)pointToSpawnNow).CompareTo(Vector3.Distance(b.transform.position, (Vector3)pointToSpawnNow)));
            facingToSpawnNow.SetLookRotation(playersTmp[0].transform.position, (Vector3)pointToSpawnNow);
        }

        Instantiate(prefabOfEnemy,(Vector3)pointToSpawnNow,facingToSpawnNow);
        return true;
    }
    public int trySpawnSomeEnemy(GameObject prefabOfEnemy, int count){
        List<Vector3> pointsToSpawnNow = tryGetNearPhysicsPositionsToSpawn(transform.position, Vector3.one, count);
        int countBadPoints = 0;
        Quaternion facingToSpawnNow = transform.rotation;
        foreach(var p in pointsToSpawnNow){
            if(spawnOnlyOutOfSight){
                if(isPlayersSeePoint()){
                    countBadPoints = countBadPoints + 1;
                    continue;
                }
            }
            if(spawnFacingToNearsPlayer){
                var playersTmp = MissionController.instance.playersOnMission;
                playersTmp.Sort((a, b) => Vector3.Distance(a.transform.position, p).CompareTo(Vector3.Distance(b.transform.position, p)));
                facingToSpawnNow.SetLookRotation(playersTmp[0].transform.position, p);
            }
            if(!tryNearSpawn){
                Instantiate(prefabOfEnemy, transform.position, facingToSpawnNow);
            }else{
                Instantiate(prefabOfEnemy, p, facingToSpawnNow);
            }

        }
        return pointsToSpawnNow.Count - countBadPoints;
    }
    public void forceSpawnEnemy(GameObject prefabOfEnemy){
        Quaternion facingToSpawnNow = transform.rotation;
        if(spawnFacingToNearsPlayer){
            var playersTmp = MissionController.instance.playersOnMission;
            playersTmp.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
            facingToSpawnNow.SetLookRotation(playersTmp[0].transform.position, transform.position);
        }
        Instantiate(prefabOfEnemy, transform.position, facingToSpawnNow);
    }

}
