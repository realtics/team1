using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager :Singletone<StageManager>
{
    private Transform m_monsterTransform;
    private Transform m_playerTransform;
    private CharacterInfo m_playerInfo;
    private GameObject m_endUI;

    private int m_iMonsterCount = 0;
    private bool m_bUserDie = false;

    public int test = 0;

    public void Start()
    {
        m_playerInfo = m_playerTransform.GetComponent<CharacterInfo>();
        m_endUI = GameObject.Find("EndUI");
        m_endUI.SetActive(false);
    }
    public void Update()
    {
        m_bUserDie = m_playerInfo.IsCharacterDie();
        test = m_iMonsterCount;
        if(m_iMonsterCount <= 0 || m_bUserDie)
        {
            m_endUI.SetActive(true);
        }
        
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
}
