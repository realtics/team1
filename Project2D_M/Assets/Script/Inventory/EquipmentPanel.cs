﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent;
	[SerializeField] EquipmentSlot[] equipmentSlots;

	public event Action<Item> OnItemRightClickedEvent;

	private void Start()
	{
		for(int i = 0; i< equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
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
