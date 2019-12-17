public interface IItemContainer
{
	bool CanAddItem(Item _item, int _amount = 1);
	bool AddItem(Item _item);

	Item RemoveItem(string _itemID);
	bool RemoveItem(Item _item);

	void Clear();

	int ItemCount(string _itemID);
}