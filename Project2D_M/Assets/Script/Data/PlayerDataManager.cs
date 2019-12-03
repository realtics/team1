using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 데이터 접근 스크립트
 */
public class PlayerDataManager : Singletone<PlayerDataManager>
{
    public string dataname = "PlayerData.dat";
    public PlayerDataScriptableObject dataSO;

    private DataUIPlayerLeval m_dataUIPlayerLeval;

    public class PlayerData
    {
        public enum DATA_ENUM
        {
            DATA_ENUM_LEVEL,
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
		public int maxFatigability;
	}

    private PlayerData m_playerData = null;
    private PlayerSaveData m_playerSaveData = null;
    private void Awake()
    {
        m_dataUIPlayerLeval = GetComponentInChildren<DataUIPlayerLeval>();
        m_playerSaveData = BinaryManager.Load<PlayerSaveData>(dataname);
        if (m_playerSaveData == null)
            InitData();

        if (dataSO == null)
			dataSO = (PlayerDataScriptableObject)Resources.Load("Data/PlayerDataScript");

        m_playerSaveData.level = dataSO.level;
        m_playerSaveData.exp = dataSO.exp;
        m_playerSaveData.gold = dataSO.gold;
        m_playerSaveData.cash = dataSO.cash;
        m_playerSaveData.fatigability = dataSO.fatigability;

        //BinaryManager.Save(m_playerSaveData, dataname);
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
        m_playerSaveData.exp = m_playerData.exp;
        m_playerSaveData.gold = m_playerData.gold;
        m_playerSaveData.cash = m_playerData.cash;
        m_playerSaveData.fatigability = m_playerData.fatigability;

        dataSO.level = m_playerSaveData.level;
        dataSO.exp = m_playerSaveData.exp;
        dataSO.gold = m_playerSaveData.gold;
        dataSO.cash = m_playerSaveData.cash;
        dataSO.fatigability = m_playerSaveData.fatigability;

        //BinaryManager.Save(m_playerSaveData, dataname);
    }

    public void SavePlayerData(PlayerData.DATA_ENUM _data_enum, int _value)
    {
        switch (_data_enum)
        {
            case PlayerData.DATA_ENUM.DATA_ENUM_LEVEL:
                m_playerSaveData.level = _value;
                dataSO.level = m_playerSaveData.level;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_GOLD:
				//m_playerSaveData.gold += _value;
				dataSO.gold += _value;// = m_playerSaveData.gold;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_CASH:
                m_playerSaveData.cash = _value;
                dataSO.cash = m_playerSaveData.cash;
                break;
            case PlayerData.DATA_ENUM.DATA_ENUM_FATIGABILITY:
                m_playerSaveData.fatigability = _value;
                dataSO.fatigability = m_playerSaveData.fatigability;
                break;
        }

        //BinaryManager.Save(m_playerSaveData, dataname);
    }

    private void InitPlayerData()
    {
        m_playerData = new PlayerData();

        m_playerData.level = m_playerSaveData.level;
        m_playerData.exp = m_playerSaveData.exp;
        m_playerData.gold = m_playerSaveData.gold;
        m_playerData.cash = m_playerSaveData.cash;
        m_playerData.fatigability = m_playerSaveData.fatigability;

		LevelToPlayerData();
	}

    private void InitData()
    {
        m_playerSaveData = new PlayerSaveData();
        m_playerSaveData.charactorName = "name";
        m_playerSaveData.level = 1;
        m_playerSaveData.exp = 0;
        m_playerSaveData.gold = 0;
        m_playerSaveData.fatigability = 25;


        //BinaryManager.Save(m_playerSaveData, dataname);
    }

    public bool PlusExp(int _exp)
    {
        _exp += m_playerData.exp;

        if (m_playerData.maxExp <= _exp)
        {
            while (m_playerData.maxExp <= _exp)
            {
                _exp -= m_playerData.maxExp;
                m_playerData.level++;

				LevelToPlayerData();

				m_playerData.exp = _exp;
				if (m_playerData.fatigability < m_playerData.maxFatigability)
					m_playerData.fatigability = m_playerData.maxFatigability;
			}

			SavePlayerData(m_playerData);
			return true;
		}

        m_playerData.exp = _exp;
        SavePlayerData(m_playerData);

        return false;
    }

	private void LevelToPlayerData()
	{
		m_playerData.attack = 10 + m_playerData.level * 9;
		m_playerData.defensive = 5 + m_playerData.level * 7;
		m_playerData.critical = 10 + m_playerData.level * 3;
		m_playerData.maxExp = 40 * m_playerData.level;
		m_playerData.maxHp = 100 + m_playerData.level * 20;
		m_playerData.maxFatigability = 10 + m_playerData.level * 2;
	}
}