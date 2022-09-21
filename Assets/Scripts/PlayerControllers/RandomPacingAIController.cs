using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPacingAIController : TimedActionAIController
{
    
    // Start is called before the first frame update
    void Start()
    {

        float stick = 1;

        Init();
        OnActionRequested += (_, _) => 
        {
            _controlledPlayer.Walk(stick *= -1);
        };

    }

}
