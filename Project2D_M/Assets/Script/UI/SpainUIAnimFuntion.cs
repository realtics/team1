using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class SpainUIAnimFuntion : MonoBehaviour
{
	private SkeletonGraphic m_skeletonGraphic = null;
	private AudioFunction m_audioFuntion = null;

	private void Awake()
	{
		m_skeletonGraphic = this.GetComponent<SkeletonGraphic>();
		m_audioFuntion = this.GetComponent<AudioFunction>();
	}

	public void AnimPlay(string _EffectAnimName, bool _roof)
	{
		m_skeletonGraphic.AnimationState.SetAnimation(0, _EffectAnimName, _roof);

		if (m_audioFuntion != null)
		{
			m_audioFuntion.AudioPlay(_EffectAnimName, false);
		}
	}
}
