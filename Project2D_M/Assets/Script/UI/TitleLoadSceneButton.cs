using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLoadSceneButton : MonoBehaviour
{
    enum MENU_INDEX
    {
        TITLE_TO_SELECT_CHARACTER,
        TITLE_TO_LOBBY,
        TITLE_TO_GAME_END,
        TITLE_MENU_INDEX_END
    }

    [SerializeField] private Animator m_cAnimator;
    private float m_fAnitmationTime = 0.5f;

    private ApplicationExit m_cApplicationExit;

    public int thisMenuIndex;
    void Start()
    {
        m_cApplicationExit = GetComponent<ApplicationExit>();
    }

    public void LoadSceneInTitle()
    {
        switch (thisMenuIndex)
        {
            case (int)MENU_INDEX.TITLE_TO_SELECT_CHARACTER:
                LoadingProgress.LoadScene("");
                break;
            case (int)MENU_INDEX.TITLE_TO_LOBBY:
                m_cAnimator.SetBool("pressed", true);
                StartCoroutine(nameof(WaitAnimationLoadScene), "02_LobyScene");
                break;
            case (int)MENU_INDEX.TITLE_TO_GAME_END:
                m_cApplicationExit.OnClickExit();
                break;
            default:
                break;
        }
    }

    IEnumerator WaitAnimationLoadScene(string _sceneName)
    {
        yield return new WaitForSeconds(m_fAnitmationTime);

        LoadingProgress.LoadScene(_sceneName);
    }

}
