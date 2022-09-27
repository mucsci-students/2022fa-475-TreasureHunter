using UnityEngine;

public class ActionPlayer : MonoBehaviour, IActionPlayer
{

    public float PlayerMovementSpeed = 10;
    public float JumpImpulseMagnitude = 5;

    public Camera PlayerCamera;
    public float CameraSlideOffset = 1.5f;
    public float CameraSlideSpeed = 10;
    
    public Vector3 _lastPosition = new(0, 0, 0);

    private Rigidbody2D _rigidBody;
    private float _cameraSlideTarget = 0;
    private Gun _gun;
    private IDamageable _damageable;

    private IInventory _inventoryComponent;

    private ActionPlayerAnimation playerAnimation;

    private Animator _animator;

    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {

        _inventoryComponent = GetComponent<IInventory>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _gun = GetComponentInChildren<Gun>();
        _damageable = GetComponentInChildren<IDamageable>();
        playerAnimation = GetComponent<ActionPlayerAnimation>();
        _sprite = GetComponent<SpriteRenderer>();
        if (_damageable is not null) {

            _damageable.OnDestroyed += (_, _) => { Destroy(gameObject); };
        
        }

        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentCameraLocation = PlayerCamera.transform.localPosition;
        currentCameraLocation.x = Mathf.Lerp(currentCameraLocation.x, _cameraSlideTarget, CameraSlideSpeed * Time.deltaTime);
        PlayerCamera.transform.localPosition = currentCameraLocation;

    }

    public void Jump()
    {

        // Only allow jumping if the player is grounded
        if (Mathf.Abs(_rigidBody.velocity.y) <= 0.01)
        {
            _rigidBody.AddForce(Vector2.up * JumpImpulseMagnitude, ForceMode2D.Impulse);
        }

    }

    public void PrimaryAttack()
        => _gun.Shoot();

    public void SecondaryAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Walk(float stick)
    {

        _animator.SetBool("isIdle", stick == 0);
        _animator.SetFloat("walkingStick", stick);
        
        if (stick < 0)
        {

            _cameraSlideTarget = -CameraSlideOffset;

            Vector3 gunScale = _gun.transform.localScale;
            gunScale.x = (gunScale.x > 0 ? -gunScale.x : gunScale.x);
            _gun.transform.localScale = gunScale;
            _sprite.flipX = true;

        }

        else if (stick > 0)
        {

            _cameraSlideTarget = CameraSlideOffset;

            Vector3 gunScale = _gun.transform.localScale;
            gunScale.x = (gunScale.x > 0 ? gunScale.x : -gunScale.x);
            _gun.transform.localScale = gunScale;
            _sprite.flipX = false;

        }

        Vector2 velocity = _rigidBody.velocity;
        velocity.x = PlayerMovementSpeed * stick;
        _rigidBody.velocity = velocity;
        
    }

    public IInventory GetInventoryComponent() => _inventoryComponent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Run interactables
        IInteractable otherAsInteractable = collision.gameObject.GetComponent<IInteractable>();
        otherAsInteractable?.Interact(gameObject);

    }

    public bool IsFlipped() => _gun.transform.localScale.x < 0;

}
