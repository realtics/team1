using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class GameDataManager : MonoBehaviour
{
    public string dataname = "Data.dat";

    public PlayerDataScriptableObject dataIO;
    private SaveData data;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        data = BinaryManager.Load<SaveData>(Application.dataPath + dataname);
        if (data == null)
            InitData();

        if (dataIO == null)
            dataIO = (PlayerDataScriptableObject)Resources.Load("Data/PlayerDataScript");

        data.playerData.level = dataIO.level;
        data.playerData.exp = dataIO.exp;
        data.playerData.gold = dataIO.gold;
        data.playerData.cash = dataIO.cash;
        data.playerData.fatigability = dataIO.fatigability;

        BinaryManager.Save(data, dataname);
    }

    public PlayerData GetPlayerData()
    {
        return data.playerData;
    }

    public void SavePlayerData(GameSaveData.PlayerData _playerData)
    {
        data.playerData = _playerData;

        dataIO.level = data.playerData.level;
        dataIO.exp = data.playerData.exp;
        dataIO.gold = data.playerData.gold;
        dataIO.cash = data.playerData.cash;
        dataIO.fatigability = data.playerData.fatigability;

        BinaryManager.Save(data, dataname);
    }


    private void InitData()
    {
        data = new SaveData();
        data.playerData = new PlayerData();
        data.playerData.charactorName = "name";
        data.playerData.level = 1;
        data.playerData.exp = 0;
        data.playerData.gold = 0;
        data.playerData.fatigability = 25;

        data.stageData = new StageData();
        BinaryManager.Save(data, dataname);
    }
}