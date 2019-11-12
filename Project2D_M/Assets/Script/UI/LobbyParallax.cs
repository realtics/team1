using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.11
 * 팀             : 1 Team
 * 스크립트 용도  : 로비용 parallax 효과 적용을 위함, 모작 메뉴가 실제론 parallax가 아니여서 일단 임시로 적용해놓음.
*/

public class LobbyParallax : MonoBehaviour
{
    private float   m_fLength;
    private float   m_fStartPosition;

    public GameObject cameraObject;
    public float    parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        m_fStartPosition = transform.position.x;
        m_fLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempDist = (cameraObject.transform.position.x * parallaxEffect);

        transform.position = new Vector3(m_fStartPosition + tempDist, transform.position.y, transform.position.z);

    }
}
