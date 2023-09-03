using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public CollectibleItemTypes ItemType;

    public void Collect()
    {
        InventoryManager.Instance.PlayerInventoryDict.Add(ItemType, gameObject);
    }

    public void Use()
    {
        Destroy(gameObject);
    }
}
