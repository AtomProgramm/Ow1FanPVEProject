using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelfDestroyer : MonoBehaviour
{
    public float timeToDestroy = 1;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
