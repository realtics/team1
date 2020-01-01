using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_20
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 공격담당 함수 (colliderBox연결) AttackCollider 상속
 */
[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerAttackCollider : AttackCollider
{
    private PlayerInfo m_playerInfo = null;
    private CinemachineImpulseSource m_cinemachineImpulse = null;
    private PlayerCombo m_playerCombo = null;
	private PlayerState m_playerState = null;

	protected void Awake()
    {
        m_playerInfo = this.transform.root.GetComponent<PlayerInfo>();
        m_playerCombo = this.transform.root.GetComponent<PlayerCombo>();
		m_playerState = this.transform.root.GetComponent<PlayerState>();
		m_collider = this.GetComponent<PolygonCollider2D>();
        m_cinemachineImpulse = this.GetComponent<CinemachineImpulseSource>();
        m_spineAnimCollider = this.GetComponent<SpineAnimCollider>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag(m_sTagName))
        {
			ReceiveDamage receiveDamage = collision.gameObject.GetComponent<ReceiveDamage>();
            if (receiveDamage.bScriptEnable != false)
            {
                if (attackForce != Vector2.zero)
                {
                    receiveDamage.AddDamageForce(attackForce);
                    m_cinemachineImpulse.GenerateImpulse();
                }

                if (m_playerInfo.IsCritical())
                {
                    m_damage = (int)((m_damage * 1.5f) + 0.5f);
                    receiveDamage.Receive(m_damage, true);
                }
                else
                {
                    receiveDamage.Receive(m_damage, false);
                }

				if(!m_playerState.IsPlayerGround())
				{
					m_playerInfo.PlusAirAttackCount();
				}
                m_playerCombo.plusCombo();
            }

        }
    }

    public override void ColliderLifeCycleOn(float _time)
    {
        StartCoroutine(ColliderLifeCycle(_time));
    }


    public override void SetDamageColliderInfo(float _damage, string _tagName, Vector2 _attackForce)
	{
		m_sTagName = _tagName;
        int randNum = Random.Range(1, 101);

        m_damage = (int)(_damage * m_playerInfo.attack + 0.5f);

        if ((this.transform.root.transform.localScale.x > 0 && _attackForce.x < 0) ||
            (this.transform.root.transform.localScale.x < 0 && _attackForce.x > 0))
        {
            _attackForce.x = _attackForce.x * -1;
        }
        attackForce = _attackForce;
	}
}
