using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemSlot : BaseItemSlot
{
	public GameObject viewDitailObject;
	public GameObject infoViewDitailObject;

	public bool isMountingSlot;

	public virtual void SetStart()
	{
		Debug.Log("인벤토리 슬롯 실행.");

		switch (eSlotState)
		{
			case SLOT_STATE.NOT_MOUNTING:
				noticeImage.enabled = false;
				break;
			case SLOT_STATE.MOUNTING:
				noticeImage.enabled = true;
				break;
			case SLOT_STATE.UPGRADE:
				break;
		}
	}

	public override bool CanAddStack(Item _item, int _amount = 1)
	{
		return base.CanAddStack(_item, _amount) && Amount + _amount <= _item.MaximumStacks;
	}

	protected override void SetViewDetails()
	{
		if (Item is EquippableItem)
		{
			infoDisplay.ShowInfomation((EquippableItem)Item);
			SetSelectSlot((EquippableItem)Item, false);
		}

		PlayAnimationViewDetails();
	}

	private void PlayAnimationViewDetails()
	{
		if (Item != null)
		{
			Animator animator = viewDitailObject.GetComponent<Animator>();

			animator.SetBool(m_hashBOpen, true);
		}
		
	}

}
