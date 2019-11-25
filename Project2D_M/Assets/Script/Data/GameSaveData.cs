using System.Collections;
using System.Collections.Generic;

namespace GameSaveData
{
    [System.Serializable]
    public class SaveData
    {
        public PlayerData playerData;
        public StageData stageData;
    }

    [System.Serializable]
    public class PlayerData
    {
        public string charactorName;
        public int level;
        public int exp;
        public int gold;
        public int fatigability;
    }

    [System.Serializable]
    public class StageData
    {

    }
}