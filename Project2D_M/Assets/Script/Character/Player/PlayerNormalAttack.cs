using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_17
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 평타 공격 관련 스크립트
 */
public class PlayerNormalAttack : MonoBehaviour
{
	private struct AttackInfo
	{
		public float damageRatio;
		public Vector2 damageForce;
		public AttackInfo(float _damageRatio, Vector2 _damageForce = default(Vector2))
		{
			damageRatio = _damageRatio;

			if (_damageForce == Vector2.zero)
				damageForce = Vector2.zero;
			else damageForce = _damageForce;
		}
	}

	public EffectAnimFuntion m_effectAnimFuntion;
	public GameObject NormalAttackEffect;
	public float fAirAttackDistansce = 2;
	private AttackCollider m_attackCollider;
	private Dictionary<string, AttackInfo> m_NormalAttackDic;
	private Rigidbody2D m_rigidbody2D = null;
	private PlayerAnimFuntion m_animFuntion = null;
	private CharacterMove m_characterMove = null;
	private PlayerState m_playerState = null;
	private PlayerInput m_playerInput = null;
	private PlayerCrowdControlManager m_playerCrowdControlManager=null;
	private PlayerRandAudioFuntion m_randAudioFuntion = null;
	private PlayerAudioFunction m_audioFuntion = null;

	[SerializeField]private bool m_bAttacking;

	private void Awake()
	{
		m_rigidbody2D = this.GetComponent<Rigidbody2D>();
		m_animFuntion = this.transform.Find("PlayerSpineSprite").GetComponent<PlayerAnimFuntion>();
		m_attackCollider = NormalAttackEffect.GetComponent<AttackCollider>();
		m_characterMove = this.GetComponent<CharacterMove>();
		m_playerState = this.GetComponent<PlayerState>();
		m_playerInput = this.GetComponent<PlayerInput>();
		m_playerCrowdControlManager = this.GetComponent<PlayerCrowdControlManager>();
		m_randAudioFuntion = this.GetComponent<PlayerRandAudioFuntion>();
		m_audioFuntion = this.GetComponent<PlayerAudioFunction>();

		m_bAttacking = false;

		m_NormalAttackDic = new Dictionary<string, AttackInfo>();

		m_NormalAttackDic.Add("attack_1", new AttackInfo(1.0f, new Vector2(2.0f, 3f)));
		m_NormalAttackDic.Add("attack_2", new AttackInfo(1.0f, new Vector2(2.0f, 3f)));
		m_NormalAttackDic.Add("attack_3_1", new AttackInfo(0.5f, new Vector2(2.0f, 5.0f)));
		m_NormalAttackDic.Add("attack_3_2", new AttackInfo(2.0f, new Vector2(2.0f, -10.0f)));
		m_NormalAttackDic.Add("attack_4", new AttackInfo(3.0f, new Vector2(2.0f, 10.0f)));
		m_NormalAttackDic.Add("attack_5", new AttackInfo(4.0f, new Vector2(10.0f, 10.0f)));

		m_NormalAttackDic.Add("air_attack_1", new AttackInfo(1.0f, new Vector2(1f, 12f)));
		m_NormalAttackDic.Add("air_attack_2", new AttackInfo(1.0f, new Vector2(1f, 12f)));
		m_NormalAttackDic.Add("air_attack_3", new AttackInfo(1.0f, new Vector2(1f, 12f)));
		m_NormalAttackDic.Add("air_attack_4", new AttackInfo(1.0f, new Vector2(5f, 12f)));

		m_NormalAttackDic.Add("attack_upper", new AttackInfo(3.0f, new Vector2(1f, 24.0f)));
		m_NormalAttackDic.Add("attack_downsmash", new AttackInfo(4.0f, new Vector2(2f, -25.0f)));
	}

	
	public void NormalAttack()
	{
		if (!m_playerState.IsPlayerGround() && Physics2D.Raycast(this.transform.position, -this.transform.up, fAirAttackDistansce, 1 << LayerMask.NameToLayer("Floor")))
		{
			return;
		}

		m_animFuntion.SetTrigger(m_animFuntion.hashTNormalAttack);

		if (!m_playerState.IsPlayerGround() && !m_playerState.IsPlayerSPAttack()) 
		{
			RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(this.transform.position + new Vector3(0, this.transform.localScale.y * 2, 0), GetComponent<BoxCollider2D>().size, 0.0f,
				this.transform.localScale.x * this.transform.right,
				fAirAttackDistansce, 1 << LayerMask.NameToLayer("Monster"));

			if(raycastHit2D.Length != 0)
				m_playerCrowdControlManager.OnAirStop();
			else m_playerCrowdControlManager.OffAirStop();
		}

		if (Input.GetAxisRaw("Vertical") > 0 || m_playerInput.joystickState == PlayerInput.JOYSTICK_STATE.JOYSTICK_UP)
		{
			m_animFuntion.SetTrigger(m_animFuntion.hashTUpper);
		}
		else if (Input.GetAxisRaw("Vertical") < 0 || m_playerInput.joystickState == PlayerInput.JOYSTICK_STATE.JOYSTICK_DOWN)
		{
			m_animFuntion.SetTrigger(m_animFuntion.hasTDownsmash);
		}
	}

	public void StartAttack()
	{
		if (!m_bAttacking)
		{
			StartCoroutine(AttackCoroutine());
			m_bAttacking = true;
		}
	}

