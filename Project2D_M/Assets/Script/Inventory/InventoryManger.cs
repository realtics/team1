using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
	public Inventory inventory;
	public EquipmentPanel equipmentPanel;

	private void Awake()
	{
		inventory.MountingOrUnMountingEvent += EquipFromInventory;
		equipmentPanel.MountingOrUnMountingEvent += UnEquipFromEquipPanel;
	}

	private void OnDestroy()
	{
		ItemSaveManager.Inst.SaveEquipment(this);
		ItemSaveManager.Inst.SaveInventory(this);
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
		//if (inventory.RemoveItem(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					//inventory.AddItem(previousItem);
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
			//inventory.AddItem(item);
		}
	}

}
