using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    
    public string WalkingAxis = "Horizontal";
    public string JumpingAxis = "Vertical";
    public string PrimaryAttackAxis = "";
    public string SecondaryAttackAxis = "";

    private IActionPlayer _controlledPlayer;

    void Start() => _controlledPlayer = GetComponent<IActionPlayer>();

    void Update()
    {

        float walkAxisValue = Input.GetAxis(WalkingAxis);
        bool isMoving = walkAxisValue != 0;
        bool movingLeft = walkAxisValue < 0;
        bool movingRight = walkAxisValue > 0;

        bool isJumping = Input.GetAxis(JumpingAxis) != 0;

        // bool isUsingPrimaryAttack = Input.GetAxis(PrimaryAttackAxis) != 0;
        // bool isUsingSecondaryAttack = Input.GetAxis(SecondaryAttackAxis) != 0;

        if (isMoving && movingLeft) { _controlledPlayer.Walk(EWalkDirection.LEFT); }
        else if (isMoving && movingRight) { _controlledPlayer.Walk(EWalkDirection.RIGHT); }

        if (isJumping) { _controlledPlayer.Jump(); }

        // if (isUsingPrimaryAttack) { _controlledPlayer.PrimaryAttack(); }
        // if (isUsingSecondaryAttack) { _controlledPlayer.SecondaryAttack(); }

    }

}
