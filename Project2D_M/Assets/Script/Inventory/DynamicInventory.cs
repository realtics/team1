using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInventory : Inventory
{
	[SerializeField] private GameObject m_itemSlotPrefab = null;

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
				itemSlotGameObj.GetComponentInChildren<ItemSlot>().slotNum = i;
				itemSlots.Add(itemSlotGameObj.GetComponentInChildren<ItemSlot>());
			}
		}
	}

	protected override void Start()
	{
		base.Start();
	}
	private void Awake()
	{
		SetMaxSlots(inventorySlotMaxNum);
	}

}
