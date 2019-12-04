﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class ItemDataManager : Singletone<ItemDataManager>, ISerializationCallbackReceiver
{
	public string dataname = "ItemData.dat";

	[SerializeField]
    private ItemDataScriptableObject m_itemData;

    [SerializeField]
    private List<ItemInfoData> m_itemList = new List<ItemInfoData>();

    private Dictionary<string, ItemInfoData> m_itemDic = new Dictionary<string, ItemInfoData>();

    [System.Serializable]
    public class ItemInfoData
    {
        public enum ITEM_RATING
        {
            NOMAL,
            MAGIC,
            RARE,
            EPIC,
            UNIQUE,
        }
        public enum ITEM_TYPE
        {
            WEAPON,
            ARMOR,
            ACCESSORIES,
        }

		public string itemName;
        public int level;
        public Sprite image;

		public ITEM_RATING itemRating;
		public string ratingName;
        public int frameColorR;
        public int frameColorG;
        public int frameColorB;
        public int frameColorA;

		public ITEM_TYPE itemType;
		public string typeName;
        public int equipmentAttack;
        public int equipmentArmor;
        public int equipmentMaxHealth;

        public ItemInfoData(string _itemName, ITEM_RATING _itemRating, ITEM_TYPE _itemType, int _level, Sprite _image)
        {
			itemName = _itemName;
			itemRating = _itemRating;
            itemType = _itemType;
            level = _level;
            image = _image;

            switch(itemRating)
            {
                case ITEM_RATING.NOMAL:
                    ratingName = "일반";
                    frameColorR = 79;
                    frameColorG = 79;
                    frameColorB = 79;
                    frameColorA = 255;
                    break;
                case ITEM_RATING.MAGIC:
                    ratingName = "매직";
                    frameColorR = 68;
                    frameColorG = 102;
                    frameColorB = 28;
                    frameColorA = 255;
                    break;
                case ITEM_RATING.RARE:
                    ratingName = "레어";
                    frameColorR = 74;
                    frameColorG = 126;
                    frameColorB = 194;
                    frameColorA = 255;
                    break;
                case ITEM_RATING.EPIC:
                    ratingName = "에픽";
                    frameColorR = 76;
                    frameColorG = 22;
                    frameColorB = 117;
                    frameColorA = 255;
                    break;
                case ITEM_RATING.UNIQUE:
                    ratingName = "유니크";
                    frameColorR = 184;
                    frameColorG = 146;
                    frameColorB = 38;
                    frameColorA = 255;
                    break;
            }

            switch(itemType)
            {
                case ITEM_TYPE.WEAPON:
                    typeName = "무기";
                    break;
                case ITEM_TYPE.ARMOR:
                    typeName = "방어구";
                    break;
                case ITEM_TYPE.ACCESSORIES:
                    typeName = "악세서리";
                    break;
            }
        }
    }

    public bool modifyValues;


	private ItemInfoData m_itemInfoData = null;
	private ItemSaveData m_itemSaveData = null;

	private void Awake()
    {
        for (int i = 0; i < m_itemData.ItemList.Count; i++)
        {
           m_itemDic.Add(m_itemData.ItemList[i].itemName, new ItemInfoData(m_itemData.ItemList[i].itemName,
																		   (ItemInfoData.ITEM_RATING)m_itemData.ItemList[i].itemRating,
																		   (ItemInfoData.ITEM_TYPE)m_itemData.ItemList[i].itemType,
																		   m_itemData.ItemList[i].level,
																		   m_itemData.ItemList[i].image));
        }
    }

	public ItemInfoData GetItemInfoData()
	{
		return m_itemInfoData;
	}

	public void SaveItemData()
	{
		// 변환문제 때문에 고민중인 함수

		for (int i = 0; i < m_itemList.Count; i++)
		{
			m_itemSaveData.itemSaveList[i].itemName = m_itemList[i].itemName;
			m_itemSaveData.itemSaveList[i].level = m_itemList[i].level;
			m_itemSaveData.itemSaveList[i].image = m_itemList[i].image;
			m_itemSaveData.itemSaveList[i].itemRating = (ItemSaveData.ItemInfoData.ITEM_RATING)m_itemList[i].itemRating;
			m_itemSaveData.itemSaveList[i].ratingName = m_itemList[i].ratingName;
			m_itemSaveData.itemSaveList[i].frameColorR = m_itemList[i].frameColorR;
			m_itemSaveData.itemSaveList[i].frameColorG = m_itemList[i].frameColorG;
			m_itemSaveData.itemSaveList[i].frameColorB = m_itemList[i].frameColorB;
			m_itemSaveData.itemSaveList[i].frameColorA = m_itemList[i].frameColorA;
			m_itemSaveData.itemSaveList[i].itemType = (ItemSaveData.ItemInfoData.ITEM_TYPE)m_itemList[i].itemType;
			m_itemSaveData.itemSaveList[i].typeName = m_itemList[i].typeName;
			m_itemSaveData.itemSaveList[i].equipmentAttack = m_itemList[i].equipmentAttack;
			m_itemSaveData.itemSaveList[i].equipmentArmor = m_itemList[i].equipmentArmor;
			m_itemSaveData.itemSaveList[i].equipmentMaxHealth = m_itemList[i].equipmentMaxHealth;
		}

		BinaryManager.Save(m_itemSaveData, dataname);
	}

    public void OnBeforeSerialize()
    {
        if(modifyValues ==false)
        {
            m_itemList.Clear();

            for(int i=0 ; i <  m_itemData.ItemList.Count; i++)
            {
                m_itemList.Add(new ItemInfoData(m_itemData.ItemList[i].itemName,
											 (ItemInfoData.ITEM_RATING)m_itemData.ItemList[i].itemRating,
											 (ItemInfoData.ITEM_TYPE)m_itemData.ItemList[i].itemType,
											 m_itemData.ItemList[i].level,
											 m_itemData.ItemList[i].image));
            }
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        Debug.Log("Deserialize");
        m_itemDic = new Dictionary<string, ItemInfoData>();

        m_itemData.ItemList.Clear();

        for(int i = 0; i< m_itemList.Count; i++)
        {
            m_itemData.ItemList.Add(new ItemDataScriptableObject.ItemInfoData(m_itemList[i].itemName,
																			   (ItemDataScriptableObject.ItemInfoData.ITEM_RATING)m_itemList[i].itemRating,
																			   (ItemDataScriptableObject.ItemInfoData.ITEM_TYPE)m_itemList[i].itemType,
																			   m_itemList[i].level,
																			   m_itemList[i].image));
            m_itemDic.Add(m_itemList[i].itemName, m_itemList[i]);
        }

        modifyValues = false;
    }
}
