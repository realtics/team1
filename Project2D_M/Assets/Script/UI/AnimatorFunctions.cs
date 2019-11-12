using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.05
 * 팀             : 1 Team
 * 스크립트 용도  : 후에 오디오 사운드 출력 등 메뉴에 필요함 함수들을 여기에 작성할 예정. 사운드 출력 확인만 하고 닫아놓은 상태.
*/

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] MenuButtonController m_cMenuButtonController;
    public bool                           isSoundPlayOnce;
    
   public void PlaySound(AudioClip _whichSound)
    {
        //if(!m_bDisableOnce)
        //{
          //  m_cMenuButtonController.m_cAudioSource.PlayOneShot(_whichSound);
        //}
        //else
        //{
        //    m_bDisableOnce = false;
        //}
    }
}
