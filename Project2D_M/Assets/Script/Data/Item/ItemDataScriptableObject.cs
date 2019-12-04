using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSaveDataIO
{
    [CreateAssetMenu(fileName = "ItemDataScript", menuName = "ItemData", order = 2)]
    [System.Serializable]
    public class ItemDataScriptableObject : ScriptableObject
    {
        [SerializeField]
        List<ItemInfoData> itemList = new List<ItemInfoData>();

        public List<ItemInfoData> ItemList { get => itemList; set => itemList = value; }

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

                switch (itemRating)
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

                switch (itemType)
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
    }
}

