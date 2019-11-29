using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager :Singletone<StageManager>
{
    private Transform m_monsterTransform;
    private Transform m_playerTransform;
    private CharacterInfo m_playerInfo;
    private GameObject m_endUI;

	[SerializeField]
	private int m_iKillMonsterCount = 0;
	[SerializeField]
	private int m_iOverKillMonsterCount = 0;
	[SerializeField]
	private int m_iMaxMonsterNum = 1;
    private bool m_bUserDie = false;

    //playerUI _ hp
    [SerializeField]
    private TextMeshProUGUI m_plyaerHpText = null;
    [SerializeField]
    private CharacterHpBar m_playerHpBar = null;

	//Stage endUI
    [SerializeField]
    private TextMeshProUGUI m_stageTimeText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageMonsterText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageMaxComboText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageAirAttckText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageOverKillText = null;


	private float m_stageTime = 0;

    public void Start()
    {
        m_playerInfo = m_playerTransform.GetComponent<CharacterInfo>();
        m_endUI = GameObject.Find("EndUI");
        m_endUI.SetActive(false);
    }
    public void Update()
    {
        m_stageTime += Time.deltaTime;
        m_bUserDie = m_playerInfo.IsCharacterDie();
        if((m_iKillMonsterCount == m_iMaxMonsterNum || m_bUserDie ) && m_endUI.activeSelf ==false)
        {
			StageEnd();
            m_endUI.SetActive(true);
        }
        UpdatePlayerUI();


    }

    public void SetMonsterCount(bool _overKill)
    {
		m_iKillMonsterCount++;
		if (_overKill)
			m_iOverKillMonsterCount++;
	}
    public Transform monsterTransform
    {
        get
        {
            return m_monsterTransform;
        }
        set
        {
            m_monsterTransform = value;
        }
    }
    
    public Transform playerTransform
    {
        get
        {
            return m_playerTransform;
        }

        set
        {
            m_playerTransform = value;
        }
    }

    private void UpdatePlayerUI()
    {
        m_plyaerHpText.text = m_playerInfo.GetHP().ToString();
        m_playerHpBar.SetHPBar(m_playerInfo);

    }

	private void StageEnd()
	{
		SetEndTime();
		SetKillCountInUI();
		SetPlayerInfoInUI();
		SetOverKillInUI();
	}

	private void SetOverKillInUI()
	{

		if (m_iOverKillMonsterCount < 10)
		{
			m_stageOverKillText.text = "0";
			m_stageOverKillText.text += m_iOverKillMonsterCount.ToString();
		}
		else
			m_stageOverKillText.text = m_iOverKillMonsterCount.ToString();

	}

	private void SetPlayerInfoInUI()
	{
		int temp = ((PlayerInfo)m_playerInfo).GetEndInfo().maxCombo;

		if(temp<10)
		{
			m_stageMaxComboText.text = "0";
			m_stageMaxComboText.text += temp.ToString();
		}
		else
			m_stageMaxComboText.text = temp.ToString();


		temp = ((PlayerInfo)m_playerInfo).GetEndInfo().airAttackCount;
		if (temp < 10)
		{
			m_stageAirAttckText.text = "0";
			m_stageAirAttckText.text += temp.ToString();
		}
		else
			m_stageAirAttckText.text = temp.ToString();

	}

	private void SetKillCountInUI()
	{
		if (m_iKillMonsterCount < 10)
		{
			m_stageMonsterText.text = "0";
			m_stageMonsterText.text += m_iKillMonsterCount.ToString();
		}
		else
			m_stageMonsterText.text = m_iKillMonsterCount.ToString();
	}

	private void SetEndTime()
    {
        int tempfloat = (int)Mathf.Round(m_stageTime * 100);

		int ms = tempfloat % 60;
        int sec = tempfloat / 60;
		int min = sec / 60;
		sec = sec % 60;

        string tempStr = "";
		tempStr += InputZeroStr(min);
        tempStr += InputZeroStr(sec);
		tempStr += ms.ToString();
		m_stageTimeText.text = tempStr;
	}

	private string InputZeroStr(int _num)
	{
		string str = "";
		if(_num <=0)
		{
			str += "00";
		}
		else if(_num < 10)
		{
			str +=  "0";
			str += _num.ToString();
		}
		else
		{
			str += _num.ToString();
		}

		str +=":";

		return str;
	}

}
