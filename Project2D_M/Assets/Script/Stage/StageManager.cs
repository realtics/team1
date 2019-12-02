using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : Singletone<StageManager>
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
	private TextMeshProUGUI m_stageRecordTimeText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageMonsterText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageMaxComboText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageAirAttckText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageOverKillText = null;
	[SerializeField]
	private TextMeshProUGUI m_rewardGoldText = null;
	[SerializeField]
	private TextMeshProUGUI m_rewardExpText = null;

	[SerializeField]
	private TextMeshProUGUI m_stageLvText = null;
	[SerializeField]
	private TextMeshProUGUI m_stageExpText = null;
	[SerializeField]
	private Image m_stageExpBar = null;
	[SerializeField]
	private TextMeshProUGUI m_stageMaxEndCurExp = null;




	private float m_stageTime = -3.0f;
	private bool m_bStageSuccess = false;

	public void Start()
	{
		m_playerInfo = m_playerTransform.GetComponent<CharacterInfo>();
		m_endUI = GameObject.Find("EndUI");
		m_endUI.SetActive(false);		
		m_bStageSuccess = false;
		m_stageTime = 0;
		m_fRewardTime = 3.0f;
	}
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		m_stageTime += deltaTime;
		m_bUserDie = m_playerInfo.IsCharacterDie();
		if ((m_iKillMonsterCount == m_iMaxMonsterNum ) && m_endUI.activeSelf == false)
		{
			m_bStageSuccess = true;
			StageEnd();
			m_endUI.SetActive(true);
			EndUIActive();
		}
		else if (m_bUserDie && m_endUI.activeSelf == false)
		{
			StageEnd();
			m_endUI.SetActive(true);
			EndUIActive();
		}
		if(m_endUI.activeSelf == true && m_fRewardTime >0)
		{
			m_fRewardTime -= deltaTime;
			if(m_fRewardTime<0)
			{
				m_rewardUI.SetActive(true);
				m_resultUiFail.SetActive(false);
				m_resultUiClear.SetActive(false);
				m_resultUiBG.SetActive(false);

				int nowStage = (int)StageDataManager.Inst.nowStage;
				PlayerDataManager.Inst.PlusExp(StageDataManager.Inst.stageDataSO.MainStageData[nowStage].rewardExp);
			}
		}


		if(m_rewardUI.activeSelf == true && m_bStageSuccess)
		{
			if (m_stageExpBar.fillAmount < PlayerDataManager.Inst.GetPlayerData().exp / PlayerDataManager.Inst.GetPlayerData().maxExp)
			{
				float tempSpeed = 0.5f;
				m_stageExpBar.fillAmount += tempSpeed * deltaTime;
			}
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
		RewardText();
	}

	[SerializeField]
	private GameObject m_rewardUI = null;
	[SerializeField]
	private GameObject m_resultUiBG = null;
	[SerializeField]
	private GameObject m_resultUiClear = null;
	[SerializeField]
	private GameObject m_resultUiFail = null;
	private float m_fRewardTime = 0;

	private void EndUIActive()
	{
		if(m_bStageSuccess)
		{
			m_resultUiBG.GetComponent<Image>().color = new Color((136.0f / 255.0f) , (106.0f / 255.0f) , (1.0f / 255.0f), 1.0f);
			m_resultUiClear.SetActive(true);
		}
		else
		{
			m_resultUiBG.GetComponent<Image>().color = new Color(15 / 255, 15 / 255, 15 / 255);
			m_resultUiFail.SetActive(true);
		}
	}

	private void RewardText()
	{
		if(m_bStageSuccess)
		{
			int nowStage = (int)StageDataManager.Inst.nowStage;
			m_rewardExpText.text = StageDataManager.Inst.stageDataSO.MainStageData[nowStage].rewardExp.ToString();
			m_rewardGoldText.text = StageDataManager.Inst.stageDataSO.MainStageData[nowStage].rewardGold.ToString();
		}
		else
		{
			m_rewardExpText.text = "000";
			m_rewardGoldText.text = "000";
		}

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

		m_stageLvText.text = "Lv ";
		m_stageLvText.text += PlayerDataManager.Inst.GetPlayerData().level.ToString();

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
		CheckTimeRecord(tempfloat);
		SetTimeText(m_stageTimeText, tempfloat);
	}

	private void CheckTimeRecord(int _nowTime)
	{
		int nowStage = (int)StageDataManager.Inst.nowStage;
		if(StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord > _nowTime || StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord == 0)
		{
			StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord = _nowTime;
		}
		SetTimeText(m_stageRecordTimeText, StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord);

	}

	private void SetTimeText(TextMeshProUGUI _text, int _time)
	{
		int ms = _time % 100;
		int sec = (_time-ms) / 100;
		int min = sec / 60;
		sec = sec % 60;

		string tempStr = "";
		tempStr += InputZeroStr(min);
		tempStr += InputZeroStr(sec);
		tempStr += ms.ToString();

		_text.text = tempStr;

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
