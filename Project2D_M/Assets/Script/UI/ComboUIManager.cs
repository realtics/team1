using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 콤보 수에 따른 콤보UI 출력 함수
 */
public class ComboUIManager : MonoBehaviour
{
    private ComboUI m_comboUI;
    private Image[] images;
    private void Awake()
    {
        m_comboUI = this.GetComponentInChildren<ComboUI>();
        images = this.GetComponentsInChildren<Image>();
    }

    public void ShowCombo(int _combo)
    {
        if (_combo > 0)
        {
            for (int i = 0; i < 2; ++i)
            {
                images[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < images.Length; ++i)
            {
                images[i].enabled = false;
            }
        }
        m_comboUI.SetComboUI(_combo);
    }
}
