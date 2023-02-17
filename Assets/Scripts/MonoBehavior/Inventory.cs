using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;

    public InventorySlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] private int maxSize = 4;

    [SerializeField] public UnityEvent OnInventoryChanged;

    public bool AddItems(Item item, int amount)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item.id == item.id) { 
                slot.amount += amount;
                OnInventoryChanged.Invoke();
                return true;
            }
        }

        if (items.Count > maxSize) return false;

        InventorySlot newSlot = new InventorySlot(item, amount);
        items.Add(newSlot);
        OnInventoryChanged.Invoke();
        return true;
    }

    public Item GetItem(int i)
    {
        return i < items.Count ? items[i].item : null;
    }

    public int GetAmount(int i)
    {
        return i < items.Count ? items[i].amount : 0;
    }

    public int GetSize()
    {
        return items.Count;
    }
}
