﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.14
 * 팀                 : 1팀
 * 스크립트 용도      : 몬스터 AI스크립트
 */


public enum MONSTER_STATE
{
    APPEAR,
    MOVE,
    ATTACK,
    HIT,
    STUN,
    DIE
}
public class MonsterAI : MonoBehaviour

{
    private MONSTER_STATE   m_eState;
    private MonsterInfo     m_monsterInfo;
    private MonsterMove m_monsterMove;
    private Transform m_playerTransform;
    private MonsterAttack m_monsterAttack;
    private bool m_bLive;

    private Animator m_animator;
    public float speed;
    public float attackDistance;

    private readonly int m_hashFSpeed = Animator.StringToHash("fSpeed");
    private readonly int m_hashBLive = Animator.StringToHash("bLive");
    private readonly int m_hashBStun = Animator.StringToHash("bStun");
    private readonly int m_hashBAppear = Animator.StringToHash("bAppear");
    private readonly int m_hashTHit = Animator.StringToHash("tHit");
    private readonly int m_hashTAttack = Animator.StringToHash("tAttack");
    private readonly int m_hashFDistanceToPlayer = Animator.StringToHash("fDistanceToPlayer");

    private WaitForSeconds m_secondsDelay  = new WaitForSeconds(0.3f);


    private void Awake()
    {
        //몬스터 유저 찾기 2가지 방법중
        //1) 어웨이크에서 유저 오브젝트를 저장 해놓고 좌표 거리계산으로 유저 찾기 << 선택
        //2) 탐색 콜리전으로 크게 콜리젼만들고 콜리젼 안에 들어오면 찾기
        StageManager.Inst.playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_playerTransform = StageManager.Inst.playerTransform;

        attackDistance = 4.0f;
        m_eState = MONSTER_STATE.APPEAR;

        InitMonsterInfo();
        InitAniamation();
    }

    private void Start()
    {
        StartCoroutine(StateCheck());
        StartCoroutine(Action());
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(StateCheck());
    //    StartCoroutine(Action());
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StateCheck()
    {
        //오브젝트 풀에 생성시 다른 스크립트의 초기화를 위해 대기
        yield return m_secondsDelay;

        while(m_bLive)
        {
            if (m_bLive == false)
                yield break;

            float dirctionToPlayer = m_playerTransform.position.x - this.transform.position.x;
            float distanceToPlayer = Vector2.Distance(m_playerTransform.position, this.transform.position);

            if (distanceToPlayer < attackDistance)
            {
                m_eState = MONSTER_STATE.ATTACK;
            }
            else if(m_eState == MONSTER_STATE.APPEAR)
            {
                m_eState = MONSTER_STATE.MOVE;
            }


        }
    }

    IEnumerator Action()
    {
        while (m_bLive)
        {
            yield return m_secondsDelay;

            switch (m_eState)
            {
                case MONSTER_STATE.ATTACK:
                    m_animator.SetTrigger(m_hashTAttack);
                    m_monsterAttack.Attack();

                    break;

                case MONSTER_STATE.HIT:
                    m_animator.SetTrigger(m_hashTHit);
                    break;

                case MONSTER_STATE.DIE:
                    m_animator.SetBool(m_hashBLive, false);
                    break;

                case MONSTER_STATE.APPEAR:
                    m_animator.SetBool(m_hashBAppear, true);
                    break;

                case MONSTER_STATE.MOVE:
                    m_animator.SetFloat(m_hashFSpeed, speed);
                    m_monsterMove.Move(speed);
                    break;

                case MONSTER_STATE.STUN:
                    break;


            }
        }
    }

    void InitMonsterInfo()
    {
        MonsterInfo.MonsterCharInfo monsterCharInfo;
        monsterCharInfo.maxHp = 40;
        monsterCharInfo.defensive = 10;
        monsterCharInfo.attack = 10;
        m_monsterInfo = GetComponent<MonsterInfo>();
        m_monsterInfo.SetInfo(monsterCharInfo);
    }

    void InitAniamation()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetBool(m_hashBLive, true);
        //m_animator.SetFloat(hashFSpeed, speed);

    }

}