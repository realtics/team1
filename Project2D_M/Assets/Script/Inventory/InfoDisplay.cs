using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfoDisplay : MonoBehaviour
{
	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;

	private StringBuilder m_sbRatingAndType = new StringBuilder();

	public TextMeshProUGUI itemRatingAndType;
	public TextMeshProUGUI itemName;
	public Image BackImage;
	public Image BackFrame;
	public Image itemMainImage;

	public TextMeshProUGUI abilityTypeName;
	public TextMeshProUGUI abilityValue;

	private readonly string attackAbility = "공격력";
	private readonly string defenseAbility = "방어력";
	private readonly string maxHealthAbility = "최대 체력량";

	public bool isWearEquipmentInfo;

	public int saveEquipmentSlotIndex;
	public int saveInventorySlotIndex;

	public void ShowInfomation(EquippableItem _item)
	{
		m_sbRatingAndType.Length = 0;
		m_sbRatingAndType.Append(_item.ratingName);
		m_sbRatingAndType.Append(" ");
		m_sbRatingAndType.Append(_item.typeName);

		itemRatingAndType.text = m_sbRatingAndType.ToString();
		itemName.text = _item.itemName;
		BackImage.color = new Color(_item.frameColorRGB.x / 255, _item.frameColorRGB.y / 255, _item.frameColorRGB.z / 255);
		BackFrame.color = new Color(_item.frameColorRGB.x / 255, _item.frameColorRGB.y / 255, _item.frameColorRGB.z / 255);
		itemMainImage.sprite = _item.icon;

		switch(_item.itemType)
		{
			case ITEM_TYPE.WEAPON:
				abilityTypeName.text = attackAbility;
				abilityValue.text = GetThousandCommaText(_item.attackBonus);
				break;
			case ITEM_TYPE.ARMOR:
				abilityTypeName.text = defenseAbility;
				abilityValue.text = GetThousandCommaText(_item.armorBonus);
				break;
			case ITEM_TYPE.ACCESSORIES:
				abilityTypeName.text = maxHealthAbility;
				abilityValue.text = GetThousandCommaText(_item.maxHealthBonus);
				break;

		}
	}

	public void ClickMountingItem()
	{
		if(isWearEquipmentInfo)
		{
			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].UnMountingItem();
			SlotChangeState(isWearEquipmentInfo);
		}
		else
		{
			inventory.itemSlots[saveInventorySlotIndex].MountingItem();
			SlotChangeState(isWearEquipmentInfo);
		}
	}

	public void SlotChangeState(bool _isWearEquipmentInfo)
	{
		if (_isWearEquipmentInfo)
		{
			saveInventorySlotIndex = equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].rememberInventoryIndex;

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].eSlotState = SLOT_STATE.NOT_MOUNTING;
			inventory.itemSlots[saveInventorySlotIndex].eSlotState = SLOT_STATE.NOT_MOUNTING;

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].SettingNoticeIcon();
			inventory.itemSlots[saveInventorySlotIndex].SettingNoticeIcon();
		}
		else 
		{
			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].eSlotState = SLOT_STATE.MOUNTING;
			inventory.itemSlots[saveInventorySlotIndex].eSlotState = SLOT_STATE.MOUNTING;

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].SettingNoticeIcon();
			inventory.itemSlots[saveInventorySlotIndex].SettingNoticeIcon();

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].rememberInventoryIndex = saveInventorySlotIndex;
		}
	}

	public string GetThousandCommaText(int data)
	{
		if (data == 0)
			return data.ToString();
		return string.Format("{0:#,###}", data);
	}

}
