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

	public GameObject mountingViewDitailObject;
	public GameObject infoMountingViewDitailObject;

	public bool isMountingSlot;

	public virtual void SetStart()
	{
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
		if(eSlotState == SLOT_STATE.NOT_MOUNTING)
		{
			if (Item is EquippableItem)
			{
				notWearInfoDisplay.ShowInfomation((EquippableItem)Item);
				SetSelectSlot((EquippableItem)Item, false);
			}

			PlayAnimationViewDetails(false);
		}
		else if(eSlotState == SLOT_STATE.MOUNTING)
		{
			if (Item is EquippableItem)
			{
				wearInfoDisplay.ShowInfomation((EquippableItem)Item);
				SetSelectSlot((EquippableItem)Item, false);
			}

			PlayAnimationViewDetails(true);
		}
	}

	private void PlayAnimationViewDetails(bool _isMounting)
	{
		if(_isMounting)
		{
			if (Item != null)
			{
				Animator animator = mountingViewDitailObject.GetComponent<Animator>();

				animator.SetBool(m_hashBOpen, true);
			}
		}
		else
		{
			if (Item != null)
			{
				Animator animator = viewDitailObject.GetComponent<Animator>();

				animator.SetBool(m_hashBOpen, true);
			}
		}
		
	}

}
