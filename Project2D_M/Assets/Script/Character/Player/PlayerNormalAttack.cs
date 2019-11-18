using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
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

    private Dictionary<string, AttackInfo> m_NormalAttackDic;
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private SkeletonAnimation m_skeletonAnimation = null;
    private CharacterMove m_characterMove = null;
    private AttackManager m_attackCollider = null;
    private PlayerState m_playerState = null;
    private bool m_bAttacking;

    private void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_animFuntion = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
        m_skeletonAnimation = this.transform.Find("AttackManager").transform.Find("NormalAttacks").GetComponent<SkeletonAnimation>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_attackCollider = this.transform.Find("AttackManager").GetComponent<AttackManager>();
        m_playerState = this.GetComponent<PlayerState>();
        m_bAttacking = false;

        m_NormalAttackDic = new Dictionary<string, AttackInfo>();

        m_NormalAttackDic.Add("attack_1", new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
        m_NormalAttackDic.Add("attack_2", new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
        m_NormalAttackDic.Add("attack_3_1", new AttackInfo(1.0f, new Vector2(2.0f, 13.0f)));
        m_NormalAttackDic.Add("attack_3_2", new AttackInfo(2.0f, new Vector2(2.0f, -10.0f)));
        m_NormalAttackDic.Add("attack_4", new AttackInfo(3.0f, new Vector2(2.0f, 10.0f)));
        m_NormalAttackDic.Add("attack_5", new AttackInfo(4.0f, new Vector2(10.0f, 10.0f)));

        m_NormalAttackDic.Add("air_attack_1", new AttackInfo(1.0f, new Vector2(2f, 15.0f)));
        m_NormalAttackDic.Add("air_attack_2", new AttackInfo(1.0f, new Vector2(2f, 15.0f)));
        m_NormalAttackDic.Add("air_attack_3", new AttackInfo(1.0f, new Vector2(2f, 15.0f)));
        m_NormalAttackDic.Add("air_attack_4", new AttackInfo(1.0f, new Vector2(5f, 15.0f)));

        m_NormalAttackDic.Add("attack_upper", new AttackInfo(1.0f, new Vector2(2f, 28.0f)));
        m_NormalAttackDic.Add("attack_downsmash", new AttackInfo(1.0f, new Vector2(2f, -25.0f)));
    }

    public void NormalAttack()
    {
        m_animFuntion.SetTrigger("tNormalAttack");

        if(Input.GetAxisRaw("Vertical") > 0)
        {
            m_animFuntion.SetTrigger("tUpper");
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            m_animFuntion.SetTrigger("tDownsmash");
        }

        if (!m_bAttacking)
        {
            StartCoroutine(AttackCoroutine());
            m_bAttacking = true;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return 0;

        PlayerState playerState = GetComponent<PlayerState>();

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
                break;
            }

            PlayingAnimSwitch(m_sAnimName);

            yield return 0;
        }

        m_bAttacking = false;
        m_sAnimName = "";

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.AddForce(new Vector2(0.1f, 0.1f));
    }

    private void PlayAnimEffect(string _animName)
    {
        if (m_animFuntion.IsTag("NormalAttack"))
        {
            switch (_animName)
            {
                case "attack_3":
                    m_skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_1", false);
                    break;
                case "attack_upper":
                    m_skeletonAnimation.AnimationState.SetAnimation(0, "upper", false);
                    break;
                case "attack_downsmash":
                    m_skeletonAnimation.AnimationState.SetAnimation(0, "downsmash", false);
                    break;
                default:
                    m_skeletonAnimation.AnimationState.SetAnimation(0, _animName, false);
                    break;
            }
        }
    }

    private void PlayingAnimSwitch(string _animName)
    {
        if (m_animFuntion.IsTag("NormalAttack"))
        {
            switch (_animName)
            {
                case "attack_3":
                    if (m_animFuntion.GetCurrntClipTime() > 0.945f && m_animFuntion.GetCurrntClipTime() < 0.97f)
                    {
                        m_characterMove.MoveStop();
                        m_attackCollider.SetDamageColliderInfo(m_NormalAttackDic["attack_3_2"].damageRatio, "Monster", m_NormalAttackDic["attack_3_2"].damageForce);
                        m_skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_2", false);
                    }
                    break;
                case "attack_upper":
                    if (m_animFuntion.GetCurrntClipTime() > 0.30f && m_animFuntion.GetCurrntClipTime() < 0.35f)
                    {   m_playerState.PlayerStateJump();
                        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                        m_rigidbody2D.AddForce(new Vector2(1f, 8f), ForceMode2D.Impulse);
                    }
                    break;
                case "attack_downsmash":
                    if (m_animFuntion.GetCurrntClipTime() > 0.30f && m_animFuntion.GetCurrntClipTime() < 0.35f)
                    {
                        m_playerState.PlayerStateJump();
                        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                        m_rigidbody2D.AddForce(new Vector2(1f, -8f), ForceMode2D.Impulse);
                    }
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
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            m_characterMove.MoveLeft(_speed);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            m_characterMove.MoveRight(_speed);
        }
    }
}