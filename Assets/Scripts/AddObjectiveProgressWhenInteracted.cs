using UnityEngine;

public class AddObjectiveProgressWhenInteracted : MonoBehaviour
{
    
    public int ObjectiveNumberToResolve;
    public int ProgressToAdd;
    private ObjectiveManager _objectiveManager;

    void Start()
    {

        _objectiveManager = FindObjectOfType<ObjectiveManager>();
        GetComponent<IInteractable>().OnInteracted += (_, _) => {

            _objectiveManager.AddProgressTo(ObjectiveNumberToResolve, ProgressToAdd);

        };

    }

}
