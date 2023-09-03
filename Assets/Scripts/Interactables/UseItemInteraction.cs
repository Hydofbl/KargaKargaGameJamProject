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
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        // Check if it did not used or inventory contains the needed item type
        return !IsUsed || InventoryManager.Instance.PlayerInventoryDict.ContainsKey(NeededItemType);
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
        //_animator.SetBool("PlayUsing", true);
        Destroy(gameObject);
    }
}
