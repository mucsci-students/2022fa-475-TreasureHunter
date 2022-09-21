using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJumpAIController : TimedActionAIController
{
    
    // Start is called before the first frame update
    void Start()
    {

        Init();
        OnActionRequested += (_, _) =>
        {
            _controlledPlayer.Jump();
        };
        
    }

}
