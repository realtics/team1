using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerNormalAttack : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private bool m_bAttacking;

    public SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        m_bAttacking    = false;
        m_playerState   = this.GetComponent<PlayerState>();
        m_rigidbody2D   = this.GetComponent<Rigidbody2D>();
        m_animFuntion   = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
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
        string m_sAnimName;

        yield return 0;

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;

        m_sAnimName = m_animFuntion.GetCurrntAnimClipName();
        PlayAnimEffect(m_sAnimName);

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
                break;
            }

            PlayingAnimSwitch(m_sAnimName);

            yield return 0;
        }

        m_bAttacking = false;
        m_sAnimName = "";

        m_rigidbody2D.constraints = RigidbodyConstraints2D.None;
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
                    skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_1", false);
                    break;
                default:
                    skeletonAnimation.AnimationState.SetAnimation(0, _animName, false);
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
                    if (m_animFuntion.GetCurrntClipTime() > 0.95f && m_animFuntion.GetCurrntClipTime() < 0.97f)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_2", false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void PlayStartSwitch(string _animName)
    {
        PlayAnimEffect(_animName);

        if (m_animFuntion.IsTag("NormalAttack"))
        {
            switch (_animName)
            {
                default:
                    break;
            }
        }
    }
    private void PlayEndSwitch(string _animName)
    {
        if (m_animFuntion.IsTag("NormalAttack"))
        {
            switch (_animName)
            {
                default:
                    break;
            }
        }
    }
}