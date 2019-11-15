using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpineAnimCollider))]
public class ReceiveDamage : MonoBehaviour
{
    private PolygonCollider2D m_collider2D = null;
    private CharacterInfo m_characterInfo = null;
    private Animator m_animator = null;
    private void Awake()
    {
        m_animator = this.GetComponent<Animator>();
        m_characterInfo = this.GetComponent<CharacterInfo>();
    }
    public void Receive(int _damage)
    {
        int damage;

        damage = m_characterInfo.DamageCalculation(_damage);
        m_characterInfo.HpDamage(damage);
        m_animator.SetTrigger("tHit");
        Debug.Log("Damage: " + damage);
    }
}