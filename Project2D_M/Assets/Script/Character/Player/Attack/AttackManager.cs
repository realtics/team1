using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AttackManager : MonoBehaviour
{
    private AttackCollider[] m_attackColliders;
    private SkeletonAnimation[] m_skeletonAnimations;

    private void Awake()
    {
        m_attackColliders = this.GetComponentsInChildren<AttackCollider>();
        m_skeletonAnimations = this.GetComponentsInChildren<SkeletonAnimation>();
    }

    public void ColliderLifeCycleOn(float _time)
    {
        foreach (AttackCollider attackCollider in m_attackColliders)
        {
            attackCollider.ColliderLifeCycleOn(_time);
        }
    }

    public void SetDamageColliderInfo(float _damageRatio, string _tagName, Vector2 _attackForce)
    {
        foreach (AttackCollider attackCollider in m_attackColliders)
        {
            attackCollider.SetDamageColliderInfo(_damageRatio, _tagName, _attackForce);
        }
    }

    public void PlayAnim(string _animname, string _attackObjectName = null, bool _roof = false)
    {
        foreach (SkeletonAnimation skeletonAnimation in m_skeletonAnimations)
        {
            if(_attackObjectName != null && skeletonAnimation.transform.name == _attackObjectName)
                skeletonAnimation.AnimationState.SetAnimation(0, _animname, _roof);
            else if (_attackObjectName == null)
                skeletonAnimation.AnimationState.SetAnimation(0, _animname, _roof);
        }
    }
}
