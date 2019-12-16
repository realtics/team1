using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
	public Inventory inventory;
	public EquipmentPanel equipmentPanel;

	private void Awake()
	{
		inventory.OnItemLeftClickedEvent += EquipFromInventory;
		equipmentPanel.OnItemLeftClickedEvent += UnEquipFromEquipPanel;

		inventory.Initialize();
		equipmentPanel.Initialize();
	}

	private void EquipFromInventory(Item item)
	{
		if(item is EquippableItem)
		{
			Equip((EquippableItem)item);
		}
	}

	private void UnEquipFromEquipPanel(Item item)
	{
		if (item is EquippableItem)
		{
			Unequip((EquippableItem)item);
		}
	}


	public void Equip(EquippableItem item)
	{
	//	if(inventory.RemoveItem(item))
	//	{
			EquippableItem previousItem;

			if(equipmentPanel.AddItem(item, out previousItem))
			{
				if(previousItem != null)
				{
					//inventory.AddItem(previousItem);
				}
			}
			else
			{
				inventory.AddItem(item);
			}
	//	}
	}

	public void Unequip(EquippableItem item)
	{
		if(!inventory.IsFull() & equipmentPanel.RemoveItem(item))
		{
		//	inventory.AddItem(item);
		}
	}

}
