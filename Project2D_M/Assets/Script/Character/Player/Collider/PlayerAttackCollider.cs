using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : AttackCollider
{
    private PlayerInfo m_playerInfo = null;
    private void Awake()
    {
        m_playerInfo = this.transform.root.GetComponent<PlayerInfo>();
        m_collider = this.GetComponent<PolygonCollider2D>();

        m_spineAnimCollider = this.GetComponent<SpineAnimCollider>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_sTagName)
        {
            ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();
            if (receiveDamage.enabled != false)
            {
                receiveDamage.AddDamageForce(attackForce);

                if (m_playerInfo.IsCritical())
                {
                    m_damage = (int)((m_damage * 1.5f) + 0.5f);
                    receiveDamage.Receive(m_damage, true);
                }
                else
                {
                    receiveDamage.Receive(m_damage, false);
                }
            }
        }
    }

    public override void ColliderLifeCycleOn(float _time)
    {
        StartCoroutine(ColliderLifeCycle(_time));
    }


    public override void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
    {
        m_sTagName = _tagName;
        int randNum = Random.Range(1, 101);

        m_damage = (int)(_damage * m_playerInfo.attack + 0.5f);

        if ((this.transform.root.transform.localScale.x > 0 && _attackForce.x < 0) ||
            (this.transform.root.transform.localScale.x < 0 && _attackForce.x > 0))
        {
            _attackForce.x = _attackForce.x * -1;
        }
        attackForce = _attackForce;
    }
}
