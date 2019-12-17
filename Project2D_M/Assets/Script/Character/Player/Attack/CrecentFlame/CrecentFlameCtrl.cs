using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecentFlameCtrl : MonoBehaviour
{
	private SpriteRenderer[] m_spriteRenderer = null;
	public float temp = 1;

	private void Awake()
	{
		InitCrecentFlame();
	}
	public void InitCrecentFlame()
	{
		m_spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		for(int i = 0; i < m_spriteRenderer.Length; ++i)
			m_spriteRenderer[i].material.SetFloat("_Progress",temp);
	}
}
