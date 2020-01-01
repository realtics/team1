using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntion : MonoBehaviour
{
    public string skillName;
    [SerializeField] protected float collisionSize;
    [SerializeField] protected float damageRatio;
    [SerializeField] protected Vector2 damageForce;
    [SerializeField] protected int level = 1;

	protected PlayerState m_playerState;
	protected PlayerAnimFuntion m_animFuntion;
	protected Rigidbody2D m_rigidbody2D;
	protected PlayerCrowdControlManager m_crowdControlManager;
	public virtual void InitSkill(PlayerAnimFuntion _animFuntion, GameObject _playerObject)
	{
        level = PlayerDataManager.Inst.GetSkillLevelInfo(gameObject.name);
        SkillDataManager.SkillInfo skillInfo = SkillDataManager.Inst.GetSkillInfo(gameObject.name);
		skillName = skillInfo.skillName;
		collisionSize = skillInfo.collisionSize;
        damageRatio = skillInfo.damageRatio + (level * 5);
		damageForce = skillInfo.damageForce;

		m_animFuntion = _animFuntion;
		m_playerState = _playerObject.GetComponent<PlayerState>();
		m_rigidbody2D = _playerObject.GetComponent<Rigidbody2D>();
		m_crowdControlManager = _playerObject.GetComponent<PlayerCrowdControlManager>();
	}

	public virtual bool SkillAction()
	{
		return true;
	}

	protected virtual IEnumerator SkillCoroutine(string _skillEffentName)
	{
		yield break;
	}
}