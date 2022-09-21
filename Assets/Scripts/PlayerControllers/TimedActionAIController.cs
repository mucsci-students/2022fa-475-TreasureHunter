using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimedActionAIController : MonoBehaviour
{

    protected IActionPlayer _controlledPlayer;

    public float DelayMin = 1;
    public float DelayMax = 5;

    protected event EventHandler OnActionRequested;

    protected void Init()
    {
        
        _controlledPlayer = GetComponent<IActionPlayer>();
        Run();

    }

    void Run()
    {

        OnActionRequested?.Invoke(this, EventArgs.Empty);
        Invoke(nameof(Run), Random.Range(DelayMin, DelayMax));

    }
}
