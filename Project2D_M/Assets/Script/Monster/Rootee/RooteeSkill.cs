using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooteeSkill : MonoBehaviour
{
	[SerializeField]
	protected GameObject shotObject = null;
	private GameObject m_projectileObject = null;
	private RooteeProjectile m_projectileComponent = null;
	[SerializeField]
	protected GameObject m_monster = null;

	public void InitSkill()
	{
		ObjectPool.Inst.Initialize(shotObject, 1);
	}

	public void SkillAction(bool _left, int _damage)
	{
		int tempInt = 1 ;
		if (_left)
			tempInt = -1;

		m_projectileObject = ObjectPool.Inst.PopFromPool("RooteeProjectile");
		m_projectileObject.GetComponent<MonsterShootAttackCollider>().SetDamageColliderInfo(_damage, "Player", new Vector2(3.0f * tempInt, 1.0f), m_monster, true);
		m_projectileComponent = m_projectileObject.GetComponent<RooteeProjectile>();
		m_projectileObject.SetActive(true);
		m_projectileComponent.Fire(this.transform.position, _left);
	}

}
