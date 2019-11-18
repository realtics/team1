using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.05
 * 팀             : 1 Team
 * 스크립트 용도  : 타이틀 메뉴 컨트롤러, 현재는 간단하게 키보드 w,s 와 화살표키 위 아래로 메뉴 선택(인덱스값)이 바뀌고
 *                  스페이스바나 엔터로 선택할수있도록 임시로 구현해놓았다. (수정사항 -> 터치 이벤트로 메뉴 선택으로 변경)
*/

public class MenuButtonController : MonoBehaviour
{
    public int               menuIndex;
    [SerializeField] bool    m_bKeyDown;
    [SerializeField] int     m_iMaxIndex;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            if(!m_bKeyDown)
            {
                if(Input.GetAxis("Vertical") < 0)
                {
                    if(menuIndex < m_iMaxIndex)
                    {
                        menuIndex++;
                    }
                    else
                    {
                        menuIndex = 0;
                    }
                }
                else if(Input.GetAxis("Vertical") > 0)
                {
                    if(menuIndex > 0)
                    {
                        menuIndex--;
                    }
                    else
                    {
                        menuIndex = m_iMaxIndex;
                    }
                }
                m_bKeyDown = true;
            }
        }
        else
        {
            m_bKeyDown = false;
        }
    }
}
