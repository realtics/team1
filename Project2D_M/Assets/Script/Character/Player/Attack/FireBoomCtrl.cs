using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoomCtrl : MonoBehaviour
{
	private Animator m_animator = null;

	public void InitFireBoom(float _speed)
	{
		m_animator = this.GetComponent<Animator>();
		m_animator.speed = _speed;
		this.gameObject.SetActive(true);
		StartCoroutine(nameof(FireCoroutine));
	}

	private IEnumerator FireCoroutine()
	{
		while (true)
		{
			if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f)
				break;
			yield return null;
		}

		ObjectPool.Inst.PushToPool(this.gameObject);
	}
}
