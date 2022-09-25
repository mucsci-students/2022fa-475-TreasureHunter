using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic inventory that allows holding one of each item (case insensitive)
/// </summary>
public class Inventory : MonoBehaviour, IInventory
{

    private List<string> _items = new();

    public bool AddItem(string toAdd)
    {

        toAdd = toAdd.ToLower();
        if (!HasItem(toAdd)) 
        { 
            
            _items.Add(toAdd);
            print($"Collected \"{toAdd}\" [{string.Join(",", _items)}]");
            return true;
        
        }

        return false;

    }

    public IEnumerable<string> GetAllItems() => _items;

    public bool HasItem(string item) => _items.Contains(item.ToLower());

    public bool RemoveItem(string toRemove) => _items.Remove(toRemove.ToLower());

}
