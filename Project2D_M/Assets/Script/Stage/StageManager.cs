using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager :Singletone<StageManager>
{
    //싱글턴에 접근하기 위한 Static 변수 선언
    //public static StageManager instance = null;

    private Transform m_monsterTransform;
    private Transform m_playerTransform;
    // Start is called before the first frame update

    //private StageManager() { }
    //private StageManager m_instance;
    //public StageManager Getinstance()
    //{
    //    if (m_instance == null)
    //        m_instance = new StageManager();
    //    return m_instance;
    //}

    //private void Awake()
    //{
    //    if(instance == null)

    //}



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
