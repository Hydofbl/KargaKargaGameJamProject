using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryParent;

    public int size;

    public Dictionary<CollectibleItemTypes, GameObject> PlayerInventoryDict = new Dictionary<CollectibleItemTypes, GameObject>(); 

    private static InventoryManager _instance;
    public static InventoryManager Instance { get => _instance; }

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        size = PlayerInventoryDict.Count;
    }

    public void AddInventory(CollectibleItemTypes type, GameObject item)
    {
        item.transform.SetParent(InventoryParent.transform, false);
        PlayerInventoryDict.Add(type, item);
    }
}
