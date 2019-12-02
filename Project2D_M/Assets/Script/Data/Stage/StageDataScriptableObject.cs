using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;

namespace GameSaveDataIO
{
    [CreateAssetMenu(fileName = "StageDataSO"
                     , menuName = "StageGameData"
                     , order = 3)]
    [System.Serializable]
    public class StageDataScriptableObject : ScriptableObject
    {
        public List<StageData> MainStageData;
    }
}