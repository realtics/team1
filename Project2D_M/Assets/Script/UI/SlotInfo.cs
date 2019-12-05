using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotInfo : MonoBehaviour
{
	public enum SLOT_TYPE
	{
		WEAPON,
		HAT,
		TOP,
		GLOVES,
		PANTS,
		SHOES,
		NECKLACE,
		EARRING_1,
		EARRING_2,
		RING_1,
		RING_2,
	}

	public SLOT_TYPE eSlotType;
	public string slotItemName; // 해당 칸의 장비 이름

	//누르기전 아이콘 info
	public Image imageFrame;
	public Image imageItem;
	public TextMeshProUGUI levelText;

	//상세보기 info
	public TextMeshProUGUI typeNameText;
	public TextMeshProUGUI itemNameText;
	public TextMeshProUGUI itemAttributeTypeText;
	public TextMeshProUGUI itemAttributeValueText;
	public Image detailItemImage;
	public Image detailItemFrame;
	public Image detailItemFrameEdge;


	//현재 플레이어가 장착된 정보로 초기화 해줘야함.
	private void Awake()
	{
		IconInitialized();
	}

	public void IconInitialized()
	{
		//플레이어 장착 정보(각 아이템 key값 ->string)에 따라 받아오고 인스펙터에서 해당 타입설정해 놓은거에 따라 슬롯 이름 설정됨.

		switch(eSlotType)
		{
			case SLOT_TYPE.WEAPON:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().weapon;
				break;
			case SLOT_TYPE.HAT:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().hat;
				break;
			case SLOT_TYPE.TOP:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().top;
				break;
			case SLOT_TYPE.GLOVES:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().gloves;
				break;
			case SLOT_TYPE.PANTS:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().pants;
				break;
			case SLOT_TYPE.SHOES:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().shoes;
				break;
			case SLOT_TYPE.NECKLACE:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().necklace;
				break;
			case SLOT_TYPE.EARRING_1:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().earring_one;
				break;
			case SLOT_TYPE.EARRING_2:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().earring_two;
				break;
			case SLOT_TYPE.RING_1:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().ring_one;
				break;
			case SLOT_TYPE.RING_2:
				slotItemName = PlayerDataManager.Inst.GetPlayerEquipInfo().ring_two;
				break;
		}

		if(slotItemName == null)
		{
			//입은게 없을 때 기본셋팅 
		}
	}

	//수정중
	public void SetIconInfoData()
	{
		itemNameText.text = slotItemName;
		typeNameText.text = ItemDataManager.Inst.GetItemInfoData(slotItemName).typeName;
		//itemAttributeTypeText.text = ItemDataManager.Inst.GetItemInfoData(slotItemName).;
	}


	private void OnValidate()
	{
		
	}
}
