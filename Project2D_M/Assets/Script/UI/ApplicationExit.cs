using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.06
 * 팀             : 1 Team
 * 스크립트 용도  : 메뉴 클릭시 어플리케이션 종료 함수
*/

public class ApplicationExit : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
