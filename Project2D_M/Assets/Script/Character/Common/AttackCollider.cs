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
    protected CharacterInfo m_characterInfo = null;
    protected string m_sTagName = null;
    protected PolygonCollider2D m_collider = null;
    protected SpineAnimCollider m_spineAnimCollider = null;
    [SerializeField] protected int m_damage;
    [SerializeField] protected Vector2 attackForce;

    private void Awake()
    {
        m_characterInfo = this.transform.root.GetComponent<CharacterInfo>();
        m_collider = this.GetComponent<PolygonCollider2D>();

        m_spineAnimCollider = this.GetComponent<SpineAnimCollider>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_sTagName)
        {
            ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();
            if (receiveDamage.enabled != false)
            {
                receiveDamage.AddDamageForce(attackForce);
                receiveDamage.Receive(m_damage);
            }
        }
    }

    public virtual void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
    {
        m_sTagName = _tagName;
        m_damage = (int)((m_characterInfo.attack * _damage) + 0.5f);

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

    protected void ColliderOn()
    {
        if (m_collider.enabled == false)
        {
            m_collider.enabled = true;
            m_spineAnimCollider.ColliderDraw();

        }
    }

    protected void ColliderOff()
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
