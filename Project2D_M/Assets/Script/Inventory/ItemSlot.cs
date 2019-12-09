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

	public event Action<Item> OnRightClickEvent;

	private Item m_item;

	public GameObject ViewDitailObject;
	private readonly int m_hashBOpen = Animator.StringToHash("bOpen");


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
				itemImage.sprite = m_item.Icon;
				itemImage.SetNativeSize();
				itemImage.enabled = true;

				frameBackImage.enabled = true;
				frameBackEdgeImage.enabled = true;
			}
		}
	}

	public void ClickViewDetailsEvent()
	{
		if (Item != null && OnRightClickEvent != null)
		{
			OnRightClickEvent(Item);
		}

		SetViewDetails();
	}

	private void SetViewDetails()
	{
		//셋팅

		//애니메이션 재생.
		PlayAnimationViewDetails();
	}

	private void PlayAnimationViewDetails()
	{
		Animator animator = ViewDitailObject.GetComponent<Animator>();

		animator.SetBool(m_hashBOpen, true);
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
