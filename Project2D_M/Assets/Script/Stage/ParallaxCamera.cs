using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.08
 * 팀                 : 1팀
 * 스크립트 용도      : 시간차를 이용해 거리감 표현을 하기위한 카메라 코드.
 */
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate cameraTranslate;
    private float m_fOldPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_fOldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x != m_fOldPosition)
        {
            if(cameraTranslate != null)
            {
                float delta = m_fOldPosition - transform.position.x;

                cameraTranslate(delta);
            }
            m_fOldPosition = transform.position.x;
        }
    }
}
