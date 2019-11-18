using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.18
 * 팀                 : 1팀
 * 스크립트 용도      : 몬스터 이동 스크립트
 */


public class MonsterMove : MonoBehaviour
{
    private Transform m_monsterTransform;
    private Transform m_playerTransform;
    private CharacterMove m_characterMove;

    // Start is called before the first frame update
    void Awake()
    {
        m_monsterTransform = this.transform;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Move(float _speed)
    {
        if (m_playerTransform.position.x -  m_monsterTransform.position.x >0)
        {
            m_characterMove.MoveRight(_speed);
        }
        else
        {
            m_characterMove.MoveLeft(_speed);
        }
    }
}
