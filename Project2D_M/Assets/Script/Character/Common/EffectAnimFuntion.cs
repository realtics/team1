using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_20
 * 팀              : 1팀
 * 스크립트 용도   : 이펙트로 사용될 spine 애니메이션 관련 함수
 */
[RequireComponent(typeof(SkeletonAnimation))]
public class EffectAnimFuntion : MonoBehaviour
{
    private SkeletonAnimation m_skeletonAnimation = null;
    private MeshRenderer m_meshRenderer = null;
	private AudioFunction m_audioFuntion = null;
    private void Awake()
    {
        m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_audioFuntion = this.GetComponent<AudioFunction>();
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

		if(m_audioFuntion)
		{
			m_audioFuntion.AudioPlay(_EffectAnimName, _roof);
		}
	}
}