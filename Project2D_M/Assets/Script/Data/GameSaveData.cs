using System;
using System.Collections;
using System.Collections.Generic;
using GameSaveDataIO;
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
		public List<ItemInfoData> itemSaveList = new List<ItemInfoData>();
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

		}
	}

	[System.Serializable]
    public class AllStageData
    {
		public StageDataManager.StageNameEnum maxStage = StageDataManager.StageNameEnum.STAGE_1_1;
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