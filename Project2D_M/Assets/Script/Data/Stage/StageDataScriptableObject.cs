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
		public StageDataManager.StageNameEnum maxStage = StageDataManager.StageNameEnum.STAGE_1_1;
		public List<StageData> MainStageData;
    }
}