using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 평타공격 함수
 */
public class NormalAttackCollider : MonoBehaviour
{
    private PlayerInfo m_playerInfo = null;
    [SerializeField] private int m_damage;

    private void Awake()
    {
        m_playerInfo = this.transform.parent.GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<ReceiveDamage>().Receive(m_damage);
        }
    }

    public void setDamage(float _damage)
    {
        m_damage = (int)((m_playerInfo.attack * _damage)+0.5f);
    }
}
