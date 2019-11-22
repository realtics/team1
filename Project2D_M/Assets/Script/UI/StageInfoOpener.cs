using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.18
 * 팀             : 1 Team
 * 스크립트 용도  : 스테이지 선택 씬에서 스테이지 정보를 애니메이션 연출과 함께 open/close 해주는 스크립트.
 *                  
*/

public class StageInfoOpener : MonoBehaviour
{
    public GameObject stageInfoPanel;

    public GameObject stageSelect;


    public GameObject lobbyBackButton;
    public GameObject stageBackButton;

    public TextMeshProUGUI backButtonText;

    private float m_waitMenuAnimation = 0.5f;

    public void OpenPanel()
    {
        stageInfoPanel.SetActive(true);
        stageBackButton.SetActive(true);
        lobbyBackButton.SetActive(false);

        BackButtonTextChange("1.에페노바 숲");

        Animator animator = stageInfoPanel.GetComponent<Animator>();

        if (stageInfoPanel != null)
        {
            animator.SetBool("bOpen", true);
            stageSelect.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        Animator animator = stageInfoPanel.GetComponent<Animator>();

        BackButtonTextChange("월드맵");

        if (stageInfoPanel != null)
        {
            animator.SetBool("bOpen", false);
            StartCoroutine(nameof(WaitInOutPanel));
        }
    }

    void BackButtonTextChange(string _buttontext)
    {
        backButtonText.text = _buttontext;
    }


    IEnumerator WaitInOutPanel()
    {
        yield return new WaitForSeconds(m_waitMenuAnimation);

        stageSelect.SetActive(true);
        stageInfoPanel.SetActive(false);

        lobbyBackButton.SetActive(true);
        stageBackButton.SetActive(false);
    }

    
}
