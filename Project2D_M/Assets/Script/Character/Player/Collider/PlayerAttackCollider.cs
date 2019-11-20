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

    public override void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
    {
        m_sTagName = _tagName;
        int randNum = Random.Range(1, 101);

        if(randNum <= m_playerInfo.critical)
            m_damage = (int)((m_playerInfo.attack * _damage * 1.5f) + 0.5f);
        else m_damage = (int)((m_playerInfo.attack * _damage) + 0.5f);

        if ((this.transform.root.transform.localScale.x > 0 && _attackForce.x < 0) ||
            (this.transform.root.transform.localScale.x < 0 && _attackForce.x > 0))
        {
            _attackForce.x = _attackForce.x * -1;
        }
        attackForce = _attackForce;
    }
}
