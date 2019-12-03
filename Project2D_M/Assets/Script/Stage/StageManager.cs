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
    [SerializeField]
    private GameObject m_LevelUpUI = null;

    [SerializeField]
    private GameObject m_rewardUI = null;
    [SerializeField]
    private GameObject m_resultUiBG = null;
    [SerializeField]
    private GameObject m_resultUiClear = null;
    [SerializeField]
    private GameObject m_resultUiFail = null;
    private float m_fRewardTime = 0;

    private float m_stageTime = -3.0f;
	private bool m_bStageSuccess = false;
    private StageDataManager.StageNameEnum m_nowStageIndex;
    private float m_fRewardExp;
    private float m_fPrevExp;
    private float m_fPrevMaxExp;
    private float m_fCurrentExp;
    private float m_fCurrentMaxExp;

    public void Start()
	{
		m_playerInfo = m_playerTransform.GetComponent<CharacterInfo>();
		m_endUI = GameObject.Find("EndUI");
        m_endUI.SetActive(false);		
		m_bStageSuccess = false;
		m_stageTime = 0;
		m_fRewardTime = 3.0f;
        m_stageExpBar.fillAmount = (float)(PlayerDataManager.Inst.GetPlayerData().exp) / (float)(PlayerDataManager.Inst.GetPlayerData().maxExp);
        m_stageExpText.text = ((m_stageExpBar.fillAmount) * 100).ToString();
        m_stageExpText.text += "%";
        m_nowStageIndex = StageDataManager.Inst.nowStage;
        m_fRewardExp = StageDataManager.Inst.stageDataSO.MainStageData[(int)m_nowStageIndex].rewardExp;
    }

	public void FixedUpdate()
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
			m_fRewardTime -= Time.fixedDeltaTime;
			if(m_fRewardTime<=0)
			{

				if (m_bStageSuccess)
				{
					m_fPrevExp = (float)PlayerDataManager.Inst.GetPlayerData().exp;
					m_fPrevMaxExp = (float)PlayerDataManager.Inst.GetPlayerData().maxExp;
					PlayerDataManager.Inst.PlusExp(StageDataManager.Inst.stageDataSO.MainStageData[(int)m_nowStageIndex].rewardExp);
					m_fCurrentExp = (float)PlayerDataManager.Inst.GetPlayerData().exp;
					m_fCurrentMaxExp = (float)PlayerDataManager.Inst.GetPlayerData().maxExp;

					PlayerDataManager.Inst.SavePlayerData(PlayerDataManager.PlayerData.DATA_ENUM.DATA_ENUM_GOLD,
						StageDataManager.Inst.stageDataSO.MainStageData[(int)m_nowStageIndex].rewardGold);
				}

				m_rewardUI.SetActive(true);
				m_resultUiFail.SetActive(false);
				m_resultUiClear.SetActive(false);
				m_resultUiBG.SetActive(false);
				//StageEnd();

				UpdateMaxStage();
			}
		}


        if (m_rewardUI.activeSelf == true && m_bStageSuccess)
        {
            if (m_stageExpBar.fillAmount < ((float)m_fPrevExp + m_fRewardExp) / (float)m_fPrevMaxExp)
            {
                float tempSpeed = 0.3f;
                m_stageExpBar.fillAmount += tempSpeed * Time.fixedDeltaTime;
                m_stageExpText.text = (Mathf.Round(m_stageExpBar.fillAmount * 10000) / 100).ToString();
                m_stageExpText.text += "%";
            }
            
            if (m_stageExpBar.fillAmount >= 1)
            {
                m_fPrevExp = m_fCurrentExp;
                m_fPrevMaxExp = m_fCurrentMaxExp;
                m_stageExpBar.fillAmount = 0;
                m_fRewardExp = 0;
                m_LevelUpUI.SetActive(true);
				SetPlayerInfoInUI();
				Invoke(nameof(LevelUpUIOff), 2f);
            }
        }
        UpdatePlayerUI();
	}

    private void LevelUpUIOff()
    {
        m_LevelUpUI.SetActive(false);
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

	private void UpdateMaxStage()
	{
		if (StageDataManager.Inst.nowStage == StageDataManager.StageNameEnum.STAGE_1_1 && m_bStageSuccess)
			StageDataManager.Inst.stageDataSO.maxStage = StageDataManager.StageNameEnum.STAGE_1_2;
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


	private void EndUIActive()
	{
		if(m_bStageSuccess)
		{
            m_resultUiBG.GetComponent<Image>().color = new Color((136.0f / 255.0f), (106.0f / 255.0f), (1.0f / 255.0f), 230.0f / 255.0f);
			m_resultUiClear.SetActive(true);
		}
		else
		{
			m_resultUiBG.GetComponent<Image>().color = new Color(15f / 255f, 15f / 255f, 15f / 255f, 230f/255f);
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
		if (m_bStageSuccess)
		{
			SetTimeText(m_stageTimeText, tempfloat);

		}
	}

	private void CheckTimeRecord(int _nowTime)
	{
		int nowStage = (int)StageDataManager.Inst.nowStage;
		if((StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord > _nowTime || StageDataManager.Inst.stageDataSO.MainStageData[nowStage].timeRecord == 0)&&m_bStageSuccess)
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
