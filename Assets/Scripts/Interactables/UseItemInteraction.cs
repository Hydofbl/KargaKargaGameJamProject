using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsableItems
{
    None,
    RedKeyCard,
    BlueKeyCard,
    GreenKeyCard,
    Bomb,
    CaptainsKeyCard,
}

public class UseItemInteraction : MonoBehaviour, IInteractables
{
    public UsableItems NeededItem;
    public bool IsUsed;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        // Check inventory if the user has the key
        return !IsUsed;
    }

    public void Interact()
    {
        // Interact
        IsUsed = true;

        if(NeededItem != UsableItems.None)
        {
            // Remove the item from the inventory
        }

        Debug.Log("hello");

        // User Trigger
        //_animator.SetBool("PlayUsing", true);
    }
}
