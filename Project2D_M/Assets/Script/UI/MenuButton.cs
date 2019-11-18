using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.11
 * 팀             : 1 Team
 * 스크립트 용도  : 타이틀 메뉴들의 공통 스크립트. 자신의 인덱스를 쉽게 인스펙터 창에서 설정하여 해당 인덱스에 따라 분기점이 나뉘며
 *                  메뉴의 애니메이션의 상태값을 바꿔주는 용도.
*/

public class MenuButton : MonoBehaviour
{
    enum MENU_INDEX
    {
        MENU_GAMESTART,
        MENU_GAMEEXIT,
        MENU_INDEX_END,
    };

    [SerializeField] MenuButtonController        m_cMenuButtonController;
    [SerializeField] Animator                    m_cAnimator;

    public int                                   thisMenuIndex;

    private ApplicationExit                      m_cApplicationExit;

    // Start is called before the first frame update
    void Start()
    {
        m_cApplicationExit = GetComponent<ApplicationExit>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_cMenuButtonController.menuIndex == thisMenuIndex)
        {
            m_cAnimator.SetBool("selected", true);

            if(Input.GetAxis("Submit")==1)
            {
                m_cAnimator.SetBool("pressed", true);
            }
            else if(m_cAnimator.GetBool("pressed"))
            {
                m_cAnimator.SetBool("pressed", false);

                // 씬 전환.
                if (thisMenuIndex == (int)MENU_INDEX.MENU_GAMESTART)
                {
                    LoadingProgress.LoadScene("02_LobyScene");
                }

                if (thisMenuIndex == (int)MENU_INDEX.MENU_GAMEEXIT)
                {
                    m_cApplicationExit.OnClickExit();
                }
            }
        }
        else
        {
            m_cAnimator.SetBool("selected", false);
        }
    }
}
