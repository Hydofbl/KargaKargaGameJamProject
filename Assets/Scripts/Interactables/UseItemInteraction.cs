using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleItemTypes
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
    public CollectibleItemTypes NeededItemType;
    public bool IsUsed;
    public BoxCollider2D CollisionCollider;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        // Check if it did not used or inventory contains the needed item type
        if (NeededItemType != CollectibleItemTypes.None)
            return !IsUsed && InventoryManager.Instance.PlayerInventoryDict.ContainsKey(NeededItemType);
        else
            return !IsUsed;
    }

    public void Interact()
    {
        // Interact
        IsUsed = true;

        if(NeededItemType != CollectibleItemTypes.None)
        {
            // Remove the item from the inventory
            Destroy(InventoryManager.Instance.PlayerInventoryDict[NeededItemType]);
            InventoryManager.Instance.PlayerInventoryDict.Remove(NeededItemType);
        }

        // User Trigger
        _animator.SetTrigger("Use");

        // Make door's and similar object's collision colliders disable
        if(CollisionCollider != null)
        {
            CollisionCollider.enabled = false;
        }
    }
}
