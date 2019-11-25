using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public class PlayerData
    {
        public int level;
        public int attack;
        public int defensive;
        public int critical;
        public int exp;
        public int maxExp;
        public int gold;
        public int fatigability; //피로도
    }

    private PlayerData m_playerData;
    private GameDataManager m_dataManager;

    private void Awake()
    {
        m_dataManager = this.GetComponent<GameDataManager>();
    }

    public PlayerData getPlayerData()
    {
        if (m_playerData == null)
        {
            InitPlayerData();
        }

        return m_playerData;
    }

    public void InitPlayerData()
    {
        m_playerData = new PlayerData();
        GameSaveData.PlayerData playerSaveData = m_dataManager.getPlayerData();

        m_playerData.level = playerSaveData.level;
        m_playerData.exp = playerSaveData.exp;
        m_playerData.gold = playerSaveData.gold;
        m_playerData.fatigability = playerSaveData.fatigability;

        m_playerData.attack = 10 + m_playerData.level * 9;
        m_playerData.defensive = 5 + m_playerData.level * 7;
        m_playerData.critical = 10 + m_playerData.level * 3;
        m_playerData.maxExp = 150 +  m_playerData.level * 15;
    }
}
