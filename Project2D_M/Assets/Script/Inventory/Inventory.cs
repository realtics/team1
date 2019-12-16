using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[SerializeField] List<Item> items;
	[SerializeField] Transform itemParent;
	public List<ItemSlot> itemSlots;
	public GameObject NotWearEquipmentInfo;

	public event Action<Item> OnItemLeftClickedEvent;


	[SerializeField] private GameObject m_itemSlotPrefab = null;
	//나중에 캐릭터SO에서 인벤토리 MAX 용량 받아와야함, 일단 임시로
	[SerializeField] int inventorySlotMaxNum;
	public int InventorySlotMaxNum
	{
		get { return inventorySlotMaxNum; }
		set { SetMaxSlots(value); }
	}

	private void SetMaxSlots(int value)
	{
		if (value <= 0)
		{
			inventorySlotMaxNum = 1;
		}
		else
		{
			inventorySlotMaxNum = value;
		}

		if (inventorySlotMaxNum < itemSlots.Count)
		{
			for (int i = inventorySlotMaxNum; i < itemSlots.Count; i++)
			{
				Destroy(itemSlots[i].transform.parent.gameObject);
			}

			int diff = itemSlots.Count - inventorySlotMaxNum;
			itemSlots.RemoveRange(inventorySlotMaxNum, diff);
		}
		else if (inventorySlotMaxNum > itemSlots.Count)
		{
			int diff = inventorySlotMaxNum - itemSlots.Count;

			for (int i = 0; i < diff; i++)
			{
				GameObject itemSlotGameObj = Instantiate(m_itemSlotPrefab);
				itemSlotGameObj.transform.SetParent(itemParent, worldPositionStays: false);
				itemSlotGameObj.GetComponentInChildren<ItemSlot>().viewDitailObject = NotWearEquipmentInfo;
				itemSlotGameObj.GetComponentInChildren<ItemSlot>().infoViewDitailObject = NotWearEquipmentInfo.transform.GetChild(1).gameObject;
				itemSlotGameObj.GetComponentInChildren<ItemSlot>().infoDisplay = NotWearEquipmentInfo.transform.GetChild(1).GetComponent<InfoDisplay>();
				itemSlotGameObj.GetComponentInChildren<ItemSlot>().slotNum = i + 1;
				itemSlots.Add(itemSlotGameObj.GetComponentInChildren<ItemSlot>());
			}
		}
	}


	public void Initialize()
	{
		SetMaxSlots(inventorySlotMaxNum);

		for (int i = 0; i < itemSlots.Count; i++)
		{
			itemSlots[i].OnItemSlotEvent += OnItemLeftClickedEvent;
		}

		RefreshUI();
	}

	private void OnValidate()
	{
		if (itemParent != null)
		{
			GetComponentsInChildren<ItemSlot>(includeInactive: true , result: itemSlots);
		}

		RefreshUI();
	}

	private void RefreshUI()
	{
		int i = 0;

		for (; i < items.Count && i < itemSlots.Count; i++)
		{
			itemSlots[i].Item = items[i];
		}

		for (; i < itemSlots.Count; i++)
		{
			itemSlots[i].Item = null;
			itemSlots[i].slotNum = i;
		}
	}

	public bool AddItem(Item item)
	{
		if (IsFull())
		{
			return false;
		}

		items.Add(item);
		RefreshUI();
		return true;
	}

	public bool RemoveItem(Item item)
	{
		if (items.Remove(item))
		{
			RefreshUI();
			return true;
		}

		return false;
	}

	public bool IsFull()
	{
		return items.Count >= itemSlots.Count;
	}

	public void Clear()
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item != null && Application.isPlaying)
			{
				itemSlots[i].Item.Destroy();
			}
			itemSlots[i].Item = null;
			itemSlots[i].Amount = 0;
		}
	}


}
