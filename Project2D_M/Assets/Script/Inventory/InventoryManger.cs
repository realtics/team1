using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
	public Inventory inventory;
	public EquipmentPanel equipmentPanel;

	[SerializeField] ItemSaveManager itemSaveManager;

	private void Awake()
	{
		inventory.MountingOrUnMountingEvent += EquipFromInventory;
		equipmentPanel.MountingOrUnMountingEvent += UnEquipFromEquipPanel;
	}

	private void Start()
	{
		if (itemSaveManager != null)
		{
			itemSaveManager.LoadEquipment(this);
			itemSaveManager.LoadInventory(this);
		}
	}

	private void OnDestroy()
	{
		if (itemSaveManager != null)
		{
			itemSaveManager.SaveEquipment(this);
			itemSaveManager.SaveInventory(this);
		}
	}

	private void EquipFromInventory(BaseItemSlot _itemSlot)
	{
		if (_itemSlot.Item is EquippableItem)
		{
			Equip((EquippableItem)_itemSlot.Item);
		}
	}

	private void UnEquipFromEquipPanel(BaseItemSlot _itemSlot)
	{
		if (_itemSlot.Item is EquippableItem)
		{
			Unequip((EquippableItem)_itemSlot.Item);
		}
	}


	public void Equip(EquippableItem item)
	{
		if (inventory.RemoveItem(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
				}
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	public void Unequip(EquippableItem item)
	{
		if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			inventory.AddItem(item);
		}
	}

}
