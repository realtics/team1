using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.18
 * 팀             : 1 Team
 * 스크립트 용도  : 다음 씬 로딩 중 text(% 숫자 표기) 와 fillAmount을 이용하여 progressbar 효과 적용을 위함.
 *                  랜덤으로 로딩씬 이미지가 바뀜.
*/

public class LoadingProgress : MonoBehaviour
{
    //씬의 빌드 순서 넘버
    public static int nextSceneNum = 0;
    private static int m_iLoadingSceneNum = 1;

    [SerializeField] private Image m_imgProgressBar = null;
    [SerializeField] private TextMeshProUGUI m_textLoadingPersent = null;

    //c#의 Action 사용해봄. using System; 을 넣어주어야함.
    private Action<float> PrintTextPersent;

    private int randomImageNum = 0;
    [SerializeField] private GameObject m_backgroundObject = null;
    [SerializeField] private Sprite[] m_backgroundImages = null; 

    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        RandomBackgroundImage();

        StartCoroutine(nameof(LoadScene));

        //람다식으로 액션에 넣어주기
        PrintTextPersent = (float _fillamount) => { m_textLoadingPersent.text = ((int)(_fillamount * 100)).ToString() + "%"; };
    }

    public void RandomBackgroundImage()
    {
        randomImageNum = UnityEngine.Random.Range(0,m_backgroundImages.Length);

        m_backgroundObject.GetComponent<Image>().sprite = m_backgroundImages[randomImageNum];

        m_backgroundObject.GetComponent<Image>().SetNativeSize();
    }

    public static void LoadScene(int _sceneNum)
    {
        nextSceneNum = _sceneNum;
        SceneManager.LoadScene(m_iLoadingSceneNum);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneNum);
        asyncOperation.allowSceneActivation = false;

        float timer = 0.0f;

        while(!asyncOperation.isDone)
        {
            yield return null;

            timer += Time.deltaTime * 0.01f + 0.005f; // 너무 빨라서 임시로 가짜 로딩 시간 계산줌.

            //90퍼 아래일 때
            if(asyncOperation.progress < 0.9f)
            {
                ProgressBar(asyncOperation.progress, timer);

                if (m_imgProgressBar.fillAmount >= asyncOperation.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                ProgressBar(1f, timer);

                if (m_imgProgressBar.fillAmount == 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    public void ProgressBar(float _persent, float _time)
    {
        m_imgProgressBar.fillAmount = Mathf.Lerp(m_imgProgressBar.fillAmount, _persent, _time);

        PrintTextPersent(m_imgProgressBar.fillAmount);
    }


}
