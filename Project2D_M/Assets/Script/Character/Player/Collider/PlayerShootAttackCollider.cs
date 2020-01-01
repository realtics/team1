using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerShootAttackCollider : ShootAttackCollider
{
	private PlayerInfo m_playerInfo = null;
	private CinemachineImpulseSource m_cinemachineImpulse = null;
	private PlayerCombo m_playerCombo = null;
	private PlayerState m_playerState = null;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (longDamage)
			return;

		if (collision.CompareTag(m_sTagName))
		{
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();

			if (!receiveDamage.bScriptEnable)
				return;

			if (attackForce != Vector2.zero)
			{
				receiveDamage.AddDamageForce(attackForce);
				m_cinemachineImpulse.GenerateImpulse();
			}

			if (m_playerInfo.IsCritical())
			{
				m_damage = (int)((m_damage * 1.5f) + 0.5f);
				receiveDamage.Receive(m_damage, true);
			}
			else
			{
				receiveDamage.Receive(m_damage, false);
			}

			if (!m_playerState.IsPlayerGround())
			{
				m_playerInfo.PlusAirAttackCount();
			}

			m_playerCombo.plusCombo();

			if (autoDistroy)
				ObjectPool.Inst.PushToPool(this.gameObject);

		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (!longDamage)
			return;

		currntTime += Time.deltaTime;
		if (currntTime < damageSpaceTime)
			return;
		else currntTime = 0;

		if (collision.CompareTag(m_sTagName))
		{
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();

			if (!receiveDamage.bScriptEnable)
				return;

			if (attackForce != Vector2.zero)
			{
				receiveDamage.AddDamageForce(attackForce);
				m_cinemachineImpulse.GenerateImpulse();
			}

			if (m_playerInfo.IsCritical())
			{
				m_damage = (int)((m_damage * 1.5f) + 0.5f);
				receiveDamage.Receive(m_damage, true);
			}
			else
			{
				receiveDamage.Receive(m_damage, false);
			}

			if (!m_playerState.IsPlayerGround())
			{
				m_playerInfo.PlusAirAttackCount();
			}

			m_playerCombo.plusCombo();

		}
	}

	public override void SetDamageColliderInfo(int _damage, string _tagName, Vector2 _attackForce, GameObject _order, bool _autoDistroy = true)
	{
		m_sTagName = _tagName;
		orderObject = _order;
		m_damage = _damage;
		attackForce = _attackForce;
		autoDistroy = _autoDistroy;

		m_playerInfo = m_playerInfo ?? _order.transform.root.GetComponent<PlayerInfo>();
		m_cinemachineImpulse = m_cinemachineImpulse ?? this.GetComponent<CinemachineImpulseSource>();
		m_playerCombo = m_playerCombo ?? _order.transform.root.GetComponent<PlayerCombo>();
		m_playerState = m_playerState ?? _order.transform.root.GetComponent<PlayerState>();
	}
}