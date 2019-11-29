using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 데이터 접근 스크립트
 */
public class PlayerDataManager : Singletone<PlayerDataManager>
{
    public string dataname = "PlayerData.dat";
    public PlayerDataScriptableObject dataIO;
    public class PlayerData
    {
        public enum DATA_ENUM
        {
            DATA_ENUM_LEVEL,
            DATA_ENUM_EXP,
            DATA_ENUM_GOLD,
            DATA_ENUM_CASH,
            DATA_ENUM_FATIGABILITY
        }

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

    public void SavePlayerData(PlayerData _playerData)
    {
        m_playerData = _playerData;

        m_playerSaveData.level = m_playerData.level;
        m_playerSaveData.exp = LevelUp(_playerData.exp);
        m_playerSaveData.gold = m_playerData.gold;
        m_playerSaveData.cash = m_playerData.cash;
        m_playerSaveData.fatigability = m_playerData.fatigability;

        dataIO.level = m_playerSaveData.level;
        dataIO.exp = m_playerSaveData.exp;
        dataIO.gold = m_playerSaveData.gold;
        dataIO.cash = m_playerSaveData.cash;
        dataIO.fatigability = m_playerSaveData.fatigability;

        BinaryManager.Save(m_playerSaveData, dataname);
    }

    public void SavePlayerData(PlayerData.DATA_ENUM _data_enum, int _value)
    {
        switch (_data_enum)
        {
            case PlayerData.DATA_ENUM.DATA_ENUM_LEVEL:
                m_playerSaveData.level = _value;
                dataIO.level = m_playerSaveData.level;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_EXP:
                m_playerSaveData.exp = LevelUp(_value);
                dataIO.exp = m_playerSaveData.exp;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_GOLD:
                m_playerSaveData.gold = _value;
                dataIO.gold = m_playerSaveData.gold;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_CASH:
                m_playerSaveData.cash = _value;
                dataIO.cash = m_playerSaveData.cash;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_FATIGABILITY:
                m_playerSaveData.fatigability = _value;
                dataIO.fatigability = m_playerSaveData.fatigability;
                break;
        }

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
        m_playerData.maxExp = 150 + m_playerData.level * 15;
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

    private int LevelUp(int _exp)
    {
        if (m_playerData.maxExp <= _exp)
        {
            m_playerSaveData.level++;

            m_playerData.attack = 10 + m_playerData.level * 9;
            m_playerData.defensive = 5 + m_playerData.level * 7;
            m_playerData.critical = 10 + m_playerData.level * 3;
            m_playerData.maxExp = 150 + m_playerData.level * 15;
            m_playerData.maxHp = 100 + m_playerData.level * 20;

            _exp -= m_playerData.maxExp;
        }
        return _exp;
    }
}