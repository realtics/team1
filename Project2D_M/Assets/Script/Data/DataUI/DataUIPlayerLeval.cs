using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUIPlayerLeval : MonoBehaviour
{
    private int m_iPrevPlayerLevel;

    private int m_iCurrentPlayerLevel;

    public void ShowPlayerLevelData(int _prevLevel,int _currentLevel)
    {
        m_iPrevPlayerLevel = _prevLevel;
        m_iCurrentPlayerLevel = _currentLevel;
    }
}
