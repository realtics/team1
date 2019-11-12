using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.08
 * 팀                 : 1팀
 * 스크립트 용도      : 시간차를 이용해 거리감 표현을 하기위한 레이어 관리용 코드
 *                      해당 코드 자식 오브젝트들에게 순차별로 레이어로 표기해주고 리스트에 넣어줍니다.
 */
public class ParallaxBackGround : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    private List<ParallaxLayer> m_lParallaxLayer = new List<ParallaxLayer>();
    // Start is called before the first frame update
    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.cameraTranslate += Move;
        SetLayer();
        
    }

    void SetLayer()
    {
        m_lParallaxLayer.Clear();
        for(int i = 0; i< transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if(layer != null)
            {
                layer.name = "Layer-" + i;
                m_lParallaxLayer.Add(layer);
            }
        }
    }

    void Move(float _delta)
    {
        foreach(ParallaxLayer layer in m_lParallaxLayer)
        {
            layer.Move(_delta);
        }
    }
}
