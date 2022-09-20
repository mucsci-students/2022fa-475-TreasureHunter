using System;

public enum EWalkDirection 
{ 
    
    LEFT,
    RIGHT

}

public interface IActionPlayer
{

    // Walking & movement controls
    public void Walk(EWalkDirection direction);
    public void Jump();

    // Attacking
    public void PrimaryAttack();
    public void SecondaryAttack();

}
