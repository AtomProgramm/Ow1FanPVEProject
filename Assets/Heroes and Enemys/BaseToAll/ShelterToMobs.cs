using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterToMobs : MonoBehaviour
{
    public static List<ShelterToMobs> allShelters;
    public static List<ShelterToMobs> completelyFree;
    public static List<ShelterToMobs> notFree;
    public static List<ShelterToMobs> occupied;
    public static List<ShelterToMobs> inPlayerView;


    public float updateStatusPeriod = 2f;
    public GameObject occupiedBy = null;

    float updateTimer;


    void Start()
    {
        if(allShelters == null) allShelters = new List<ShelterToMobs>();
        if(completelyFree == null) completelyFree = new List<ShelterToMobs>();
        if(notFree == null) notFree = new List<ShelterToMobs>();
        if(occupied == null) occupied = new List<ShelterToMobs>();
        if(inPlayerView == null) inPlayerView = new List<ShelterToMobs>();

        allShelters.Add(this);
        doUpdateStatus();
        updateTimer = updateStatusPeriod; 
    }


    public void setOccupied(GameObject by){
        occupiedBy = by;
    }
    public void setDeOccupied(GameObject by){
        if(occupiedBy == by){
            occupiedBy = null;
        }else{
            throw new System.Exception("DeOccupied can only by who occupied");
        }
    }
    // public void setDeOccupied(){ occupiedBy = null; } // idk need check who try deOccupied?
    


    void Update()
    {
        if(updateTimer < 0){
            doUpdateStatus();
            updateTimer = updateStatusPeriod; 
        }else{
            updateTimer = updateTimer - Time.deltaTime;
        }
        
    }


    void doUpdateStatus(){
        bool isCompletelyFree = true;

        if(checkOccupied()){
            isCompletelyFree = false;
            if(!occupied.Contains(this)) occupied.Add(this);
        }else{
            if(occupied.Contains(this)) occupied.Remove(this);
        }
        if(checkInPlayerView()){
            isCompletelyFree = false;
            if(!inPlayerView.Contains(this)) inPlayerView.Add(this);
        }else{
            if(inPlayerView.Contains(this)) inPlayerView.Remove(this);
        }

        if(isCompletelyFree){
            if(notFree.Contains(this)) notFree.Remove(this);
            if(!completelyFree.Contains(this)) completelyFree.Add(this);
        }else{
            if(!notFree.Contains(this)) notFree.Add(this);
            if(completelyFree.Contains(this)) completelyFree.Remove(this);
        }
    }
    bool checkOccupied(){
        return (occupied != null);
    }
    bool checkInPlayerView(){
        if(MissionController.instance == null) return false;
        var tmpProbablyCan = MissionController.instance.playersOnMission.FindAll(item => (Vector3.Angle((item.transform.position - transform.position), item.transform.forward) < item.FPSController.playerCamera.fieldOfView));
        foreach(var nowPlayer in tmpProbablyCan){
            RaycastHit  hit;
            Vector3 dir = nowPlayer.transform.position - transform.position;
            Vector3 from = nowPlayer.FPSController.playerCamera.transform.position + nowPlayer.transform.forward;
            if(Physics.Raycast(from, dir, out hit)){ 
                if ((hit.point - from).magnitude > (transform.position - from).magnitude){ // shelter have no collider - if distance to hit bigger than to shelter mean itInPlayer view because raycast shot throw 
                    return true;
                }
            }
        }
        return false;
    }
}
