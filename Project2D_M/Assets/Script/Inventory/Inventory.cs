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

	protected override void Awake()
	{
		base.Awake();
		SetStartingItems();
	}

	protected override void OnValidate()
	{
		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true , result: itemSlots);
		}

		if(!Application.isPlaying)
		{
			SetStartingItems();
		}
	}

	private void SetStartingItems()
	{
		Clear();
		foreach(Item item in startingItems)
		{
			AddItem(item.GetCopy());
		}
	}
}
