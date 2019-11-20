using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.14
 * 팀             : 1 Team
 * 스크립트 용도  : 인벤토리에서 장비나 소모품 아이콘 설정을 위한 스크립트.
*/

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image m_imgSlot = null;

    public void SetIcon(Sprite _image)
    {
        m_imgSlot.sprite = _image;
    }

}
