using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSaveDataIO
{
    [CreateAssetMenu(fileName = "ItemDataScript", menuName = "ItemData", order = 2)]
    [System.Serializable]
    public class ItemDataScriptableObject : ScriptableObject
    {
        public Dictionary<string, ItemInfoData> itemDic = new Dictionary<string, ItemInfoData>();

        public class ItemInfoData
        {
            public enum ITEM_RATING
            {
                NOMAL,
                MAGIC,
                UNIQUE,
            }
            public enum ITEM_TYPE
            {
                WEAPON,
                ARMOR,
                ACCESSORIES,
            }

            public ITEM_RATING itemRating;
            public ITEM_TYPE itemType;
            public int level;
            public Sprite image;
            public Dictionary<ITEM_RATING, ItemRatingInfo> ratingDic = new Dictionary<ITEM_RATING, ItemRatingInfo>();
            public Dictionary<ITEM_TYPE, ItemTypeInfo> typeDic = new Dictionary<ITEM_TYPE, ItemTypeInfo>();

        }
        public class ItemRatingInfo
        {
            public string ratingName;
            public int frameColorR;
            public int frameColorG;
            public int frameColorB;
            public int frameColorA;
        }
        public class ItemTypeInfo
        {
            public string typeName;
            public int equipmentAttack;
            public int equipmentArmor;
            public int equipmentMaxHealth;
        }
    }
}