    private IEnumerator AttackCoroutine()
	{
		if (!m_animFuntion.IsTag("NormalAttack"))
		{
			m_bAttacking = false;
			yield break;
		}

		string m_sAnimName = m_animFuntion.GetCurrntAnimClipName();

		PlayStartSwitch(m_sAnimName);
		PlayAnimEffect(m_sAnimName);

		while (true)
		{
			if (!m_animFuntion.IsName(m_sAnimName))
			{
				PlayEndSwitch(m_sAnimName);
				m_sAnimName = m_animFuntion.GetCurrntAnimClipName();
				PlayStartSwitch(m_sAnimName);
			}

			if (!m_animFuntion.IsTag("NormalAttack"))
			{
				PlayEndSwitch(m_sAnimName);
				break;
			}

			yield return 0;
		}

		m_bAttacking = false;
		m_sAnimName = null;

		CancelInvoke();
		m_playerCrowdControlManager.OffAirStop();

		if (!m_playerState.IsPlayerGround())
		{
			m_playerState.PlayerStateDoubleJump();
		}
	}

	private void PlayAnimEffect(string _animName)
	{
		if (m_animFuntion.IsTag("NormalAttack"))
		{
			switch (_animName)
			{
				case "attack_3":
					m_effectAnimFuntion.EffectPlay("attack_3_1", false);
					break;
				case "attack_upper":
					m_effectAnimFuntion.EffectPlay("upper", false);
					break;
				case "attack_downsmash":
					m_effectAnimFuntion.EffectPlay("downsmash", false);
					break;
				default:
					m_effectAnimFuntion.EffectPlay(_animName, false);
					break;
			}

			m_effectAnimFuntion.EffectOn();
		}
	}

	private void PlayingStartInvokeSwitch(string _animName)
	{
		if (m_animFuntion.IsTag("NormalAttack"))
		{
			switch (_animName)
			{
				case "attack_3":
					Invoke(nameof(Attack3Plus), 0.97f);
					break;
				case "attack_upper":
					m_playerState.PlayerStateSPAttack();
					m_playerCrowdControlManager.SuperArmorOn();
					Invoke(nameof(UpperJump), 0.3f);
					break;
				case "attack_downsmash":
					m_playerState.PlayerStateSPAttack();
					m_playerCrowdControlManager.SuperArmorOn();
					Invoke(nameof(SmashDown), 0.37f);
					break;
				default:
					break;
			}
		}
	}

	private void PlayStartSwitch(string _animName)
	{
		if (m_animFuntion.IsTag("NormalAttack"))
		{
			if (m_playerState.IsPlayerGround())
				m_rigidbody2D.velocity = new Vector2(0, m_rigidbody2D.velocity.y);

			m_playerState.PlayerStateAttack();

			switch (_animName)
			{
				case "attack_3":
					AddMeve(4.0f);
					m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["attack_3_1"].damageRatio, "Monster", m_NormalAttackDic["attack_3_1"].damageForce);
					m_randAudioFuntion.VoiceRandPlay("Attack");
					break;
				case "air_attack_3":
					AddMeve(4.0f);
					m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["air_attack_3"].damageRatio, "Monster", m_NormalAttackDic["air_attack_3"].damageForce);
					m_randAudioFuntion.VoiceRandPlay("Attack");
					break;
				case "attack_upper":
					m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic[_animName].damageRatio, "Monster", m_NormalAttackDic[_animName].damageForce);
					m_audioFuntion.VoicePlay("Upper",false);
					break;
				case "attack_downsmash":
					m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic[_animName].damageRatio, "Monster", m_NormalAttackDic[_animName].damageForce);
					m_audioFuntion.VoicePlay("Downsmash", false);
					break;
				default:
					m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic[_animName].damageRatio, "Monster", m_NormalAttackDic[_animName].damageForce);
					m_randAudioFuntion.VoiceRandPlay("Attack");
					break;
			}

			PlayAnimEffect(_animName);
			PlayingStartInvokeSwitch(_animName);
		}
	}
	private void PlayEndSwitch(string _animName)
	{
		if (m_animFuntion.IsTag("NormalAttack"))
		{
			m_characterMove.MoveStop();			
		}

		switch (_animName)
		{
			case "attack_upper":
					m_playerState.PlayerStateJump();
					m_playerCrowdControlManager.SuperArmorOff();
				break;
			case "attack_downsmash":
				m_playerCrowdControlManager.SuperArmorOff();
				break;
			default:
				if (!m_playerState.IsPlayerGround())
					m_playerState.PlayerStateDoubleJump();
				break;
		}

		m_effectAnimFuntion.EffectOff();
		m_playerCrowdControlManager.OffAirStop();

	}

	private void AddMeve(float _speed)
	{
		if (Input.GetAxisRaw("Horizontal") < 0 || m_playerInput.joystickState == PlayerInput.JOYSTICK_STATE.JOYSTICK_LEFT)
		{
			m_characterMove.MoveLeft(_speed);
		}
		else if (Input.GetAxisRaw("Horizontal") > 0 || m_playerInput.joystickState == PlayerInput.JOYSTICK_STATE.JOYSTICK_RIGHT)
		{
			m_characterMove.MoveRight(_speed);
		}
	}

	//Invoke용 함수들
	private void Attack3Plus()
	{
		m_characterMove.MoveStop();
		m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["attack_3_2"].damageRatio, "Monster", m_NormalAttackDic["attack_3_2"].damageForce);
		m_effectAnimFuntion.EffectPlay("attack_3_2", false);
		m_randAudioFuntion.VoiceRandPlay("Attack");
	}

	private void UpperJump()
	{
		m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 22f);
	}

	private void SmashDown()
	{
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -40f);
	}
}