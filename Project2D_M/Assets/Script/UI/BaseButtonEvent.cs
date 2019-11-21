﻿using System.Collections;
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

    public void OpenEventButton()
    {
        if(!selectObject)
        {
            Debug.Log("오픈할 버튼이 없습니다.");
            return;
        }
        selectObject.SetActive(true);
    }

    public void CloseEventButton()
    {
        if (!selectObject)
        {
            Debug.Log("닫을 버튼이 없습니다.");
            return;
        }
        selectObject.SetActive(false);
    }
}
