using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoomCtrl : MonoBehaviour
{
	private Animator m_animator = null;
	private AudioFunction m_audioFuntion = null;

	public void InitFireBoom(float _speed)
	{
		m_animator = m_animator ?? this.GetComponent<Animator>();
		m_audioFuntion = m_audioFuntion ?? this.GetComponent<AudioFunction>();
		m_animator.speed = _speed;
	}

    public void ShootBoom()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(nameof(FireCoroutine));
    }

    private IEnumerator FireCoroutine()
	{
		m_audioFuntion.AudioPlay(this.gameObject.name,false);
		while (true)
		{
			if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f)
				break;
			yield return null;
		}

		ObjectPool.Inst.PushToPool(this.gameObject);
	}
}
