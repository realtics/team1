using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MONSTER_STATE
{
    APPEAR,
    MOVE,
    ATTACK,
    HIT,
    STUN,
    DIE,
    IDLE
}
public class MonsterAI : MonoBehaviour

{
    enum Monster_Position
    {
        Monster_Position_Air,
        Monster_Position_Ground
    }

    [SerializeField] private Monster_Position m_monsterPosition;
    private MONSTER_STATE m_eState;
    private MonsterInfo m_monsterInfo;
    private MonsterMove m_monsterMove;
    private Transform m_playerTransform;
    private MonsterAttack m_monsterAttack;
    private AnimFuntion m_animFunction;
    private MonsterHpBar m_monsterHpBar;
    private ReceiveDamage m_receiveDamage;
    private bool m_bLive;
    private bool m_bAppear;

    private Animator m_animator;
    private float m_attackDistance;

    private readonly int m_hashFSpeed = Animator.StringToHash("fSpeed");
    private readonly int m_hashBLive = Animator.StringToHash("bLive");
    private readonly int m_hashBStun = Animator.StringToHash("bStun");
    private readonly int m_hashBAppear = Animator.StringToHash("bAppear");
    private readonly int m_hashTHit = Animator.StringToHash("tHit");
    private readonly int m_hashTAttack = Animator.StringToHash("tAttack");

    private WaitForSeconds m_secondsDelay = new WaitForSeconds(0.3f);

    [SerializeField]
    private float m_fAppearTime;
    [SerializeField]
    private CrowdControlManager m_crowdControlMg;


    private void Awake()
    {
        //몬스터 유저 찾기 2가지 방법중
        //1) 어웨이크에서 유저 오브젝트를 저장 해놓고 좌표 거리계산으로 유저 찾기 << 선택
        //2) 탐색 콜리전으로 크게 콜리젼만들고 콜리젼 안에 들어오면 찾기
        StageManager.Inst.playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_playerTransform = StageManager.Inst.playerTransform;

        m_eState = MONSTER_STATE.APPEAR;

        InitMonsterInfo();
        InitAniamation();

        m_attackDistance = m_monsterInfo.GetAttackDistance();
    }

    private void Start()
    {
        //나중에 바꿀거
        m_monsterHpBar.transform.gameObject.SetActive(false);
        //
        m_bAppear = m_animator.GetBool(m_hashBAppear);
        StartCoroutine(StateCheck());
        StartCoroutine(Action());
        m_crowdControlMg.Impenetrable(m_fAppearTime);
    }

    private void Update()
    {
        if(m_fAppearTime >0)
            m_fAppearTime -= Time.deltaTime;
        if(m_bAppear ==false && m_fAppearTime <= 0)
        {
            m_bAppear = true;
            m_animator.SetBool(m_hashBAppear, true);
            m_monsterHpBar.transform.gameObject.SetActive(true);
        }
        m_monsterHpBar.SetHPBar(m_monsterInfo);
        m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
        // 다시볼것
        if (m_animFunction.GetCurrntAnimClipName() == "hit")
        {
            m_monsterAttack.m_bAttack = false;
        }
    }
    IEnumerator StateCheck()
    {
        //오브젝트 풀에 생성시 다른 스크립트의 초기화를 위해 대기
        yield return new WaitForSeconds(1.0f);

        while (m_bLive)
        {
            if (m_bAppear == false)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            //    yield return null;

            if (m_bLive == false)
                yield break;


            float distanceToPlayer = Vector2.Distance(m_playerTransform.position, this.transform.position);


            if (m_animFunction.GetCurrntAnimClipName() == "hit")
                yield return null;

            if (m_monsterInfo.IsCharacterDie())
            {
                m_eState = MONSTER_STATE.DIE;
            }
            else if (distanceToPlayer < m_attackDistance)
            {
                m_eState = MONSTER_STATE.ATTACK;
            }
            else if (m_eState == MONSTER_STATE.APPEAR)
            {
                m_eState = MONSTER_STATE.MOVE;
            }
            else
            {
                m_eState = MONSTER_STATE.APPEAR;
            }

            if (m_monsterPosition != Monster_Position.Monster_Position_Ground)
            {
                m_eState = MONSTER_STATE.HIT;
                ResetAnim();
                if (m_monsterInfo.IsCharacterDie())
                    m_eState = MONSTER_STATE.DIE;
            }
            yield return m_secondsDelay;
        }
    }

