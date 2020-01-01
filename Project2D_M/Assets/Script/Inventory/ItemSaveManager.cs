using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : Singletone<ItemSaveManager>
{
	[SerializeField] ItemDataBase m_itemDataBase;

	private const string InventoryFileName = "Inventory";
	private const string EquipmentFileName = "Equipment";

	private void Awake()
	{
		if (m_itemDataBase == null)
			m_itemDataBase = (ItemDataBase)Resources.Load("Data/ItemDataBase");
	}

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
				itemSlot.eSlotState = savedSlot.slotState;
				itemSlot.SetStart();
			}
		}
	}

	public void LoadEquipment(InventoryManger _inventoryManager)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
		if (savedSlots == null) return;

		for (int i = 0; i < savedSlots.savedSlots.Length; i++)
		{
			ItemSlot itemSlot = _inventoryManager.equipmentPanel.equipmentSlots[i];
			ItemSlotSaveData savedSlot = savedSlots.savedSlots[i];

			if (savedSlot == null)
			{
				itemSlot.Item = null;
				itemSlot.Amount = 0;
			}
			else
			{
				itemSlot.Item = m_itemDataBase.GetItemCopy(savedSlot.itemID);
				itemSlot.Amount = savedSlot.amount;
				((EquipmentSlot)itemSlot).rememberInventoryIndex = savedSlot.rememberInventoryIndex;
				itemSlot.eSlotState = savedSlot.slotState;
				itemSlot.SetStart();
			}
		}
	}

	public PlayerDataManager.PlayerData LoadPlayerEquipmentStatusSetting(PlayerDataManager.PlayerData _playerData)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);

		if (savedSlots == null) return _playerData;

		for (int i = 0; i < savedSlots.savedSlots.Length; i++)
		{
			ItemSlotSaveData savedSlot = savedSlots.savedSlots[i];

			if (savedSlot == null)
			{
				
			}
			else
			{
				EquippableItem equipItem = (EquippableItem)m_itemDataBase.GetItemReference(savedSlot.itemID);

				_playerData.attack += equipItem.attackBonus;
				_playerData.defensive += equipItem.armorBonus;
				_playerData.maxHp += equipItem.maxHealthBonus;
			}
		}
		return _playerData;
	}


	public void SaveInventory(InventoryManger _inventoryManger)
	{
		SaveItems(_inventoryManger.inventory.itemSlots, InventoryFileName);
	}

	public void SaveEquipment(InventoryManger _inventoryManger)
	{
		SaveEquipmentItems(_inventoryManger.equipmentPanel.equipmentSlots, EquipmentFileName);
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
				saveData.savedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount, itemSlot.eSlotState);
			}
		}

		ItemSaveIO.SaveItems(saveData, _fileName);
       
	}

	private void SaveEquipmentItems(IList<ItemSlot> _itemSlots, string _fileName)
	{
		var saveData = new ItemContainerSaveData(_itemSlots.Count);

		for (int i = 0; i < saveData.savedSlots.Length; i++)
		{
			ItemSlot itemSlot = _itemSlots[i];

			if (itemSlot.Item == null)
			{
				saveData.savedSlots[i] = null;
			}
			else
			{
				saveData.savedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount, itemSlot.eSlotState,((EquipmentSlot)itemSlot).rememberInventoryIndex);
			}
		}

		ItemSaveIO.SaveItems(saveData, _fileName);
	}

}
