using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 공격판정 담당하는 콜라이더 관련 스크립트
 */
public class AttackCollider : MonoBehaviour
{
    private PlayerInfo m_playerInfo = null;
    private string m_sTagName = null;
    private PolygonCollider2D m_collider = null;
    private SpineAnimCollider m_spineAnimCollider = null;
    [SerializeField] private int m_damage;
    [SerializeField] private Vector2 attackForce;

    private void Awake()
    {
        m_playerInfo = this.transform.root.GetComponent<PlayerInfo>();
        m_collider = this.GetComponent<PolygonCollider2D>();
        m_spineAnimCollider = this.GetComponent<SpineAnimCollider>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_sTagName)
        {
            Debug.Log(collision.gameObject.name);
            ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();
            receiveDamage.Receive(m_damage);
            receiveDamage.AddDamageForce(attackForce);
        }
    }

    public void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
    {
        m_sTagName = _tagName;
        m_damage = (int)((m_playerInfo.attack * _damage) + 0.5f);

        if ((this.transform.root.transform.localScale.x > 0 && _attackForce.x < 0) ||
            (this.transform.root.transform.localScale.x < 0 && _attackForce.x > 0))
        {
            _attackForce.x = _attackForce.x * -1;
        }
        attackForce = _attackForce;
    }

    public void ColliderLifeCycleOn(float _time)
    {
        StartCoroutine(ColliderLifeCycle(_time));
    }

    private void ColliderOn()
    {
        if (m_collider.enabled == false)
        {
            m_collider.enabled = true;
            m_spineAnimCollider.ColliderDraw();

        }
    }

    private void ColliderOff()
    {
        if (m_collider.enabled == true)
        {
            m_collider.enabled = false;
        }
    }

    IEnumerator ColliderLifeCycle(float _time)
    {
        ColliderOn();
        yield return new WaitForSeconds(_time);
        ColliderOff();
    }
}
