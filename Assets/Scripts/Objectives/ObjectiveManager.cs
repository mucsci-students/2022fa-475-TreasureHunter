using System;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public Objective[] Objectives;

    public bool AllObjectivesResolved { 
        
        get 
        {

            bool allResolved = true;
            foreach(Objective obj in Objectives)
            {

                if (!obj.IsResolved) 
                {

                    allResolved = false;
                    break;

                }

            }

            return allResolved;

        } 

    }

    public event EventHandler OnAllObjectivesResolved;

    public void AddProgressTo (int objectiveNumber, int progressToAdd)
    {

        Objective targetObjective = Objectives[objectiveNumber];
        
        // Don't do anything with an already resolved objective
        if (targetObjective.IsResolved) { return; } 

        if ((targetObjective.CurrentProgress += progressToAdd) >= targetObjective.MinProgressToResolve)
        {

            targetObjective.IsResolved = true;
            AudioSource.PlayClipAtPoint(targetObjective.ResolveSound, transform.position);

            print($"Completed objective #{objectiveNumber}: \"{targetObjective.Description}\"");

        }

        Objectives[objectiveNumber] = targetObjective;
        if (AllObjectivesResolved)
        {

            print("All objectives resolved!");
            OnAllObjectivesResolved?.Invoke(this, EventArgs.Empty);

        }

    }

}
