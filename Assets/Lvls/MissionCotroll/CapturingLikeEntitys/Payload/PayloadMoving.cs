using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadMoving : MonoBehaviour
{
    public LineRenderer path;
    public CapturablePoint counterMoving;
    



    void Start() {
    }
    void Update() {
        if(counterMoving.nowCapturedSize > 100){
            var pEnd = path.GetPosition(path.positionCount -1);
            transform.position = pEnd + path.transform.position;
            return;
        }
        var partOfPath = (counterMoving.nowCapturedSize / 100f);
        var indClosedPoint = path.positionCount * partOfPath;
        var p1 = path.GetPosition((int)indClosedPoint);
        var p2 = path.GetPosition((int)indClosedPoint+1);
        
        transform.position = Vector3.Lerp(p1, p2, (indClosedPoint - ((int)indClosedPoint))) + path.transform.position;
        var tmpOldRotationToSmooth = transform.rotation;
        transform.LookAt(p2+path.transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, tmpOldRotationToSmooth, 0.5f);
    }
}
