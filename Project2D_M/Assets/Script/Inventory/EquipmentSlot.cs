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
