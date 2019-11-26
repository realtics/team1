using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSaveDataIO
{
    [CreateAssetMenu(fileName = "PlayerDataScript", menuName = "PlayerData", order = 1)]
    [System.Serializable]
    public class PlayerDataScriptableObject : ScriptableObject
    {
        public string charactorName;
        public int level;
        public int exp;
        public int gold;
        public int cash;
        public int fatigability;
    }
}