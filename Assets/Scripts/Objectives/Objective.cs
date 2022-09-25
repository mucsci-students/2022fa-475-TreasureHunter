using System;
using UnityEngine;

[Serializable]
public struct Objective
{

    public string Description;
    public AudioClip ResolveSound;
    public bool IsResolved;
    public int MinProgressToResolve;
    public int CurrentProgress;

}
