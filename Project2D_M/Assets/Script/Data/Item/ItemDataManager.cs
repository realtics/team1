using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameSaveData;
using GameSaveDataIO;

public class ItemDataManager : Singletone<ItemDataManager>, ISerializationCallbackReceiver
{
    [SerializeField]
    private ItemDataScriptableObject m_itemData;

    [SerializeField]
    private List<string> m_itemNameKey = new List<string>();

    [SerializeField]
    private List<ItemInfoData> m_values = new List<ItemInfoData>();

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

        public ItemInfoData(ITEM_RATING _itemRating, ITEM_TYPE _itemType, int _level, Sprite _image)
        {
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

    private void Awake()
    {
        for (int i = 0; i < Mathf.Min(m_itemData.ItemNameKeyList.Count, m_itemData.ValueList.Count); i++)
        {
           m_itemDic.Add(m_itemData.ItemNameKeyList[i], new ItemInfoData((ItemInfoData.ITEM_RATING)m_itemData.ValueList[i].itemRating, (ItemInfoData.ITEM_TYPE)m_itemData.ValueList[i].itemType, m_itemData.ValueList[i].level, m_itemData.ValueList[i].image));
        }
    }

    public void OnBeforeSerialize()
    {
        if(modifyValues ==false)
        {
            m_itemNameKey.Clear();
            m_values.Clear();
            for(int i=0 ; i < Mathf.Min(m_itemData.ItemNameKeyList.Count, m_itemData.ValueList.Count); i++)
            {
                m_itemNameKey.Add(m_itemData.ItemNameKeyList[i]);
                m_values.Add(new ItemInfoData((ItemInfoData.ITEM_RATING)m_itemData.ValueList[i].itemRating, (ItemInfoData.ITEM_TYPE)m_itemData.ValueList[i].itemType, m_itemData.ValueList[i].level, m_itemData.ValueList[i].image));
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

        m_itemData.ItemNameKeyList.Clear();
        m_itemData.ValueList.Clear();

        for(int i = 0; i< Mathf.Min(m_itemNameKey.Count, m_values.Count); i++)
        {
            m_itemData.ItemNameKeyList.Add(m_itemNameKey[i]);
            m_itemData.ValueList.Add(new ItemDataScriptableObject.ItemInfoData((ItemDataScriptableObject.ItemInfoData.ITEM_RATING)m_values[i].itemRating, (ItemDataScriptableObject.ItemInfoData.ITEM_TYPE)m_values[i].itemType, m_values[i].level, m_values[i].image));
            m_itemDic.Add(m_itemNameKey[i], m_values[i]);
        }

        modifyValues = false;
    }
}
