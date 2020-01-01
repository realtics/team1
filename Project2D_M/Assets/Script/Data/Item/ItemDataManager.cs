using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class ItemDataManager : Singletone<ItemDataManager>, ISerializationCallbackReceiver
{
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

	private void Awake()
    {
		if (m_itemData == null)
			m_itemData = (ItemDataScriptableObject)Resources.Load("Data/ItemDataSO");

		for (int i = 0; i < m_itemData.ItemList.Count; i++)
        {
           m_itemDic.Add(m_itemData.ItemList[i].itemName, new ItemInfoData(m_itemData.ItemList[i].itemName,
																		   (ItemInfoData.ITEM_RATING)m_itemData.ItemList[i].itemRating,
																		   (ItemInfoData.ITEM_TYPE)m_itemData.ItemList[i].itemType,
																		   m_itemData.ItemList[i].level,
																		   m_itemData.ItemList[i].image));
        }
	}

	public ItemInfoData GetItemInfoData(string _itemName)
	{
		if (m_itemDic.TryGetValue(_itemName, out m_itemInfoData))
		{
			return m_itemInfoData;
		}
		else
		{
			return null;
		}
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
		m_itemDic = new Dictionary<string, ItemInfoData>();

		for (int i = 0; i < m_itemList.Count; i++)
		{
			m_itemDic.Add(m_itemList[i].itemName, m_itemList[i]);
		}
	}

    public void DeserializeDictionary()
    {
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
