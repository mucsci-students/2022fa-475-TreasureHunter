using UnityEngine;

public class AddObjectiveProgressWhenDestroyed : MonoBehaviour
{

    public int ObjectiveNumberToResolve;
    public int ProgressToAdd;
    private ObjectiveManager _objectiveManager;

    void Start()
    {

        _objectiveManager = FindObjectOfType<ObjectiveManager>();
        GetComponent<IDamageable>().OnDestroyed += (_, _) => {

            _objectiveManager.AddProgressTo(ObjectiveNumberToResolve, ProgressToAdd);

        };
        
    }

}
