using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ItemContainer
{
	[SerializeField] protected Item[] startingItems;
	[SerializeField] protected Transform itemParent;
	public GameObject NotWearEquipmentInfo;
	[SerializeField] InventoryManger inventoryManger;
	protected override void Start()
	{
		base.Start();
		LoadItem();
		SetStartingItems();
	}

	protected override void OnValidate()
	{
		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true , result: itemSlots);
		}
	}

	private void SetStartingItems()
	{
		//Clear();

		foreach (Item item in startingItems)
		{
			AddItem(item.GetCopy());
		}
	}

	private void LoadItem()
	{
		ItemSaveManager.Inst.LoadEquipment(inventoryManger);
		ItemSaveManager.Inst.LoadInventory(inventoryManger);
	}
}
