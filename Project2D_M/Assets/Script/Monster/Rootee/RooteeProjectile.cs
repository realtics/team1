using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooteeProjectile : MonoBehaviour
{
	private Vector3 m_position;
	private Rigidbody2D m_rigidBody;
	private Vector2 m_fireForce;

	private void Awake()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_fireForce = new Vector2(5.5f, 3.0f);
	}

	private void OnEnable()
	{
		m_rigidBody.constraints = RigidbodyConstraints2D.None;
		this.GetComponent<CircleCollider2D>().enabled = true;

	}

	public void Fire(Vector3 _startPos, bool _left)
	{
		if (_left)
		{
			this.transform.position = new Vector3(_startPos.x - 0.3f, _startPos.y + 1.7f, _startPos.z);

			m_fireForce.x *= -1;
			m_rigidBody.velocity = m_fireForce;
			m_fireForce.x *= -1;
		}
		else
		{
			this.transform.position = new Vector3(_startPos.x + 0.3f, _startPos.y + 1.7f, _startPos.z);

			m_rigidBody.velocity = m_fireForce;
		}
	}

	public IEnumerator InGround()
	{
		float fTime = 0.3f;
		m_rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

		while (true)
		{
			fTime -= Time.deltaTime;
			if (fTime < 0.2f)
			{
				this.GetComponent<CircleCollider2D>().enabled = false;
			}
			if (fTime < 0)
			{
				this.gameObject.SetActive(false);
				ObjectPool.Inst.PushToPool(this.gameObject);

				break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
