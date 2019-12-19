using System;

[Serializable]
public class ItemSlotSaveData
{
	public string itemID;
	public int amount;
	public SLOT_STATE slotState;
	public int rememberInventoryIndex;

	public ItemSlotSaveData(string _id, int _amount, SLOT_STATE _slotState, int _rememberInventoryIndex = 0)
	{
		itemID = _id;
		amount = _amount;
		slotState = _slotState;
		rememberInventoryIndex = _rememberInventoryIndex;
	}
}

//public class EquipmentSlotSaveData
//{
//	public string itemID;
//	public int amount;
//	public SLOT_STATE slotState;

//	public EquipmentSlotSaveData(string _id, int _amount, SLOT_STATE _slotState)
//	{
//		itemID = _id;
//		amount = _amount;
//		slotState = _slotState;
//	}
//}

[Serializable]
public class ItemContainerSaveData
{
	public ItemSlotSaveData[] savedSlots;

	public ItemContainerSaveData(int _numItems)
	{
		savedSlots = new ItemSlotSaveData[_numItems];
	}
}