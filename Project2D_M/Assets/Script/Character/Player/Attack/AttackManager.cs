using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AttackCollider[] m_attackColliders;

    private void Awake()
    {
        m_attackColliders = this.GetComponentsInChildren<AttackCollider>();
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
}
