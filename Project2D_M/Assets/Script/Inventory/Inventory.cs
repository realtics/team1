using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ItemContainer
{
	[SerializeField] protected Transform itemParent;

	public GameObject NotWearEquipmentInfo;
	public GameObject WearEquipmentInfo;

	[SerializeField] InventoryManger inventoryManger = null;
	protected override void Start()
	{
		base.Start();
		LoadItem();
	}

	protected override void OnValidate()
	{
		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true , result: itemSlots);
		}
	}

	private void LoadItem()
	{
		ItemSaveManager.Inst.LoadEquipment(inventoryManger);
		ItemSaveManager.Inst.LoadInventory(inventoryManger);
	}
}