    IEnumerator Action()
    {
        while (m_bLive )
        {
            if (m_bAppear == false)
                yield return null;
            //yield return null;
            yield return m_secondsDelay;

            if (m_monsterPosition == Monster_Position.Monster_Position_Ground)
                switch (m_eState)
                {
                    case MONSTER_STATE.ATTACK:
                        m_monsterMove.isMove = false;
                        if (m_animFunction.GetCurrntAnimClipName() == "idle")
                            m_monsterMove.MoveDir();
                        m_monsterAttack.m_bAttack = true;
                        m_animator.SetFloat(m_hashFSpeed, 0);
                        m_monsterMove.GetMoveParent().MoveStop();
                        //m_monsterMove.SetSpeed(0.0f);
                        break;
                    case MONSTER_STATE.HIT:
                        m_monsterAttack.m_bAttack = false;
						m_animator.SetTrigger(m_hashTHit);
                        break;
                    case MONSTER_STATE.DIE:
                        m_monsterAttack.m_bAttack = false;
                        m_monsterMove.isMove = false;

                        m_animator.SetBool(m_hashBLive, false);

                        StageManager.Inst.SetMonsterCount(m_monsterInfo.bOverKill);
                        m_bLive = false;
                        //m_animator.SetTrigger("tDie");
                        //if (m_animFunction.IsTag("knockdown"))
                        //{
                        //    StageManager.Inst.SetMonsterCount(m_monsterInfo.bOverKill);
                        //    m_bLive = false;
                        //}
                        break;
                    case MONSTER_STATE.APPEAR:
                        m_monsterAttack.m_bAttack = false;

                        //m_animator.SetBool(m_hashBAppear, true);
                        break;
                    case MONSTER_STATE.MOVE:
                        m_monsterAttack.m_bAttack = false;
                        if (m_animFunction.GetCurrntAnimClipName() == "idle")
                        {
                            m_animator.SetFloat(m_hashFSpeed, m_monsterInfo.speed);
                            m_monsterMove.isMove = true;
                        }
                        break;
                    case MONSTER_STATE.STUN:
                        break;
                    default:
                        break;
                }
        }

    }

    void InitMonsterInfo()
    {
        MonsterInfo.MonsterCharInfo monsterCharInfo;
		if(StageDataManager.Inst.nowStage == StageDataManager.StageNameEnum.STAGE_1_1)
		{
			monsterCharInfo.level = 1;
			monsterCharInfo.maxHp = 300;
			monsterCharInfo.defensive = 10;
			monsterCharInfo.attack = 70;
			monsterCharInfo.attackDistance = 2.5f;
			monsterCharInfo.speed = 5.0f;
			m_monsterInfo = GetComponent<MonsterInfo>();
			m_monsterInfo.SetInfo(monsterCharInfo);
		}
		else if (StageDataManager.Inst.nowStage == StageDataManager.StageNameEnum.STAGE_1_2)
		{
			monsterCharInfo.level = 1;
			monsterCharInfo.maxHp = 700;
			monsterCharInfo.defensive = 10;
			monsterCharInfo.attack = 150;
			monsterCharInfo.attackDistance = 2.5f;
			monsterCharInfo.speed = 5.0f;
			m_monsterInfo = GetComponent<MonsterInfo>();
			m_monsterInfo.SetInfo(monsterCharInfo);
		}
        m_monsterMove = GetComponent<MonsterMove>();
        m_monsterAttack = GetComponent<MonsterAttack>();
        m_animFunction = transform.GetComponentInChildren<AnimFuntion>();
        m_receiveDamage = GetComponent<ReceiveDamage>();
        m_monsterHpBar = GetComponentInChildren<MonsterHpBar>();

        m_monsterPosition = Monster_Position.Monster_Position_Ground;

        m_monsterMove.SetSpeed(m_monsterInfo.speed);
        m_bLive = true;
    }


    void InitAniamation()
    {
        m_animator = this.GetComponentInChildren<Animator>();
        m_animator.SetBool(m_hashBLive, true);
    }

    public void SetMonsterPositionAir()
    {
        m_monsterPosition = Monster_Position.Monster_Position_Air;
    }

    public void SetMonsterPositionGround()
    {
        m_monsterPosition = Monster_Position.Monster_Position_Ground;
    }

    private void ResetAnim()
    {
        m_animator.SetFloat(m_hashFSpeed, 0);
        m_monsterMove.isMove=false;

        m_animator.ResetTrigger(m_hashTAttack);
    }
}
