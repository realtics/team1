using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.26
 * 팀             : 1 Team
 * 스크립트 용도  : enum 종류마다 이미지 FillAmount를 표현 하고 숫자표기에 들어갈 내용을 바이너리 파일에서 읽어서 출력되도록 하는 스크립트.
*/

public class InfoFillAmount : MonoBehaviour
{
    public enum INFO_FILL_TYPE
    {
        EXP,
    }

    public INFO_FILL_TYPE kindOfFillData;
    private float m_fMaxValue;
    private float m_fCurrnetValue;

    [SerializeField] private Image m_imgFillBar = null;
    [SerializeField] private TextMeshProUGUI m_textPersent = null;
    private void Awake()
    {
        Initialized();
    }

    private void Initialized()
    {
        switch(kindOfFillData)
        {
            case INFO_FILL_TYPE.EXP:
                m_fMaxValue = PlayerDataManager.Inst.GetPlayerData().maxExp;
                m_fCurrnetValue = PlayerDataManager.Inst.GetPlayerData().exp;
                break;
        }

        AverageFillBar();
        AverageText();
    }

    void AverageFillBar()
    {
        if(!m_imgFillBar)
        {
            return;
        }

        m_imgFillBar.fillAmount = m_fCurrnetValue / m_fMaxValue;
    }
    
    void AverageText()
    {
        if (!m_textPersent)
        {
            return;
        }

        m_textPersent.text = UpToTheSecondDecimalPlace(m_fCurrnetValue / m_fMaxValue * 100.0f) +"%";
    }

    public string UpToTheSecondDecimalPlace(float data)
    {
        return string.Format("{0:f2}", data); 
    }
}