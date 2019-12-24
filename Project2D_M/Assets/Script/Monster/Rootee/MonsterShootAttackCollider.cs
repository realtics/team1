using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MonsterShootAttackCollider : ShootAttackCollider
{
	private MonsterInfo m_monsterInfo = null;
	private CinemachineImpulseSource m_cinemachineImpulse = null;
	private bool m_hited = false;
	private RooteeProjectile m_projectile;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		//if(longDamage)
		if (m_hited)
			return;

		if (collision.tag == m_sTagName)
		{
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();

			if (!receiveDamage.bScriptEnable)
				return;

			if (attackForce != Vector2.zero)
			{
				receiveDamage.AddDamageForce(attackForce);
				m_cinemachineImpulse.GenerateImpulse();
			}

			if (m_monsterInfo.IsCritical())
			{
				m_damage = (int)((m_damage * 1.5f) + 0.5f);
				receiveDamage.Receive(m_damage, true);
			}
			else
			{
				receiveDamage.Receive(m_damage, false);
			}

			if (autoDistroy)
				ObjectPool.Inst.PushToPool(this.gameObject);

		}
		if(collision.tag == "Floor")
		{
			StartCoroutine(m_projectile.InGround());
		}
	}

	public override void SetDamageColliderInfo(int _damage, string _tagName, Vector2 _attackForce, GameObject _order, bool _autoDistroy = true)
	{
		m_sTagName = _tagName;
		orderObject = _order;
		m_damage = _damage;
		attackForce = _attackForce;
		autoDistroy = _autoDistroy;

		m_monsterInfo = m_monsterInfo ?? _order.transform.root.GetComponent<MonsterInfo>();
		m_cinemachineImpulse = m_cinemachineImpulse ?? this.GetComponent<CinemachineImpulseSource>();
		m_projectile = this.GetComponent<RooteeProjectile>();
	}



}
