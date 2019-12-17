using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent;
	public EquipmentSlot[] equipmentSlots;

	public GameObject WearEquipmentInfo;

	public event Action<BaseItemSlot> MountingOrUnMountingEvent;

	private void Start()
	{
		for(int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].MountingOrUnMountingEvent += slot => MountingOrUnMountingEvent(slot);
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
