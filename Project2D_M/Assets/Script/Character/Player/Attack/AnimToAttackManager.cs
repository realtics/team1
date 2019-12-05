using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_29
 * 팀              : 1팀
 * 스크립트 용도   : 애니메이션이벤트, 관련 공격 콜라이더 연결
 */
public class AnimToAttackManager : MonoBehaviour
{
    public SkillManager SkillManager;
	public AttackCollider normalAttackCollider;
	private AnimFuntion m_animFuntion;

	private void Awake()
	{
		m_animFuntion = GetComponent<AnimFuntion>();
	}
	//애니메이션 이벤트 사용
	public void ColliderLifeCycleOn(float _time)
    {
		normalAttackCollider.ColliderLifeCycleOn(_time);
    }

	//애니메이션 이벤트 사용
	public void ColliderSkillLifeCycleOn(float _time)
	{
		SkillManager.ColliderLifeCycleOn(_time, m_animFuntion.GetCurrntAnimClipName());
	}

	//애니메이션 이벤트 사용
	public void ColliderSkillLifeCycleOnDraw(float _time)
	{
		SkillManager.ColliderLifeCycleOnDraw(_time, m_animFuntion.GetCurrntAnimClipName());
	}


	//애니메이션 이벤트 사용
	public void PlayAnim(string _animname)
    {
		SkillManager.PlayAnim(_animname);
    }
}
