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
        if(other.TryGetComponent(out IInteractables interactable))
        {
            _interactablesInRange.Add(interactable);
            InteractionKeyObj.SetActive(true);
        }

        if(other.CompareTag("Collectible") && other.TryGetComponent(out CollectibleObject collectible))
        {
            other.tag = "Usable";

            // Collect it and destroy. Additionally, hover the collectibles around the player...
            // Destroy(other.gameObject);

            collectible.Collect();
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
            var interactable = _interactablesInRange[0];

            if(interactable.CanInteract())
            {
                InteractionKeyObj.SetActive(false);

                // Must be checked
                interactable.Interact();

                if (!interactable.CanInteract())
                {
                    _interactablesInRange.Remove(interactable);
                }
            }
        }
    }
}
