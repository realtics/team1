using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecentFlameCtrl : MonoBehaviour
{
	private SpriteRenderer[] m_spriteRenderer = null;
	private BoxCollider2D colliderBox = null;
	private Animator[] animator = null;
	private AudioFunction m_audioFunction;
	public void InitCrecentFlame()
	{
		m_spriteRenderer = m_spriteRenderer ?? GetComponentsInChildren<SpriteRenderer>();
        colliderBox = colliderBox ?? GetComponent<BoxCollider2D>();
		animator = animator ?? GetComponentsInChildren<Animator>();

		SetSpriteProgress(0.0f);
		SetSpriteAlpha(1.0f);
		this.gameObject.SetActive(true);

		m_audioFunction = m_audioFunction ?? GetComponent<AudioFunction>();
		m_audioFunction.AudioPlay("CrecentFlame_Init",false);
	}

	public void SkillAction()
	{
		m_audioFunction.AudioPlay("CrecentFlame_Shot", false);
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

		for (int i = 0; i < animator.Length; ++i)
		{
			animator[i].SetTrigger("tShoot");
		}

        colliderBox.enabled = true;

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

        colliderBox.enabled = false;

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

        this.transform.localScale = new Vector3(1, 1, 1);

		ObjectPool.Inst.PushToPool(this.gameObject);
	}
}
