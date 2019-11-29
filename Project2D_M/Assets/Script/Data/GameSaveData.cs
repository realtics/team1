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
        public int level;
        public Sprite image;
        public Dictionary<string, ItemRatingInfo> ratingDic;
        public string type;

    }

    [System.Serializable]
    public class ItemRatingInfo
    {
        public int frameColorR;
        public int frameColorG;
        public int frameColorB;
        public int frameColorA;
    }


}