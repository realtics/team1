using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.11
 * 팀                 : 1팀
 * 스크립트 용도      : 다음 스테이지에 가기 위한 포탈 스크립트
 */
public class Portal : MonoBehaviour
{
    //해당 스테이지에 몬스터가 전부 죽였을 경우 활성화
    private bool m_bStageClear = false;


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(m_bStageClear && _collision.tag == "Player")
        {
            SceneManager.LoadScene("00_LoadingScene");
        }
    }
}
