using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    private Transform Center;
    private Transform High;
    private Transform Low;

    public float ViewDistance = 20;
    public int LayerToIgnore = 3;

    // Start is called before the first frame update
    void Start()
    {
        
        Center = transform.Find(nameof(Center));
        Low = transform.Find(nameof(High));
        High = transform.Find(nameof(Low));

    }

    public ICollection<(GameObject HitObject, float Distance)> GetPercievedObjects()
    {

        float effectiveViewDistance = ViewDistance;
        IActionPlayer rootPlayer = gameObject.transform.parent.gameObject.GetComponent<IActionPlayer>();
        if (rootPlayer != null && rootPlayer.IsFlipped()) { effectiveViewDistance *= -1; }

        RaycastHit2D centerResult = Physics2D.Raycast(Center.position, new Vector2(1, 0), effectiveViewDistance, LayerToIgnore);
        RaycastHit2D lowResult = Physics2D.Raycast(Low.position, transform.forward, effectiveViewDistance, LayerToIgnore);
        RaycastHit2D highResult = Physics2D.Raycast(High.position, transform.forward, effectiveViewDistance, LayerToIgnore);

        (GameObject HitObject, float Distance)[] result = new (GameObject HitObject, float Distance)[] { 
        
            (centerResult.transform?.gameObject, centerResult.distance),
            (lowResult.transform?.gameObject, lowResult.distance),
            (highResult.transform?.gameObject, highResult.distance),

        };

        // Filter out misses and hits on self (this is 500x more clean to do in Unreal Engine)
        return result.Where((hit) => hit.HitObject != null).ToArray();
        
    }

    public ICollection<(GameObject HitObject, float Distance)> GetPercievedObjects<TScript>()
        => GetPercievedObjects().Where((item) => item.HitObject.GetComponent<TScript>() != null).ToArray();

}
