using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractables
{
    // public Dialog Dialog;
    // public Dialog FinalDialog; -> Could be inside the Dialog class
    [SerializeField] private DialogScriptableObject dialog;
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        DialogManager.Instance.StartDialog(dialog);
    }
}
