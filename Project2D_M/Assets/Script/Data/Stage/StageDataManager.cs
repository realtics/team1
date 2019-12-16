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
        STAGE_1_2 
    }

	public StageNameEnum nowStage;
    private AllStageData m_stageData = null;

    private void Awake()
    {
        //AllStageData m_stageData = BinaryManager.Load<AllStageData>(dataName);

        if (m_stageData == null)
            InitData();

        if (stageDataSO == null)
            stageDataSO = (StageDataScriptableObject)Resources.Load("Data/StageDataSO");

		ConnectData();

		//BinaryManager.Save(m_stageData, dataName);
    }

    private void InitData()
    {
        m_stageData = new AllStageData();
        BinaryManager.Save(m_stageData, dataName);
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
		m_stageData = new AllStageData();

		m_stageData.MainStageData = stageDataSO.MainStageData;
		m_stageData.maxStage = stageDataSO.maxStage;
	}
}
