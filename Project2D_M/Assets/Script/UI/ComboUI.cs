using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_28
 * 팀              : 1팀
 * 스크립트 용도   : 콤보 숫자 출력 함수
 */
public class ComboUI : MonoBehaviour
{
    private enum IMAGE_NUM
    {
        IMAGE_NUM_1,
        IMAGE_NUM_2,
        IMAGE_NUM_3,
        IMAGE_NUM_END
    }

    private Image[] Images;
    public Sprite[] comboSprites;
    private void Awake()
    {
        Images = this.GetComponentsInChildren<Image>();
    }

    public void SetComboUI(int _combo)
    {
        if (_combo > 0)
        {
            int maxCombo = (int)Mathf.Pow(10, (int)IMAGE_NUM.IMAGE_NUM_END);
            if (_combo >= maxCombo)
                _combo = maxCombo-1;

            string comboStr = _combo.ToString();

            char[] comboChars = new char[comboStr.Length];

            for (int i = 0; i < comboStr.Length; ++i)
            {
                comboChars[i] = comboStr[comboStr.Length - i - 1];
            }

            for (int i = 0; i < comboStr.Length; ++i)
            {
                Images[i].enabled = true;
                Images[i].sprite = comboSprites[(int)(comboChars[i] - '0')];
            }
        }else
        {
            for (int i = 0; i < Images.Length; ++i)
            {
                Images[i].enabled = false;
            }
        }
    }
}
