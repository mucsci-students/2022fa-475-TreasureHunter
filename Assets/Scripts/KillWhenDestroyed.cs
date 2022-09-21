using UnityEngine;

public class KillWhenDestroyed : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

        (GetComponent<IDamageable>()).OnDestroyed += (_, _) =>
        {
            Destroy(gameObject);
        };
        
    }

}
