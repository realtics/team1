using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class StageDataManager : Singletone<StageDataManager>
{
    public string dataName = "StageData.dat";
    public StageDataScriptableObject stageDataSO;
    // Start is called before the first frame update
    public enum StageNameEnum
    {
        STAGE_1_1,
        STAGE_1_2,
		STAGE_1_3,
		STAGE_1_4
	}

	public StageNameEnum nowStage;
    private AllStageData m_stageData = null;

    private void Awake()
    {
        m_stageData = BinaryManager.Load<AllStageData>(dataName);

        if (stageDataSO == null)
            stageDataSO = (StageDataScriptableObject)Resources.Load("Data/StageDataSO");

        if (m_stageData == null)
            InitData();

		ConnectData();

		BinaryManager.Save(m_stageData, dataName);
    }

    private void InitData()
    {
        m_stageData = new AllStageData();

        m_stageData.MainStageData = stageDataSO.MainStageData;
        m_stageData.maxStage = stageDataSO.maxStage;
    }


    public AllStageData GetStageData()
    {
        //if(m_stageData==null)
        //{

        //}
        return m_stageData;
    }

    public void SaveStageData()
    {
		m_stageData.MainStageData = stageDataSO.MainStageData;
		m_stageData.maxStage = stageDataSO.maxStage;
		BinaryManager.Save(m_stageData, dataName);
    }

    public void ConnectData()
    {

        if (m_stageData.MainStageData.Count == 0)
        {
            for (int i = 0; i < stageDataSO.MainStageData.Count; i++)
            {
                m_stageData.MainStageData.Insert(i, stageDataSO.MainStageData[i]);
            }
        }
        else
        {
            if (m_stageData.MainStageData.Count == stageDataSO.MainStageData.Count)
            {
                stageDataSO.MainStageData = m_stageData.MainStageData;
            }
            else
            {
                int temp = stageDataSO.MainStageData.Count - m_stageData.MainStageData.Count;

                for(int i = (m_stageData.MainStageData.Count -1); i< stageDataSO.MainStageData.Count; i++)
                {
                    m_stageData.MainStageData.Insert(i, stageDataSO.MainStageData[i]);
                }
            }
        }
        stageDataSO.maxStage = m_stageData.maxStage;
    }
}
