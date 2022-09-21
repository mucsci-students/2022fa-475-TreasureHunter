using UnityEngine;

public class Gun : MonoBehaviour
{

    public ProjectileBase Projectile;
    public float ChargeDelaySeconds = 0.5f;

    private float _chargeTimer;
    private AudioSource _gunshot;

    public void Start()
    {
        
        _chargeTimer = ChargeDelaySeconds;
        _gunshot = GetComponent<AudioSource>();

    }

    public void Update()
    {
        _chargeTimer += Time.deltaTime;
    }

    public void Shoot()
    {

        if (_chargeTimer >= ChargeDelaySeconds)
        {

            if (_gunshot is not null) { _gunshot.Play(); }

            _chargeTimer = 0;
            bool isGunMirrored = transform.localScale.x < 0;
            ProjectileBase bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
            bullet.Fire(isGunMirrored, gameObject.transform.parent.gameObject);

        }

    }

}
