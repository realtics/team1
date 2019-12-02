using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우, 박종현
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 데이터, 아이템 데이터 구조
 */
namespace GameSaveData
{
    [System.Serializable]
    public class PlayerSaveData
    {
        public string charactorName;
        public int level;
        public int exp;
        public int gold;
        public int cash;
        public int fatigability; //피로도

        //아이템 장착 정보 추가해야함
    }

    [System.Serializable]
    public class ItemSaveData
    {
		public Dictionary<string, ItemInfoData> itemDic;
	}

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

        public ITEM_RATING itemRating;
        public ITEM_TYPE itemType;
        public int level;
        public Sprite image;
        public Dictionary<ITEM_RATING, ItemRatingInfo> ratingDic;
        public Dictionary<ITEM_TYPE, ItemTypeInfo> typeDic;

		public ItemInfoData(ITEM_RATING _itemRating, ITEM_TYPE _itemType, int _level, Sprite _image)
		{
			itemRating = _itemRating;
			itemType = _itemType;
			level = _level;
			image = _image;
			ratingDic = new Dictionary<ITEM_RATING, ItemRatingInfo>();
			typeDic = new Dictionary<ITEM_TYPE, ItemTypeInfo>();

			ratingDic.Add(ITEM_RATING.NOMAL, new ItemRatingInfo("일반", 79, 79, 79, 255));
			ratingDic.Add(ITEM_RATING.MAGIC, new ItemRatingInfo("매직", 68, 102, 28, 255));
			ratingDic.Add(ITEM_RATING.RARE, new ItemRatingInfo("레어", 74, 126, 194, 255));
			ratingDic.Add(ITEM_RATING.EPIC, new ItemRatingInfo("에픽", 76, 22, 117, 255));
			ratingDic.Add(ITEM_RATING.UNIQUE, new ItemRatingInfo("유니크", 184, 146, 38, 255));

			//typeDic.Add(ITEM_TYPE.WEAPON, new ItemTypeInfo("무기",))

		}
	}

    [System.Serializable]
    public class ItemRatingInfo
    {
        public string ratingName;
        public int frameColorR;
        public int frameColorG;
        public int frameColorB;
        public int frameColorA;

		public ItemRatingInfo(string _ratingName, int _frameColorR, int _frameColorG, int _frameColorB
			, int _frameColorA)
		{
			ratingName = _ratingName;
			frameColorR = _frameColorR;
			frameColorG = _frameColorG;
			frameColorB = _frameColorB;
			frameColorA = _frameColorA;
		}
	}

    [System.Serializable]
    public class ItemTypeInfo
    {
        public string typeName;
        public int equipmentAttack;
        public int equipmentArmor;
        public int equipmentMaxHealth;

		public ItemTypeInfo(string _typeName, int _equipmentAttack, int _equipmentArmor, int _equipmentMaxHealth)
		{
			typeName = _typeName;
			equipmentAttack = _equipmentAttack;
			equipmentArmor = _equipmentArmor;
			equipmentMaxHealth = _equipmentMaxHealth;
		}
	}

    [System.Serializable]
    public class AllStageData
    {
        public List<StageData> MainStageData = new List<StageData>();
    }

    [System.Serializable]
    public class StageData
    {
        public int rewardExp = 0;
        public int rewardGold = 0;
        public int monsterNum = 0;
        public int monsterLevel = 1;

        public int timeRecord = 0;
    }
}