using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EffectSpineAnimFunction : MonoBehaviour
{
	SkeletonAnimation skeletonAnimation;
	Spine.ExposedList<Spine.Animation> spines;

	private bool FindAnimName(string _animName)
	{
		spines = spines ?? skeletonAnimation.SkeletonDataAsset.GetAnimationStateData().skeletonData.animations;

		foreach (Spine.Animation animation in spines)
		{
			if (_animName == animation.name)
				return true;
		}

		return false;
	}

	public void PlayAnim(string _animName)
	{
		skeletonAnimation = skeletonAnimation ?? GetComponent<SkeletonAnimation>();
		
		if (FindAnimName(_animName))
			skeletonAnimation.AnimationState.SetAnimation(0, _animName, false);
	}
}
