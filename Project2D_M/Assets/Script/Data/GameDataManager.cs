using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;

public class GameDataManager : Singletone<GameDataManager>
{
    public string dataname = "Data.dat";

    private SaveData data;
    private void Awake()
    {
        data = BinaryManager.Load<SaveData>(dataname);
        if(data == null)
            InitData();
    }

    public PlayerData getPlayerData()
    {
        return data.playerData;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 20), "Save"))
        {
            BinaryManager.Save(data, dataname);
        }

        if (GUI.Button(new Rect(10, 30, 200, 20), "LV_Up"))
        {
            data.playerData.level += 1;
        }

        GUI.Box(new Rect(10, 60, 100, 20), "Level :" + data.playerData.level);
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
    }
}