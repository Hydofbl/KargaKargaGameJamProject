using UnityEngine;
using UnityEngine.UI;

public class CollectibleObject : MonoBehaviour
{
    public CollectibleItemTypes ItemType;

    public GameObject ItemPrefab;

    public void Collect()
    {
        GameObject collectedItem = Instantiate(ItemPrefab);
        InventoryManager.Instance.AddInventory(ItemType, collectedItem);

        Destroy(gameObject);
    }
}
