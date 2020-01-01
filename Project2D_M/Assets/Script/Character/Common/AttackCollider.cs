using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 공격판정 담당하는 콜라이더 관련 스크립트
 */
public class AttackCollider : MonoBehaviour
{
    protected CharacterInfo m_characterInfo = null;
    protected string m_sTagName = null;
    protected PolygonCollider2D m_collider = null;
    protected SpineAnimCollider m_spineAnimCollider = null;
    [SerializeField] protected int m_damage;
    [SerializeField] protected Vector2 attackForce;
	public float iCollisionSize = 1.0f;

	private void Awake()
    {
        m_characterInfo = this.transform.root.GetComponent<CharacterInfo>();
        m_collider = this.GetComponent<PolygonCollider2D>();
        m_spineAnimCollider = this.GetComponent<SpineAnimCollider>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag(m_sTagName))
		{
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();
            if (receiveDamage.enabled != false)
            {
				if (attackForce != Vector2.zero)
				{
					receiveDamage.AddDamageForce(attackForce);
				}
				receiveDamage.Receive(m_damage, false);
			}
		}
    }

    public virtual void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
    {
        m_sTagName = _tagName;
        m_damage = (int)((m_characterInfo.attack * _damage) + 0.5f);

        if ((this.transform.root.transform.localScale.x > 0 && _attackForce.x < 0) ||
            (this.transform.root.transform.localScale.x < 0 && _attackForce.x > 0))
        {
            _attackForce.x = _attackForce.x * -1;
        }
        attackForce = _attackForce;
    }

    public virtual void ColliderLifeCycleOn(float _time)
    {
        StartCoroutine(ColliderLifeCycle(_time));
    }

	public virtual void ColliderLifeCycleOnDraw(float _time)
	{
		StartCoroutine(ColliderLifeCycleDraw(_time));
	}

	protected void ColliderOn()
    {
		if (m_collider.enabled == false)
        {
            m_collider.enabled = true;
            m_spineAnimCollider.ColliderDraw(iCollisionSize);
		}
	}

    protected void ColliderOff()
    {
        if (m_collider.enabled == true)
        {
			m_spineAnimCollider.DeleteCollider();
			m_collider.enabled = false;
        }
    }

    protected IEnumerator ColliderLifeCycle(float _time)
    {
        ColliderOn();
		yield return new WaitForSeconds(_time);
		m_spineAnimCollider.DeleteCollider();
		ColliderOff();
    }

	protected IEnumerator ColliderLifeCycleDraw(float _time)
	{
		ColliderOn();
		m_spineAnimCollider.ColliderDraw(iCollisionSize);
		float time = 0;

		while (time <= _time)
		{
			m_spineAnimCollider.ColliderDraw(iCollisionSize);
			yield return new WaitForSeconds(0.01f);
			time += Time.fixedDeltaTime;
		}
		m_spineAnimCollider.DeleteCollider();
		ColliderOff();
	}
}
