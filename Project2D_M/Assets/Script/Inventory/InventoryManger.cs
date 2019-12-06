using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
	[SerializeField] List<Item> items;
	[SerializeField] Transform itemParent;
	[SerializeField] ItemSlot[] itemSlots;

	private void OnValidate()
	{
		if(itemParent != null)
		{
			itemSlots = itemParent.GetComponentsInChildren<ItemSlot>();
		}

		RefreshUI();
	}

	private void RefreshUI()
	{
		int i = 0;
		
		for(; i < items.Count && i < itemSlots.Length; i++)
		{
			itemSlots[i].Item = items[i];
		}

		for( ; i < itemSlots.Length; i++)
		{
			itemSlots[i].Item = null;
		}
	}

}
