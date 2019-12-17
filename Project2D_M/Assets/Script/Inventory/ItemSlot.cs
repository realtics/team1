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
