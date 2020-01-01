using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.21
 * 팀             : 1 Team
 * 스크립트 용도  : 오브젝트를 컨트롤 할수있는 이벤트 용으로 만든 버튼 스크립트.
*/

public class BaseButtonEvent : MonoBehaviour
{
    public GameObject selectObject;
    public int eventNum;
    public void OpenEventButton() 
    { 
        if(!selectObject)
        {
            return;
        }
        selectObject.SetActive(true);
    }

    public void CloseEventButton()
    {
        if (!selectObject)
        {
            return;
        }
        selectObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        Animator animator = selectObject.GetComponent<Animator>();
   
       if(!animator.GetBool("bOpen"))
        {
            animator.SetBool("bOpen", true);
        }
        else
        {
            animator.SetBool("bOpen", false);
        }
    }


}
