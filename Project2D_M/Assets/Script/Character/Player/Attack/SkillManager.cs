﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_29
 * 팀              : 1팀
 * 스크립트 용도   : 자식 오브젝트의 공격관련 함수 매니저
 */
public class SkillManager : MonoBehaviour
{
    private AttackCollider[] m_attackColliders;
    private SkeletonAnimation[] m_skeletonAnimations;
	private SkillFuntion[] m_skillFuntions;
	[SerializeField] private PlayerAnimFuntion m_animFuntion = null;
	[SerializeField] private PlayerState m_playerState = null;

	private void Awake()
    {
        m_attackColliders = this.GetComponentsInChildren<AttackCollider>();
        m_skeletonAnimations = this.GetComponentsInChildren<SkeletonAnimation>();
		m_skillFuntions = this.GetComponentsInChildren<SkillFuntion>();

		for (int i = 0; i < m_skillFuntions.Length; ++i)
		{
			m_skillFuntions[i].InitSkill(m_animFuntion, m_playerState);
		}
	}

	public void ColliderLifeCycleOn(float _time, string _animName)
    {
        for(int i = 0; i < m_attackColliders.Length; ++i)
        {
			if (m_animFuntion.IsTag(m_skillFuntions[i].sSkillName))
				m_attackColliders[i].ColliderLifeCycleOn(_time);
        }
    }

	public void ColliderLifeCycleOnDraw(float _time, string _animName)
	{
		for (int i = 0; i < m_attackColliders.Length; ++i)
		{
			if (m_animFuntion.IsTag(m_skillFuntions[i].sSkillName))
				m_attackColliders[i].ColliderLifeCycleOnDraw(_time);
		}
	}

	public void SetDamageColliderInfo(float _damageRatio, string _tagName, Vector2 _attackForce)
    {
		for (int i = 0; i < m_attackColliders.Length; ++i)
		{
			m_attackColliders[i].SetDamageColliderInfo(_damageRatio, _tagName, _attackForce);
        }
    }

    public void PlayAnim(string _animname, string _attackObjectName = null, bool _roof = false)
    {
		for (int i = 0; i < m_skeletonAnimations.Length; ++i)
		{
			if (_attackObjectName != null && m_skeletonAnimations[i].transform.name == _attackObjectName)
				m_skeletonAnimations[i].AnimationState.SetAnimation(0, _animname, _roof);
            else if (_attackObjectName == null)
				m_skeletonAnimations[i].AnimationState.SetAnimation(0, _animname, _roof);
        }
    }

	public void SkillAction(string _skillName)
	{
		for (int i = 0; i < m_skillFuntions.Length; ++i)
		{
			if(m_skillFuntions[i].sSkillName.Equals(_skillName))
				m_skillFuntions[i].SkillAction();
		}
	}
}
