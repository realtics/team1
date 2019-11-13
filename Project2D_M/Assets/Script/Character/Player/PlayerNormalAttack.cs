using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : MonoBehaviour
{
    private PlayerState m_playerState = null;
<<<<<<< HEAD
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private NORMAL_ATTACK_NUM m_eNormalNum;
    private bool m_normalB;

    public SkeletonAnimation skeletonAnimation;

    private void Awake()
=======
    private Animator    m_animator = null;
    // Start is called before the first frame update
    void Start()
>>>>>>> parent of 77029aa... 11.12
    {
        m_playerState   = this.GetComponent<PlayerState>();
<<<<<<< HEAD
        m_rigidbody2D   = this.GetComponent<Rigidbody2D>();
        m_animFuntion   = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
=======
        m_animator = this.transform.Find("PlayerSpineSprite").GetComponent<Animator>();
>>>>>>> parent of 77029aa... 11.12
    }

    public void NormalAttack()
    {
<<<<<<< HEAD
        m_animFuntion.SetTrigger("TriggerNormalAttack");

        if (m_eNormalNum == NORMAL_ATTACK_NUM.NORMAL_NO)
        {
            StartCoroutine(AttackCoroutine());
            m_eNormalNum = NORMAL_ATTACK_NUM.NORMAL_1;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        string m_sAnimName;
        m_normalB = false;
        yield return 0;

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;

        m_sAnimName = m_animFuntion.GetCurrntAnimClipName();
        PlayAnimEffect(m_sAnimName);

        while (true)
        {
            if (!m_animFuntion.IsName(m_sAnimName))
            {
                m_sAnimName = m_animFuntion.GetCurrntAnimClipName();
                PlayAnimEffect(m_sAnimName);
            }

            if (!m_animFuntion.IsAnimationPlayingTag("NormalAttack"))
            {
                Debug.Log("break");
                break;
            }

            PlayAnimSwitch(m_sAnimName);

            yield return 0;
        }

        m_eNormalNum = NORMAL_ATTACK_NUM.NORMAL_NO;
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
=======
        m_animator.SetTrigger("TriggerNormalAttack");
>>>>>>> parent of 77029aa... 11.12
    }

    private void PlayAnimSwitch(string _animName)
    {
        if (m_animFuntion.IsTag("NormalAttack"))
        {
            switch (_animName)
            {
                case "attack_3":
                    Debug.Log(m_animFuntion.GetCurrntClipTime());
                    if (m_animFuntion.GetCurrntClipTime() > 0.95f && !m_normalB)
                    {
                        m_normalB = true;
                        skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_2", false);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}