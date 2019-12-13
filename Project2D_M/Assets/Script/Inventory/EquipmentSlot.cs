using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentSlot : ItemSlot
{
	public EQUIPMENT_TYPE equipmentType;
	public event Action<Item> OnEquipmentSlotEvent;
	public int rememberInventoryIndex;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = equipmentType.ToString() + " Slot";
	}

	public void UnMountingItem()
	{
		if (Item != null && OnEquipmentSlotEvent != null)
		{
			OnEquipmentSlotEvent(Item);
		}
	}
}
