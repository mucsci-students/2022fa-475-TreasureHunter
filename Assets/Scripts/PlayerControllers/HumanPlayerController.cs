using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    
    public string WalkingAxis = "Horizontal";
    public string JumpingAxis = "Vertical";
    public string PrimaryAttackAxis = "Fire1";
    public string SecondaryAttackAxis = "";

    private IActionPlayer _controlledPlayer;

    void Start() => _controlledPlayer = GetComponent<IActionPlayer>();

    void Update()
    {

        _controlledPlayer.Walk(Input.GetAxis(WalkingAxis));
        if (Input.GetAxis(JumpingAxis) != 0) { _controlledPlayer.Jump(); }
        if (Input.GetAxis(PrimaryAttackAxis) != 0) { _controlledPlayer.PrimaryAttack(); }
        
        // TODO: Implement secondary attack
        // if (isUsingSecondaryAttack) { _controlledPlayer.SecondaryAttack(); }

    }

}
