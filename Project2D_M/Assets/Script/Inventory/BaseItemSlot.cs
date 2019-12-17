using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SLOT_STATE
{
	NOT_MOUNTING,
	MOUNTING,
	UPGRADE,
}

public class BaseItemSlot : MonoBehaviour
{
	[SerializeField] protected Image itemImage;
	[SerializeField] protected Image frameBackImage;
	[SerializeField] protected Image frameBackEdgeImage;
	[SerializeField] protected TextMeshProUGUI itemlevel;
	[SerializeField] protected Image noticeImage;
	[SerializeField] protected TextMeshProUGUI amountText;
	public InfoDisplay infoDisplay;

	protected readonly int m_hashBOpen = Animator.StringToHash("bOpen");

	public int slotNum;

	public SLOT_STATE eSlotState;

	public event Action<BaseItemSlot> MountingOrUnMountingEvent;

	private Item m_item;
	public Item Item
	{
		get { return m_item; }
		set
		{
			m_item = value;

			if (m_item == null)
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
	private int m_amount;
	public int Amount
	{
		get { return m_amount; }
		set
		{
			m_amount = value;
			if (m_amount < 0) m_amount = 0;
			if (m_amount == 0 && Item != null) Item = null;

			if (amountText != null)
			{
				amountText.enabled = m_item != null && m_amount > 1;
				if (amountText.enabled)
				{
					amountText.text = m_amount.ToString();
				}
			}
		}
	}

	public virtual bool CanAddStack(Item item, int amount = 1)
	{
		return Item != null && Item.ID == item.ID;
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

		if (noticeImage == null)
		{
			noticeImage = transform.GetChild(4).gameObject.GetComponent<Image>();
		}

		Item = m_item;
		Amount = m_amount;
	}

	public void MountingOrUnMountingItem()
	{
		if (Item != null && MountingOrUnMountingEvent != null)
		{
			MountingOrUnMountingEvent(this);
		}
	}
	public void ClickViewDetailsEvent()
	{
		SetViewDetails();
	}

	protected virtual void SetViewDetails()
	{
		
	}

	public void SetSelectSlot(EquippableItem _item, bool _isMountingSlot)
	{
		if (_isMountingSlot)
		{
			infoDisplay.saveEquipmentSlotIndex = slotNum;
		}
		else
		{
			infoDisplay.saveInventorySlotIndex = slotNum;
			switch (_item.equipmentType)
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

	public void SettingNoticeIcon(bool _isMountingSlot)
	{
		if (_isMountingSlot)
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
}
