using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 몬스터 발이 땅에 닿고 떨어지는 상황에 대한 스크립트
 */
public class MonsterFootCollider : MonoBehaviour
{
    private MonsterAI m_monsterAI = null;

    private void Awake()
    {
        m_monsterAI = this.transform.parent.GetComponent<MonsterAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_monsterAI.SetMonsterPositionGround();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_monsterAI.SetMonsterPositionAir();
        }
    }
}
