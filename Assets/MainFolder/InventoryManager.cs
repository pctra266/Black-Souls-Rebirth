using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<string> items = new List<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Added item: " + itemName);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public List<string> GetAllItems()
    {
        return items;
    }
}
