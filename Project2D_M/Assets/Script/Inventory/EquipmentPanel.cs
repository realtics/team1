﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent;
	public EquipmentSlot[] equipmentSlots;
	public GameObject WearEquipmentInfo;

	public event Action<Item> OnItemLeftClickedEvent;

	public void Initialize()
	{
		for(int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnEquipmentSlotEvent += OnItemLeftClickedEvent;
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();

		for (int i = 0 ; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].slotNum = i;
		}
	}

	public bool AddItem(EquippableItem item, out EquippableItem previousItem)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if(equipmentSlots[i].equipmentType == item.equipmentType)
			{
				previousItem = (EquippableItem)equipmentSlots[i].Item;
				equipmentSlots[i].Item = item;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == item)
			{
				equipmentSlots[i].Item = null;
				return true;
			}
		}
		return false;
	}

}
