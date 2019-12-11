using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] Image itemImage;
	[SerializeField] Image frameBackImage;
	[SerializeField] Image frameBackEdgeImage;
	[SerializeField] Image noticeImage;

	[SerializeField] InfoDisplay infoDisplay;

	public event Action<Item> OnLeftClickEvent;

	private Item m_item;

	private readonly int m_hashBOpen = Animator.StringToHash("bOpen");

	public GameObject viewDitailObject;
	public GameObject infoViewDitailObject;
	public GameObject mountingViewDitailObject;
	public GameObject infoMountingViewDitailObject;
	public bool isMounting;

	public int slotNum;

	public Item Item
	{
		get { return m_item; }
		set
		{
			m_item = value;

			if(m_item == null)
			{
				itemImage.enabled = false;
				frameBackImage.enabled = false;
				frameBackEdgeImage.enabled = false;
			}
			else
			{
				itemImage.enabled = true;
				itemImage.sprite = m_item.icon;
				itemImage.SetNativeSize();

				frameBackImage.enabled = true;
				frameBackEdgeImage.enabled = true;
			}
		}
	}

	public void MountingItem()
	{
		if (Item != null && OnLeftClickEvent != null)
		{
			OnLeftClickEvent(Item);
		}
	}

	public void ClickViewDetailsEvent()
	{
		SetViewDetails();
	}

	private void SetViewDetails()
	{
		if(Item is EquippableItem)
		{
			infoDisplay.ShowInfomation((EquippableItem)Item);
			infoDisplay.selectSlotNum = slotNum;
		}

		PlayAnimationViewDetails();
	}

	private void PlayAnimationViewDetails()
	{
		if(isMounting)
		{
			Animator animator = mountingViewDitailObject.GetComponent<Animator>();

			animator.SetBool(m_hashBOpen, true);
		}
		else
		{
			Animator animator = viewDitailObject.GetComponent<Animator>();

			animator.SetBool(m_hashBOpen, true);
		}
		
	}

	protected virtual void OnValidate()
	{
		if (frameBackImage == null)
		{
			frameBackImage = transform.GetChild(0).gameObject.GetComponent<Image>();
		}

		if (frameBackEdgeImage == null)
		{
			frameBackEdgeImage = transform.GetChild(1).gameObject.GetComponent<Image>();
		}

		if (itemImage == null)
		{
			itemImage = transform.GetChild(2).gameObject.GetComponent<Image>();
		}

	}
}
