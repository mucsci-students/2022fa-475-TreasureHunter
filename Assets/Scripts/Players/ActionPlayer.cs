using UnityEngine;

public class ActionPlayer : MonoBehaviour, IActionPlayer
{

    public float PlayerMovementSpeed = 10;
    public float JumpImpulseMagnitude = 5;

    public Camera PlayerCamera;
    public float CameraSlideOffset = 1.5f;
    public float CameraSlideSpeed = 10;

    // TODO: Allow specifying a projectile

    private Rigidbody2D _rigidBody;
    private float _cameraSlideTarget = 0;
    private bool _facingLeft = false;

    public void Jump()
    {

        // Only allow jumping if the player is grounded
        if (_rigidBody.velocity.y == 0)
        {
            _rigidBody.AddForce(Vector2.up * JumpImpulseMagnitude, ForceMode2D.Impulse);
        }

    }

    public void PrimaryAttack()
    {
        throw new System.NotImplementedException();
    }

    public void SecondaryAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Walk(EWalkDirection direction)
    {

        float speed = 0;
        if (direction == EWalkDirection.LEFT)
        {

            speed = -PlayerMovementSpeed;
            _cameraSlideTarget = -CameraSlideOffset;

        }

        else if (direction == EWalkDirection.RIGHT)
        {

            speed = PlayerMovementSpeed;
            _cameraSlideTarget = CameraSlideOffset;

        }

        _rigidBody.transform.Translate(new Vector2(speed * Time.deltaTime, 0));

    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentCameraLocation = PlayerCamera.transform.localPosition;
        currentCameraLocation.x = Mathf.Lerp(currentCameraLocation.x, _cameraSlideTarget, CameraSlideSpeed * Time.deltaTime);
        PlayerCamera.transform.localPosition = currentCameraLocation;
        
    }
}
