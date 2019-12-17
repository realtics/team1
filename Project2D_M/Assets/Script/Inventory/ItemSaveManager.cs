using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
	[SerializeField] ItemDataBase m_itemDataBase;

	private const string InventoryFileName = "Inventory";
	private const string EquipmentFileName = "Equipment";

	public void LoadInventory(InventoryManger _inventoryManager)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
		if (savedSlots == null) return;

		_inventoryManager.inventory.Clear();

		for(int i = 0; i < savedSlots.savedSlots.Length; i++)
		{
			ItemSlot itemSlot = _inventoryManager.inventory.itemSlots[i];
			ItemSlotSaveData savedSlot = savedSlots.savedSlots[i];

			if(savedSlot == null)
			{
				itemSlot.Item = null;
				itemSlot.Amount = 0;
			}
			else
			{
				itemSlot.Item = m_itemDataBase.GetItemCopy(savedSlot.itemID);
				itemSlot.Amount = savedSlot.amount;
			}
		}
	}

	public void LoadEquipment(InventoryManger _inventoryManger)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
		if (savedSlots == null) return;

		foreach(ItemSlotSaveData savedSlot in savedSlots.savedSlots)
		{
			if(savedSlot == null)
			{
				continue;
			}

			Item item = m_itemDataBase.GetItemCopy(savedSlot.itemID);
			_inventoryManger.inventory.AddItem(item);
			_inventoryManger.Equip((EquippableItem)item);
		}
	}


	public void SaveInventory(InventoryManger _inventoryManger)
	{
		SaveItems(_inventoryManger.inventory.itemSlots, InventoryFileName);
	}

	public void SaveEquipment(InventoryManger _inventoryManger)
	{
		SaveItems(_inventoryManger.equipmentPanel.equipmentSlots, EquipmentFileName);
	}

	private void SaveItems(IList<ItemSlot> _itemSlots, string _fileName)
	{
		var saveData = new ItemContainerSaveData(_itemSlots.Count);
		
		for(int i = 0; i < saveData.savedSlots.Length; i++)
		{
			ItemSlot itemSlot = _itemSlots[i];

			if (itemSlot.Item == null)
			{
				saveData.savedSlots[i] = null;
			}
			else
			{
				saveData.savedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount);
			}
		}

		ItemSaveIO.SaveItems(saveData, _fileName);
	}

}
