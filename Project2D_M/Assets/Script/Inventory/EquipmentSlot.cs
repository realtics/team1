using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentSlot : ItemSlot
{
	public EQUIPMENT_TYPE equipmentType;
	public int rememberInventoryIndex;

	public GameObject mountingViewDitailObject;
	public GameObject infoMountingViewDitailObject;

	public override void SetStart()
	{
		Debug.Log("장비 슬롯 실행.");

		switch (eSlotState)
		{
			case SLOT_STATE.NOT_MOUNTING:
				noticeImage.enabled = true;
				break;
			case SLOT_STATE.MOUNTING:
				noticeImage.enabled = false;
				break;
			case SLOT_STATE.UPGRADE:
				break;
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = equipmentType.ToString() + " Slot";
	}
	protected override void SetViewDetails()
	{
		if (Item is EquippableItem)
		{
			infoDisplay.ShowInfomation((EquippableItem)Item);
			SetSelectSlot((EquippableItem)Item, true);
		}

		PlayAnimationViewDetails();
	}

	private void PlayAnimationViewDetails()
	{
		if (Item != null)
		{
			Animator animator = mountingViewDitailObject.GetComponent<Animator>();

			animator.SetBool(m_hashBOpen, true);
		}

	}
}
