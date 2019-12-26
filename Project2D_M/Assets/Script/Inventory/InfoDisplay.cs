using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfoDisplay : MonoBehaviour
{
	[SerializeField] Inventory inventory = null;
	[SerializeField] EquipmentPanel equipmentPanel = null;

	private StringBuilder m_sbRatingAndType = new StringBuilder();

	public TextMeshProUGUI itemRatingAndType;
	public TextMeshProUGUI itemName;
	public Image backImage;
	public Image backFrame;
	public Image itemMainImage;

	public TextMeshProUGUI abilityTypeName;
	public TextMeshProUGUI abilityValue;

	private readonly string attackAbility = "공격력";
	private readonly string defenseAbility = "방어력";
	private readonly string maxHealthAbility = "최대 체력량";

	public bool isWearEquipmentInfo;

	public int saveEquipmentSlotIndex;
	public int saveInventorySlotIndex;

	public InfoText[] RefreshInfoList;

	public void ShowInfomation(EquippableItem _item)
	{
		m_sbRatingAndType.Length = 0;
		m_sbRatingAndType.Append(_item.ratingName);
		m_sbRatingAndType.Append(" ");
		m_sbRatingAndType.Append(_item.typeName);

		itemRatingAndType.text = m_sbRatingAndType.ToString();
		itemName.text = _item.itemName;
		backImage.color = new Color(_item.frameColorRGB.x / 255, _item.frameColorRGB.y / 255, _item.frameColorRGB.z / 255);
		backFrame.color = new Color(_item.frameColorRGB.x / 255, _item.frameColorRGB.y / 255, _item.frameColorRGB.z / 255);
		itemMainImage.sprite = _item.icon;
		itemMainImage.SetNativeSize();

		switch (_item.itemType)
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

	public void UpdateStatusInfo()
	{
		for (int i = 0; i < RefreshInfoList.Length; i++)
		{
			RefreshInfoList[i].RefreshStatus();
		}
	}

	public void ClickMountingItem()
	{
		inventory.itemSlots[saveInventorySlotIndex].MountingItem();
		SlotChangeState(isWearEquipmentInfo);
	}

	public void ClickUnMountingItem()
	{
		equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].UnMountingItem();
		SlotChangeState(isWearEquipmentInfo);
	}

	public void SlotChangeState(bool _isWearEquipmentInfo)
	{
		if (_isWearEquipmentInfo)
		{
			//착용일때

			saveInventorySlotIndex = equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].rememberInventoryIndex;

			//상태 바꾸고
			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].eSlotState = SLOT_STATE.NOT_MOUNTING;
			inventory.itemSlots[saveInventorySlotIndex].eSlotState = SLOT_STATE.NOT_MOUNTING;
			//아이콘 상태 바꾸고
			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].SettingNoticeIcon(true);
			inventory.itemSlots[saveInventorySlotIndex].SettingNoticeIcon(false);
		}
		else 
		{
			if(equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].eSlotState == SLOT_STATE.MOUNTING)
			{
				inventory.itemSlots[equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].rememberInventoryIndex].eSlotState = SLOT_STATE.NOT_MOUNTING;
				inventory.itemSlots[equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].rememberInventoryIndex].SettingNoticeIcon(false);
			}

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].eSlotState = SLOT_STATE.MOUNTING;
			inventory.itemSlots[saveInventorySlotIndex].eSlotState = SLOT_STATE.MOUNTING;

			equipmentPanel.equipmentSlots[saveEquipmentSlotIndex].SettingNoticeIcon(true);
			inventory.itemSlots[saveInventorySlotIndex].SettingNoticeIcon(false);

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
