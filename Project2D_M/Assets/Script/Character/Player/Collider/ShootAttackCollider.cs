using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttackCollider : MonoBehaviour
{
	protected GameObject orderObject;
	protected string m_sTagName = null;
	[SerializeField] protected int m_damage;
	[SerializeField] protected Vector2 attackForce;
	protected bool longDamage = false;
	protected float damageSpaceTime = 0.2f;
	protected float currntTime = 0;
	protected bool autoDistroy = true;

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (longDamage)
			return;

		if (collision.CompareTag(m_sTagName))
		{
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();

			if (receiveDamage.bScriptEnable != false)
			{
				if (attackForce != Vector2.zero)
				{
					receiveDamage.AddDamageForce(attackForce);
				}
				receiveDamage.Receive(m_damage, false);
			}

			if(autoDistroy)
				ObjectPool.Inst.PushToPool(this.gameObject);
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
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

			if (receiveDamage.bScriptEnable != false)
			{
				if (attackForce != Vector2.zero)
				{
					receiveDamage.AddDamageForce(attackForce);
				}
				receiveDamage.Receive(m_damage, false);
			}
		}
	}

	public virtual void SetDamageColliderInfo(int _damage, string _tagName, Vector2 _attackForce, GameObject _order, bool _autoDistroy = true)
	{
		m_sTagName = _tagName;
		orderObject = _order;
		m_damage = _damage;
		attackForce = _attackForce;
		autoDistroy = _autoDistroy;
	}

	public virtual void SetDamageOption(bool _longDamage, float _damageSpaceTime)
	{
		longDamage = _longDamage;
		damageSpaceTime = _damageSpaceTime;
	}
}
