using System;
using UnityEngine;

/// <summary>
/// An item that adds a specific string to a player's inventory
/// </summary>
public class Collectable : MonoBehaviour, IInteractable
{

    public string CollectableName = "nothing";

    public AudioClip CollectSound;

    public event EventHandler OnInteracted;

    public void Interact(GameObject interactor)
    {

        IInventory interactorInventory = interactor.GetComponent<IInventory>();
        if (interactorInventory != null && interactorInventory.AddItem(CollectableName))
        {

            if (CollectSound != null) { AudioSource.PlayClipAtPoint(CollectSound, gameObject.transform.position); }
            OnInteracted?.Invoke(interactor, EventArgs.Empty);
            Destroy(gameObject);

        }

    }

}
