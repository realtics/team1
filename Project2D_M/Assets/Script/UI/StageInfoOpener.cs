using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public enum STAGE_NUM_TYPE
    {
        STAGE_01_1,
        STAGE_01_2,
        STAGE_01_3,
        STAGE_01_4,
        STAGE_01_5,
    }

    public GameObject stageInfoPanel;
    public GameObject stageSelect;

    public GameObject lobbyBackButton;
    public GameObject stageBackButton;

    public TextMeshProUGUI backButtonText;
    public Sprite[] stageImages;

    private float m_waitMenuAnimation = 0.5f;

    public STAGE_NUM_TYPE stageMenuNumber;
    public Image selectStageImage;
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI stageExplanationText;
    public TextMeshProUGUI rewordGold;
    public TextMeshProUGUI rewordExp;

	public GameObject StageStartButton2;

    public void OpenPanel()
    {
        SetStageInfo();

        stageInfoPanel.SetActive(true);
        stageBackButton.SetActive(true);
        lobbyBackButton.SetActive(false);

        BackButtonTextChange("뒤로가기");

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

    
    void SetStageInfo()
    {
        switch(stageMenuNumber)
        {
            case STAGE_NUM_TYPE.STAGE_01_1:
                selectStageImage.sprite = stageImages[(int)STAGE_NUM_TYPE.STAGE_01_1];
                stageName.text = "[1-1] 근교 숲 어귀";
                stageExplanationText.text = "그라노스 왕성마을 바로 근처 에페노바 근교 숲 어귀";
                rewordGold.text = GetThousandCommaText(320);
                rewordExp.text = GetThousandCommaText(50);
				StageStartButton2.GetComponent<StageLoadSceneButton>().wantToGoSceneName = LoadSceneButton.SCENE_NAME.STAGE_1;
				StageDataManager.Inst.nowStage = StageDataManager.StageNameEnum.STAGE_1_1;

				break;
            case STAGE_NUM_TYPE.STAGE_01_2:
                selectStageImage.sprite = stageImages[(int)STAGE_NUM_TYPE.STAGE_01_2];
                stageName.text = "[1-2] 근교 숲 진입로";
                stageExplanationText.text = "에페노바 근교 숲으로 들어서는 진입로";
                rewordGold.text = GetThousandCommaText(500);
                rewordExp.text = GetThousandCommaText(70);
				StageStartButton2.GetComponent<StageLoadSceneButton>().wantToGoSceneName = LoadSceneButton.SCENE_NAME.STAGE_2;
				StageDataManager.Inst.nowStage = StageDataManager.StageNameEnum.STAGE_1_2;

				break;
            case STAGE_NUM_TYPE.STAGE_01_3:
                selectStageImage.sprite = stageImages[(int)STAGE_NUM_TYPE.STAGE_01_3];
                stageName.text = "[1-3] 중간 숲 푹신한 흙바닥";
                stageExplanationText.text = "흙이 유달리 푹신하게 깔려 있는 중간 숲 한구석";
                rewordGold.text = GetThousandCommaText(1000);
                rewordExp.text = GetThousandCommaText(100);
				StageStartButton2.GetComponent<StageLoadSceneButton>().wantToGoSceneName = LoadSceneButton.SCENE_NAME.STAGE_3;
				StageDataManager.Inst.nowStage = StageDataManager.StageNameEnum.STAGE_1_3;
				break;
            case STAGE_NUM_TYPE.STAGE_01_4:
                selectStageImage.sprite = stageImages[(int)STAGE_NUM_TYPE.STAGE_01_4];
                stageName.text = "[1-4] 중간 숲 고블린 배회지역";
                stageExplanationText.text = "에페노바 중간 숲의 고블린 영토 변투리 지역";
                rewordGold.text = GetThousandCommaText(1200);
                rewordExp.text = GetThousandCommaText(140);
				StageStartButton2.GetComponent<StageLoadSceneButton>().wantToGoSceneName = LoadSceneButton.SCENE_NAME.STAGE_4;
				StageDataManager.Inst.nowStage = StageDataManager.StageNameEnum.STAGE_1_4;
				break;
            case STAGE_NUM_TYPE.STAGE_01_5:
                selectStageImage.sprite = stageImages[(int)STAGE_NUM_TYPE.STAGE_01_5];
                stageName.text = "[1-5] 중간 숲 폭군의 영토";
                stageExplanationText.text = "난폭한 대장 멧돼지 펭크스가 살고 있는 지역";
                rewordGold.text = GetThousandCommaText(2000);
                rewordExp.text = GetThousandCommaText(240);
                break;
        }
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
