using System;
using UnityEngine;

public interface IInteractable
{

    public void Interact(GameObject interactor);

    public event EventHandler OnInteracted;

}
