using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float InitialVelocity = 100;
    public float Damage = 33;
    public float ProjectileLifetime = 3;

    private Rigidbody2D _rigidBody;
    private float _targetVelocity = 0;
    private GameObject _instigator;

    // Start is called before the first frame update
    void Start()
    {

        _rigidBody = GetComponent<Rigidbody2D>();
        
        if (ProjectileLifetime > 0) { Destroy(gameObject, ProjectileLifetime); }

    }

    private void Update() { _rigidBody.velocity = new Vector2(_targetVelocity, 0); }

    public virtual void Fire(bool isBackwardsBullet, GameObject instigator)
    {

        _instigator = instigator;
        float scaleModifier = (isBackwardsBullet ? -1 : 1);
        
        Vector3 bulletScale = transform.localScale;
        bulletScale.x *= scaleModifier;
        transform.localScale = bulletScale;

        _targetVelocity = InitialVelocity * scaleModifier;
        (GetComponent<Collider2D>()).enabled = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        GameObject hit = other.gameObject;
        if (hit != _instigator)
        {

            IDamageable damageComponent = hit.GetComponent<IDamageable>();
            if (damageComponent is not null)
            {
                damageComponent.ApplyDamage(_instigator, Damage);
            }

            Destroy(gameObject);

        }

    }

}
