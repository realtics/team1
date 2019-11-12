using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.08
 * 팀                 : 1팀
 * 스크립트 용도      : 특정값을 연산하여 해당 레이어의 위치를 이동시켜줌 (설정한 값과 카메라의 이동한 거리만큼 곱해서 위치를 이동)
 */

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    
    public void Move(float _delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= _delta * parallaxFactor;

        transform.localPosition = newPos;
    }
}
