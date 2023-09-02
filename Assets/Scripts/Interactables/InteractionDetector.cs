using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    public GameObject InteractionKeyObj;
    private List<IInteractables> _interactablesInRange = new List<IInteractables>();

    private void Start()
    {
        InputManager.Instance.OnInteracting += OnInteracting;

        InteractionKeyObj.SetActive(false);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnInteracting -= OnInteracting;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractables interactable) && interactable.CanInteract())
        {
            _interactablesInRange.Add(interactable);
            InteractionKeyObj.SetActive(true);
        }

        if(other.CompareTag("Collectible"))
        {
            // Collect it and destroy. Additionally, hover the collectibles around the player...
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractables interactable) && _interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Remove(interactable);
            InteractionKeyObj.SetActive(false);
        }
    }

    private void OnInteracting()
    {
        if (_interactablesInRange.Count > 0)
        {
            InteractionKeyObj.SetActive(false);

            var interactable = _interactablesInRange[0];

            // Must be checked
            interactable.Interact();

            if (!interactable.CanInteract())
            {
                _interactablesInRange.Remove(interactable);
            }
        }
    }
}
