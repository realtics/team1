using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager :Singletone<StageManager>
{
    private Transform m_monsterTransform;
    private Transform m_playerTransform;

    private int m_iMonsterCount = 0;
    private bool m_bUserDie;

    public int test = 0;
    public void Update()
    {
        test = m_iMonsterCount;
        if(m_iMonsterCount <= 0)
        {

        }
    }

    public void AddMonsterCount()
    {
        m_iMonsterCount++;
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
