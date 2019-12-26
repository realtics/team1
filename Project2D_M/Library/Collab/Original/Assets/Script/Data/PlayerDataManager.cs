using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class PlayerDataManager : Singletone<PlayerDataManager>
{
    public string dataname = "PlayerData.dat";
    public PlayerDataScriptableObject dataIO;
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int defensive;
        public int critical;
        public int exp;
        public int maxExp;
        public int gold;
        public int cash;
        public int fatigability; //피로도
    }

    private PlayerData m_playerData = null;
    private PlayerSaveData m_playerSaveData = null;
    private void Awake()
    {
        m_playerSaveData = BinaryManager.Load<PlayerSaveData>(dataname);
        if (m_playerSaveData == null)
            InitData();

        if (dataIO == null)
            dataIO = (PlayerDataScriptableObject)Resources.Load("Data/PlayerDataScript");

        m_playerSaveData.level = dataIO.level;
        m_playerSaveData.exp = dataIO.exp;
        m_playerSaveData.gold = dataIO.gold;
        m_playerSaveData.cash = dataIO.cash;
        m_playerSaveData.fatigability = dataIO.fatigability;

        BinaryManager.Save(m_playerSaveData, dataname);
    }

    public PlayerData GetPlayerData()
    {
        if (m_playerData == null)
        {
            InitPlayerData();
        }

        return m_playerData;
    }

    public void SavePlayerData()
    {
        m_playerSaveData.level = m_playerData.level;
        m_playerSaveData.exp = m_playerData.exp;
        m_playerSaveData.gold = m_playerData.gold;
        m_playerSaveData.fatigability = m_playerData.fatigability;

        dataIO.level = m_playerSaveData.level;
        dataIO.exp = m_playerSaveData.exp;
        dataIO.gold = m_playerSaveData.gold;
        dataIO.cash = m_playerSaveData.cash;
        dataIO.fatigability = m_playerSaveData.fatigability;

        BinaryManager.Save(m_playerSaveData, dataname);
    }

    private void InitPlayerData()
    {
        m_playerData = new PlayerData();

        m_playerData.level = m_playerSaveData.level;
        m_playerData.exp = m_playerSaveData.exp;
        m_playerData.gold = m_playerSaveData.gold;
        m_playerData.cash = m_playerSaveData.cash;
        m_playerData.fatigability = m_playerSaveData.fatigability;

        m_playerData.attack = 10 + m_playerData.level * 9;
        m_playerData.defensive = 5 + m_playerData.level * 7;
        m_playerData.critical = 10 + m_playerData.level * 3;
        m_playerData.maxExp = 150 +  m_playerData.level * 15;
        m_playerData.maxHp = 100 + m_playerData.level * 20;
    }

    private void InitData()
    {
        m_playerSaveData = new PlayerSaveData();
        m_playerSaveData.charactorName = "name";
        m_playerSaveData.level = 1;
        m_playerSaveData.exp = 0;
        m_playerSaveData.gold = 0;
        m_playerSaveData.fatigability = 25;
        BinaryManager.Save(m_playerSaveData, dataname);
    }
}
