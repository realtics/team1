using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_20
 * 팀              : 1팀
 * 스크립트 용도   : 공격(데미지와 공격의 힘)을 받는 클래스
 */
public class ReceiveDamage : ScriptEnable
{
    protected CharacterInfo m_characterInfo = null;
    protected Animator m_animator = null;
    protected Rigidbody2D m_rigidbody2D = null;
    protected CrowdControlManager m_crowdControlManager = null;
    protected PlayerRandAudioFuntion m_randAudioFuntion = null;
    private void Awake()
    {
        m_animator = this.GetComponentInChildren<Animator>();
		m_randAudioFuntion = this.GetComponent<PlayerRandAudioFuntion>();
		m_characterInfo = this.GetComponent<CharacterInfo>();
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_crowdControlManager = this.GetComponent<CrowdControlManager>();
    }

    public virtual void Receive(int _damage, bool _bCritical)
    {
        if (!bScriptEnable)
            return;

        int damage = m_characterInfo.DamageCalculation(_damage);
        DamageFontManager.Inst.ShowDamage(damage, DamageShowPosition(), _bCritical);
        m_characterInfo.HpDamage(damage);

        if (m_characterInfo.IsCharacterDie())
        {
            m_animator.SetTrigger("tDie");
            this.bScriptEnable = false;
        }

		if(m_randAudioFuntion != null)
			m_randAudioFuntion.VoiceRandPlay("Hit");
	}

    public virtual void AddDamageForce(Vector2 _force)
    {
        if (!bScriptEnable)
            return;

		if (!m_crowdControlManager.superArmor && Vector2.zero != _force)
        {
            m_rigidbody2D.velocity = Vector2.zero;
            m_animator.SetTrigger("tHit");

            if (m_crowdControlManager != null)
                m_crowdControlManager.Stiffen(0.5f);

			m_rigidbody2D.velocity = _force;
		}
	}

    protected Vector3 DamageShowPosition()
    {
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        return pos;
    }
}