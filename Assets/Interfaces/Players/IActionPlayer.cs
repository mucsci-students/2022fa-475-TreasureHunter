public interface IActionPlayer
{

    // Walking & movement controls
    public void Walk(float stick);
    public void Jump();
    public bool IsFlipped();

    // Attacking
    public void PrimaryAttack();
    public void SecondaryAttack();

    // Inventory
    public IInventory? GetInventoryComponent();

}
