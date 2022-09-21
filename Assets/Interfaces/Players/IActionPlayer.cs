public interface IActionPlayer
{

    // Walking & movement controls
    public void Walk(float stick);
    public void Jump();

    // Attacking
    public void PrimaryAttack();
    public void SecondaryAttack();

}
