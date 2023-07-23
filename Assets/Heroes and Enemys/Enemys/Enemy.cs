using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : HittableEntity
{
    public HittableEntity lastBittedBy;
    public abstract void regSelfSummon();
    public abstract void regSelfDel();
}
