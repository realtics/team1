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
	public GameObject WearEquipmentInfo;

	[SerializeField] InventoryManger inventoryManger = null;
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

	//임의로 아이템 추가시키고 싶을때 사용하는 임시 함수
	private void SetStartingItems()
	{
		//인벤토리 내부 지우기 용도
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
