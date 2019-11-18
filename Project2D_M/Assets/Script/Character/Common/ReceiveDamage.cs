using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    private CharacterInfo m_characterInfo = null;
    private Animator m_animator = null;
    private Rigidbody2D m_rigidbody2D = null;
    private CrowdControlManager m_crowdControlManager = null;
    private void Awake()
    {
        m_animator = this.GetComponent<Animator>();

        m_characterInfo = this.GetComponent<CharacterInfo>();
        if(m_characterInfo == null)
            m_characterInfo = this.transform.root.transform.GetComponent<CharacterInfo>();

        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        if(m_rigidbody2D == null)
            m_rigidbody2D = this.transform.root.transform.GetComponent<Rigidbody2D>();

        m_crowdControlManager = this.GetComponent<CrowdControlManager>();
        if(m_crowdControlManager == null)
            m_crowdControlManager = this.transform.root.transform.GetComponent<CrowdControlManager>();
    }
    public void Receive(int _damage)
    {
        int damage = m_characterInfo.DamageCalculation(_damage);
        m_characterInfo.HpDamage(damage);
        if (m_characterInfo.IsCharacterDie())
            m_animator.SetTrigger("tDie");
        Debug.Log("Damage: " + damage);
    }

    public void AddDamageForce(Vector2 _force)
    {
        m_rigidbody2D.velocity = Vector2.zero;
        if (Vector2.zero != _force)
        {
            m_animator.SetTrigger("tHit");
            m_crowdControlManager.Stiffen(0.5f);
        }
        m_rigidbody2D.AddForce(_force,ForceMode2D.Impulse);
    }
}