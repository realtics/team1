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
    struct AttackInfo
    {
        public float damageRatio;
        public Vector2 damageForce;
        public AttackInfo(float _damageRatio, Vector2 _damageForce = default(Vector2))
        {
            damageRatio = _damageRatio;

            if(_damageForce == Vector2.zero)
                damageForce = Vector2.zero;
            else damageForce = _damageForce;
        }
    }

    public EffectAnimFuntion m_effectAnimFuntion;

    private Dictionary<string, AttackInfo> m_NormalAttackDic;
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private CharacterMove m_characterMove = null;
    private AttackManager m_attackCollider = null;
    private PlayerState m_playerState = null;
    private PlayerUiInput m_playerUiInput = null;
    private bool m_bAttacking;

    private void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_animFuntion = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_attackCollider = this.transform.Find("AttackManager").GetComponent<AttackManager>();
        m_playerState = this.GetComponent<PlayerState>();
        m_playerUiInput = this.GetComponent<PlayerUiInput>();
        m_bAttacking = false;

        m_NormalAttackDic = new Dictionary<string, AttackInfo>();

        m_NormalAttackDic.Add("attack_1", new AttackInfo(1.0f, new Vector2(2.0f, 3f)));
        m_NormalAttackDic.Add("attack_2", new AttackInfo(1.0f, new Vector2(2.0f, 3f)));
        m_NormalAttackDic.Add("attack_3_1", new AttackInfo(0.5f, new Vector2(2.0f, 5.0f)));
        m_NormalAttackDic.Add("attack_3_2", new AttackInfo(2.0f, new Vector2(2.0f, -10.0f)));
        m_NormalAttackDic.Add("attack_4", new AttackInfo(3.0f, new Vector2(2.0f, 10.0f)));
        m_NormalAttackDic.Add("attack_5", new AttackInfo(4.0f, new Vector2(10.0f, 10.0f)));

        m_NormalAttackDic.Add("air_attack_1", new AttackInfo(1.0f, new Vector2(2f, 12.0f)));
        m_NormalAttackDic.Add("air_attack_2", new AttackInfo(1.0f, new Vector2(2f, 12.0f)));
        m_NormalAttackDic.Add("air_attack_3", new AttackInfo(1.0f, new Vector2(2f, 12.0f)));
        m_NormalAttackDic.Add("air_attack_4", new AttackInfo(1.0f, new Vector2(5f, 12.0f)));

        m_NormalAttackDic.Add("attack_upper", new AttackInfo(3.0f, new Vector2(2f, 30.0f)));
        m_NormalAttackDic.Add("attack_downsmash", new AttackInfo(4.0f, new Vector2(2f, -25.0f)));
    }

    public void NormalAttack()
    {
        m_animFuntion.SetTrigger("tNormalAttack");

        if(Input.GetAxisRaw("Vertical") > 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_UP)
        {
            m_animFuntion.SetTrigger("tUpper");
        }
        else if (Input.GetAxisRaw("Vertical") < 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_DOWN)
        {
            m_animFuntion.SetTrigger("tDownsmash");
        }

        if (!m_bAttacking)
        {
            m_effectAnimFuntion.EffectOn();
            StartCoroutine(AttackCoroutine());
            m_bAttacking = true;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return 0;

        if (!m_animFuntion.IsTag("NormalAttack"))
        {
            m_bAttacking = false;
            yield break;
        }

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        string m_sAnimName = m_animFuntion.GetCurrntAnimClipName();
        PlayAnimEffect(m_sAnimName);
        PlayStartSwitch(m_sAnimName);

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
                m_effectAnimFuntion.EffectOff();
                break;
            }

            yield return 0;
        }

        m_bAttacking = false;
        m_sAnimName = "";

        if (!m_playerState.IsPlayerEvasion())
        {
            m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_rigidbody2D.AddForce(new Vector2(0.1f, 0.1f));
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
                    Invoke(nameof(UpperJump), 0.3f);
                    break;
                case "attack_downsmash":
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
            PlayAnimEffect(_animName);
            switch (_animName)
            {
                case "attack_3":
                    AddMeve(4.0f);
                    m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["attack_3_1"].damageRatio, "Monster", m_NormalAttackDic["attack_3_1"].damageForce);
                    break;
                case "air_attack_3":
                    AddMeve(4.0f);
                    m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["air_attack_3"].damageRatio, "Monster", m_NormalAttackDic["air_attack_3"].damageForce);
                    break;
                default:
                    AddMeve(0.0f);
                    m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic[_animName].damageRatio, "Monster", m_NormalAttackDic[_animName].damageForce);
                    break;
            }

            PlayingStartInvokeSwitch(_animName);
        }
    }
    private void PlayEndSwitch(string _animName)
    {
        if (m_animFuntion.IsTag("NormalAttack"))
        {
            m_characterMove.MoveStop();
        }
    }

    private void AddMeve(float _speed)
    {
        if (Input.GetAxisRaw("Horizontal") < 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_LEFT)
        {
            m_characterMove.MoveLeft(_speed);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_RIGHT)
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
    }

    private void UpperJump()
    {
        m_playerState.PlayerStateJump();
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.AddForce(new Vector2(1f, 25f), ForceMode2D.Impulse);
    }

    private void SmashDown()
    {
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.AddForce(new Vector2(1f, -40f), ForceMode2D.Impulse);
    }
}