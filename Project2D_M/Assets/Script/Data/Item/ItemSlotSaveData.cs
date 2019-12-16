using System;

[Serializable]
public class ItemSlotSaveData
{
	public string itemID;
	public int amount;

	public ItemSlotSaveData(string _id, int _amount)
	{
		itemID = _id;
		amount = _amount;
	}
}

[Serializable]
public class ItemContainerSaveData
{
	public ItemSlotSaveData[] savedSlots;

	public ItemContainerSaveData(int _numItems)
	{
		savedSlots = new ItemSlotSaveData[_numItems];
	}
}