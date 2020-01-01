using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : Singletone<InventoryManger>
{
	public Inventory inventory;
	public EquipmentPanel equipmentPanel;

	public InfoDisplay wearInfoDisplay;
	public InfoDisplay notWearInfoDisplay;

	private void Awake()
	{
		inventory.MountingEvent += EquipFromInventory;
		inventory.UnMountingEvent += UnEquipFromEquipPanel;
		equipmentPanel.UnMountingEvent += UnEquipFromEquipPanel;
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
		EquippableItem previousItem;

		if (equipmentPanel.AddItem(item, out previousItem))
		{
			if (previousItem != null)
			{
				previousItem.UnEquip(this);
			}
			item.Equip(this);
			wearInfoDisplay.UpdateStatusInfo();
		}
		else
		{
			inventory.AddItem(item);
		}
	}

	public void Unequip(EquippableItem item)
	{
		if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			item.UnEquip(this);
			notWearInfoDisplay.UpdateStatusInfo();
		}
	}

	public void StatModifier(bool _isWearing, ITEM_TYPE _type, int _value)
	{
		PlayerDataManager.Inst.EquipMentToPlayerData(_isWearing, _type, _value);
	}

}
