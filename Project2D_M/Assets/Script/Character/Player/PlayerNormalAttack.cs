using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerNormalAttack : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private SkeletonAnimation m_skeletonAnimation = null;
    private CharacterMove m_characterMove = null;
    private bool m_bAttacking;

    private void Awake()
    {
        m_rigidbody2D   = this.GetComponent<Rigidbody2D>();
        m_animFuntion   = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
        m_skeletonAnimation = this.transform.Find("NormalAttacks").GetComponent<SkeletonAnimation>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_bAttacking = false;
    }

    public void NormalAttack()
    {
        m_animFuntion.SetTrigger("TriggerNormalAttack");

        if (!m_bAttacking)
        {
            StartCoroutine(AttackCoroutine());
            m_bAttacking = true;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return 0;

        m_characterMove.MoveStop();
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

            if (!m_animFuntion.IsAnimationPlayingTag("NormalAttack"))
            {
                Debug.Log("break");
                PlayEndSwitch(m_sAnimName);
                break;
            }

            PlayingAnimSwitch(m_sAnimName);

            yield return 0;
        }

        m_bAttacking = false;
        m_sAnimName = "";

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.AddForce(new Vector2(0.1f,0.1f));
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
                        m_skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_2", false);
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
                    break;
                case "air_attack_3":
                    AddMeve(4.0f);
                    break;
                default:
                    AddMeve(0.0f);
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