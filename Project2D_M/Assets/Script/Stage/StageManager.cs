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
    private int m_iMonsterCount = 0;
    private bool m_bUserDie = false;

    //playerUI _ hp
    [SerializeField]
    private TextMeshProUGUI m_plyaerHpText = null;
    [SerializeField]
    private CharacterHpBar m_playerHpBar = null;
    [SerializeField]
    private TextMeshProUGUI m_stageTimeObject = null;

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
        if((m_iMonsterCount <= 0 || m_bUserDie ) && m_endUI.activeSelf ==false)
        {
            EndTime();
            m_endUI.SetActive(true);
        }
        UpdatePlayerUI();


    }

    public void AddMonsterCount()
    {
        m_iMonsterCount++;
    }
    public void RemoveMonsterCount()
    {
        m_iMonsterCount--;
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

    private void EndTime()
    {
        int tempfloat = (int)Mathf.Round(m_stageTime * 100);

        int min = tempfloat / 1000;
        int sec;
        int ms;

        

        string tempStr;
        tempStr = ((int)m_stageTime).ToString();
        m_stageTimeObject.text = tempfloat.ToString();


    }

}
