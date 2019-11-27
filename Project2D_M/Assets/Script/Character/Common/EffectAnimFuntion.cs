using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class EffectAnimFuntion : MonoBehaviour
{
    private SkeletonAnimation m_skeletonAnimation = null;
    private MeshRenderer m_meshRenderer = null;

    private void Awake()
    {
        m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        m_meshRenderer = this.GetComponent<MeshRenderer>();
    }

    public void EffectOff()
    {
        m_meshRenderer.enabled = false;
        m_skeletonAnimation.enabled = false;
    }

    public void EffectOn()
    {
        m_meshRenderer.enabled = true;
        m_skeletonAnimation.enabled = true;
    }

    public void EffectPlay(string _EffectAnimName, bool _roof)
    {
        m_skeletonAnimation.AnimationState.SetAnimation(0, _EffectAnimName, _roof);
    }
}
