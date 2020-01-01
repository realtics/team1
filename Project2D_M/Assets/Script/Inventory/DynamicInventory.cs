using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInventory : Inventory
{
	[SerializeField] private GameObject m_itemSlotPrefab = null;

	private int inventorySlotMaxNum;
	public int InventorySlotMaxNum
	{
		get { return inventorySlotMaxNum ; }
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

				var slotChild = itemSlotGameObj.GetComponentInChildren<ItemSlot>();

				slotChild.viewDitailObject = NotWearEquipmentInfo;
				slotChild.infoViewDitailObject = NotWearEquipmentInfo.transform.GetChild(1).gameObject;
				slotChild.notWearInfoDisplay = NotWearEquipmentInfo.transform.GetChild(1).GetComponent<InfoDisplay>();
				slotChild.mountingViewDitailObject = WearEquipmentInfo;
				slotChild.infoMountingViewDitailObject = WearEquipmentInfo.transform.GetChild(1).gameObject;
				slotChild.wearInfoDisplay = WearEquipmentInfo.transform.GetChild(1).GetComponent<InfoDisplay>();

				slotChild.slotNum = i;
				itemSlots.Add(slotChild);
			}
		}
	}

	protected override void Start()
	{
		base.Start();
	}
	private void Awake()
	{
        inventorySlotMaxNum = PlayerDataManager.Inst.GetPlayerData().inventoySize;

        SetMaxSlots(inventorySlotMaxNum);
	}

}
