using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFireAIController : TimedActionAIController
{
    // Start is called before the first frame update
    void Start()
    {

        Init();

        // Turn left
        Invoke(nameof(TurnLeft), 2);
        OnActionRequested += (_, _) =>
        {

            _controlledPlayer.PrimaryAttack();

        };

    }

    void TurnLeft () => _controlledPlayer.Walk(-1);

}
