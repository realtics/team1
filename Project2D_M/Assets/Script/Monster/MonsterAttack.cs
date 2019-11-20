using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class MonsterAttack : MonoBehaviour
{
    struct AttackInfo
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

    private Dictionary<string, AttackInfo> m_normalAttackDic;
    private Rigidbody2D m_rigidbody2D = null;
    private AnimFuntion m_animFuntion = null;
    private SkeletonAnimation m_skeletonAnimation = null;
    private CharacterMove m_characterMove = null;
    private AttackCollider m_attackcollider = null;
    private PlayerState m_playerState = null;
    private bool m_bAttacking;

    private void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_animFuntion = this.GetComponent<AnimFuntion>();
        m_skeletonAnimation = this.GetComponentInChildren<AttackManager>().GetComponentInChildren<SkeletonAnimation>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_attackcollider = this.GetComponentInChildren<AttackCollider>();
        m_playerState = this.GetComponent<PlayerState>();
        m_bAttacking = false;

        m_normalAttackDic = new Dictionary<string, AttackInfo>();

        m_normalAttackDic.Add("Attack_1", new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
        m_normalAttackDic.Add("Attack_2", new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));
    }




    public void Attack()
    {

        m_attackcollider.SetDamageColliderInfo(m_normalAttackDic["Attack_1"].damageRatio
        , "Player", m_normalAttackDic["Attack_1"].damageForce);

        //m_skeletonAnimation.AnimationState.SetAnimation(0, "Attack_1", false);

        if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > 0)
        {


        }
        else
        {

        }
    }

    //private void PlayAnimEffect(string _animName)
    //{
    //    if (m_animFuntion.IsTag("NormalAttack"))
    //    {
    //        switch (_animName)
    //        {
    //            case "attack_3":
    //                m_skeletonAnimation.AnimationState.SetAnimation(0, "attack_3_1", false);
    //                break;
    //            case "attack_upper":
    //                m_skeletonAnimation.AnimationState.SetAnimation(0, "upper", false);
    //                break;
    //            case "attack_downsmash":
    //                m_skeletonAnimation.AnimationState.SetAnimation(0, "downsmash", false);
    //                break;
    //            default:
    //                m_skeletonAnimation.AnimationState.SetAnimation(0, _animName, false);
    //                break;
    //        }
    //    }
    //}
}
