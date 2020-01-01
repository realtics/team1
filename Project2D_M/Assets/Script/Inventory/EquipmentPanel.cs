using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent = null;

	public EquipmentSlot[] equipmentSlots;

	public event Action<BaseItemSlot> UnMountingEvent;

	private void Start()
	{
		for(int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].UnMountingEvent += slot => UnMountingEvent(slot);
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
	}

	public bool AddItem(EquippableItem item, out EquippableItem previousItem)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].equipmentType == item.equipmentType)
			{
				previousItem = (EquippableItem)equipmentSlots[i].Item;
				equipmentSlots[i].Item = item;
				equipmentSlots[i].Amount = 1;
				return true;
			}
		}
		previousItem = null;
		return false;
	}
	public bool AddItem(EquippableItem _item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == null)
			{
				equipmentSlots[i].Item = _item;
				return true;
			}
		}
		return false;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == item)
			{
				equipmentSlots[i].Item = null;
				equipmentSlots[i].Amount = 0;
				return true;
			}
		}
		return false;
	}

}
