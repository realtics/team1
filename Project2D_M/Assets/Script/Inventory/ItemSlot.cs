using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum SLOT_STATE
{
	NOT_MOUNTING,
	MOUNTING,
	UPGRADE,
}

public class ItemSlot : MonoBehaviour
{
	[SerializeField] Image itemImage;
	[SerializeField] Image frameBackImage;
	[SerializeField] Image frameBackEdgeImage;
	[SerializeField] TextMeshProUGUI itemlevel;
	[SerializeField] Image noticeImage;
	[SerializeField] TextMeshProUGUI amountText;
	public InfoDisplay infoDisplay;

	public event Action<Item> OnItemSlotEvent;

	private readonly int m_hashBOpen = Animator.StringToHash("bOpen");

	public GameObject viewDitailObject;
	public GameObject infoViewDitailObject;
	public GameObject mountingViewDitailObject;
	public GameObject infoMountingViewDitailObject;
	public bool isMountingSlot;
	public int slotNum;

	public SLOT_STATE eSlotState;

	private Item m_item;
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
				itemlevel.enabled = false;
			}
			else
			{
				itemImage.enabled = true;
				itemImage.sprite = m_item.icon;
				itemImage.SetNativeSize();

				itemlevel.enabled = true;
				frameBackImage.color = new Color(m_item.frameColorRGB.x / 255, m_item.frameColorRGB.y / 255, m_item.frameColorRGB.z / 255);
				frameBackEdgeImage.color = new Color(m_item.frameColorRGB.x / 255, m_item.frameColorRGB.y / 255, m_item.frameColorRGB.z / 255);
				frameBackImage.enabled = true;
				frameBackEdgeImage.enabled = true;
			}
		}
	}

	// 수량 표기가 필요할 때 쓰이는 '양'
	private int _amount;
	public int Amount
	{
		get { return _amount; }
		set
		{
			_amount = value;
			if (_amount < 0) _amount = 0;
			if (_amount == 0 && Item != null) Item = null;

			if (amountText != null)
			{
				amountText.enabled = m_item != null && _amount > 1;
				if (amountText.enabled)
				{
					amountText.text = _amount.ToString();
				}
			}
		}
	}
	public void SettingNoticeIcon()
	{
		if (isMountingSlot)
		{
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
		else
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
	}

	public void MountingItem()
	{
		if (Item != null && OnItemSlotEvent != null)
		{
			OnItemSlotEvent(Item);

		}
	}

	public void SetSelectSlot(EquippableItem _item)
	{
		if (isMountingSlot)
		{
			infoDisplay.saveEquipmentSlotIndex = slotNum;
		}
		else
		{
			infoDisplay.saveInventorySlotIndex = slotNum;
			switch(_item.equipmentType)
			{
				case EQUIPMENT_TYPE.WEAPON:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.WEAPON;
					break;
				case EQUIPMENT_TYPE.HAT:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.HAT;
					break;
				case EQUIPMENT_TYPE.TOP:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.TOP;
					break;
				case EQUIPMENT_TYPE.GLOVES:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.GLOVES;
					break;
				case EQUIPMENT_TYPE.PANTS:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.PANTS;
					break;
				case EQUIPMENT_TYPE.SHOES:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.SHOES;
					break;
				case EQUIPMENT_TYPE.NECKLACE:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.NECKLACE;
					break;
				case EQUIPMENT_TYPE.EARRING_1:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.EARRING_1;
					break;
				case EQUIPMENT_TYPE.EARRING_2:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.EARRING_2;
					break;
				case EQUIPMENT_TYPE.RING_1:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.RING_1;
					break;
				case EQUIPMENT_TYPE.RING_2:
					infoDisplay.saveEquipmentSlotIndex = (int)EQUIPMENT_TYPE.RING_2;
					break;
			}
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
			SetSelectSlot((EquippableItem)Item);
		}

		PlayAnimationViewDetails();
	}

	public void PlayAnimationViewDetails()
	{
		if (Item != null)
		{
			if (isMountingSlot)
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

		if (itemlevel == null)
		{
			itemlevel = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
		}

		if (noticeImage==null)
		{
			noticeImage = transform.GetChild(4).gameObject.GetComponent<Image>();
		}
	}
}
