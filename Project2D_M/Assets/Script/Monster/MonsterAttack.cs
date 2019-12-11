using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class MonsterAttack : MonoBehaviour
{
    private enum MONSTER_ATTACK
    {
        ATTACK_1 = 1,
        ATTACK_2,
        SKILL_1,
        SkILL_2,
        PAGE_1
    }
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
    private AttackCollider m_attackcollider = null;
    private bool m_bAttacking;
    private Animator m_animator;
    private float m_fAttackDelay = 2.0f;
    private float m_fNextAttackTime = 0.0f;


    //수정중인 사항
    private MONSTER_ATTACK m_eAttack;
    public bool m_bAttack;
    //해당 스크립트 스킬을 받아서 해당 스크립트로 발동하기 위한 틀 (attack은 기본적인 밀리 공격)
    public Component m_monsterSkill;
    private readonly int m_hashiAttackType = Animator.StringToHash("iAttackType");

    private void Awake()
    {
        m_attackcollider = this.GetComponentInChildren<AttackCollider>();
        m_animator = this.GetComponentInChildren<Animator>();
        m_bAttacking = false;
        m_normalAttackDic = new Dictionary<string, AttackInfo>();
        m_normalAttackDic.Add("Attack_1", new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
        m_normalAttackDic.Add("Attack_2", new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));

        
    }


    private void Update()
    {
        //if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("move"))

        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            m_bAttacking = false;
        }
        if (!m_bAttacking && m_bAttack)
        {
            if (Time.time >= m_fNextAttackTime)
            {
                Attack();
                m_bAttacking = true;

                m_fNextAttackTime = Time.time + m_fAttackDelay;
            }

        }
    }

    public void Attack()
    {
        /////
        ///// 어택 루틴 
        ///// 1. ai에서 어택의 m_bAttack = true
        ///// 2. 업데이트에서 어택함수 호출 
        ///// 3. 어택에서 whatAttack() 호출
        ///// 4. 어떤공격을 할건지 랜덤으로 뽑아줌
        ///// 5. 어택함수에서 각각의 공격을 실행시켜줌
        ///// 6. 각 공격에 맞는 넘버를 애니메이션 컨트롤러에 넘겨줌


        //m_attackcollider.SetDamageColliderInfo(m_normalAttackDic["Attack_1"].damageRatio
        //    , "Player", m_normalAttackDic["Attack_1"].damageForce);
        ////수정전 코드가 작동하도록 하기위해 return;문을 넣어둠
        //return;


        WhatAttack();

        switch (m_eAttack)
        {
            case MONSTER_ATTACK.ATTACK_1:
                m_attackcollider.SetDamageColliderInfo(m_normalAttackDic["Attack_1"].damageRatio, "Player", m_normalAttackDic["Attack_1"].damageForce);
                m_animator.SetInteger(m_hashiAttackType, (int)m_eAttack);
                break;
            case MONSTER_ATTACK.ATTACK_2:
                m_attackcollider.SetDamageColliderInfo(m_normalAttackDic["Attack_2"].damageRatio, "Player", m_normalAttackDic["Attack_2"].damageForce);
                m_animator.SetInteger(m_hashiAttackType, (int)m_eAttack);

                break;
            case MONSTER_ATTACK.SKILL_1:
                break;
            case MONSTER_ATTACK.SkILL_2:
                break;
            case MONSTER_ATTACK.PAGE_1:
                break;
        }
        m_animator.SetTrigger("tAttack");

    }


    private void WhatAttack()
    {
        int random;
        random = Random.Range(1, 40);
                         
        if(random %4 == 0)
        {
            m_eAttack = MONSTER_ATTACK.ATTACK_2;
        }
        else
        {
            m_eAttack = MONSTER_ATTACK.ATTACK_1;
        }
        
    }
}
