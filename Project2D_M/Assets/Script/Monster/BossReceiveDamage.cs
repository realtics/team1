using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReceiveDamage : ReceiveDamage
{

	public override void Receive(int _damage, bool _bCritical)
	{
		if (!bScriptEnable)
			return;

		int damage = m_characterInfo.DamageCalculation(_damage);
		DamageFontManager.Inst.ShowDamage(damage, DamageShowPosition(), _bCritical);
		m_characterInfo.HpDamage(damage);
		((MonsterInfo)m_characterInfo).ArmorDamage(damage);

		if (m_characterInfo.IsCharacterDie())
		{
			m_animator.SetTrigger("tDie");
			this.bScriptEnable = false;
		}

		if (m_randAudioFuntion != null)
			m_randAudioFuntion.VoiceRandPlay("Hit");
	}
	public override void AddDamageForce(Vector2 _force)
    {
        if (!bScriptEnable)
            return;

        if (!m_crowdControlManager.superArmor && Vector2.zero != _force)
        {
            m_rigidbody2D.velocity = Vector2.zero;
            //m_animator.SetTrigger("tHit");

            //if (m_crowdControlManager != null)
                //m_crowdControlManager.Stiffen(0.5f);

            //m_rigidbody2D.velocity = _force;
        }
    }
}
