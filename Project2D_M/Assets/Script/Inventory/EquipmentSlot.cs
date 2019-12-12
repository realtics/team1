using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentSlot : ItemSlot
{
	public EQUIPMENT_TYPE equipmentType;
	public event Action<Item> OnEquipmentSlotEvent;

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
			Debug.Log("언 마운트 실행");
			eSlotState = SLOT_STATE.NOT_MOUNTING;
		}
	}
}
