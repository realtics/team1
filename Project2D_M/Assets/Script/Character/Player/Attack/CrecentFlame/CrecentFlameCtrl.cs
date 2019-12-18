using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecentFlameCtrl : MonoBehaviour
{
	private SpriteRenderer[] m_spriteRenderer = null;
	private BoxCollider2D collider = null;
	private Animator[] animator = null;
	public void InitCrecentFlame()
	{
		m_spriteRenderer = m_spriteRenderer ?? GetComponentsInChildren<SpriteRenderer>();
		collider = collider ?? GetComponent<BoxCollider2D>();
		animator = animator ?? GetComponentsInChildren<Animator>();

		SetSpriteProgress(0.0f);
		SetSpriteAlpha(1.0f);
		this.gameObject.SetActive(true);
	}

	public void SkillAction()
	{
		StartCoroutine(nameof(CrecentFlameCoroutine));
	}

	private void SetSpriteProgress(float _value)
	{
		for(int i = 0; i < m_spriteRenderer.Length; ++i)
			m_spriteRenderer[i].material.SetFloat("_Progress", _value);
	}

	private void SetSpriteAlpha(float _value)
	{
		for (int i = 0; i < m_spriteRenderer.Length; ++i)
		{
			Color color = m_spriteRenderer[i].color;
			color.a = _value;
			m_spriteRenderer[i].material.SetColor("_Color", color);
		}
	}

	private IEnumerator CrecentFlameCoroutine()
	{
		float value = 0.0f;
		collider.enabled = true;

		for (int i = 0; i < animator.Length; ++i)
		{
			animator[i].SetTrigger("tShoot");
		}

		while (true)
		{
			value += 0.06f;
			if (value >= 1f)
				value = 1f;

			SetSpriteProgress(value);
			yield return new WaitForSeconds(0.01f);

			if (value >= 1f)
				break;
		}

		collider.enabled = false;

		while (true)
		{
			value -= 0.01f;
			if (value <= 0f)
				value = 0f;

			SetSpriteAlpha(value);
			yield return new WaitForSeconds(0.001f);

			if (value <= 0f)
				break;
		}

		SetSpriteProgress(0.0f);
		SetSpriteAlpha(1.0f);

		ObjectPool.Inst.PushToPool(this.gameObject);
	}
}
