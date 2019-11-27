using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameDataManager))]
public class PlayerDataManager : Singletone<PlayerDataManager>
{
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

    [SerializeField] private PlayerData m_playerData;
    private GameDataManager m_dataManager;

    private void Awake()
    {
        m_dataManager = this.GetComponent<GameDataManager>();
    }

    private void Start()
    {
        InitPlayerData();
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
        GameSaveData.PlayerData playerData = m_dataManager.GetPlayerData();
        playerData.level = m_playerData.level;
        playerData.exp = m_playerData.exp;
        playerData.gold = m_playerData.gold;
        playerData.fatigability = m_playerData.fatigability;

        m_dataManager.SavePlayerData(playerData);
    }

    private void InitPlayerData()
    {
        m_playerData = new PlayerData();
        GameSaveData.PlayerData playerSaveData = m_dataManager.GetPlayerData();

        m_playerData.level = playerSaveData.level;
        m_playerData.exp = playerSaveData.exp;
        m_playerData.gold = playerSaveData.gold;
        m_playerData.cash = playerSaveData.cash;
        m_playerData.fatigability = playerSaveData.fatigability;

        m_playerData.attack = 10 + m_playerData.level * 9;
        m_playerData.defensive = 5 + m_playerData.level * 7;
        m_playerData.critical = 10 + m_playerData.level * 3;
        m_playerData.maxExp = 150 +  m_playerData.level * 15;
        m_playerData.maxHp = 100 + m_playerData.level * 20;
    }

    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 10, 200, 90), "level : " + m_playerData.level
    //        + "\nattack : " + m_playerData.attack
    //        + "\ndefensive : " + m_playerData.defensive
    //        + "\ncritical : " + m_playerData.critical
    //        + "\nmaxExp : " + m_playerData.maxExp);
    //}
}
