using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
