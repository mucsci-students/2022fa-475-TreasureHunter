using System.Collections.Generic;

public interface IInventory
{

    public bool AddItem(string toAdd);

    public bool RemoveItem(string toRemove);

    public bool HasItem(string item);

    public IEnumerable<string> GetAllItems();

}
