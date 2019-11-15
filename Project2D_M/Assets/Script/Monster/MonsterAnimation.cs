using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MONSTER_STATE
{
    APPEAR,
    MOVE,
    ATTACK,
    STUN,
    DIE
}
public class MonsterAnimation : MonoBehaviour
{
    private Animator m_animator;
    public float speed;

    private readonly int hashFSpeed = Animator.StringToHash("fSpeed");
    private readonly int hashBLive = Animator.StringToHash("bLive");
    private readonly int hashBStun = Animator.StringToHash("bStun");
    private readonly int hashBAppear = Animator.StringToHash("bAppear");
    private readonly int hashTHit = Animator.StringToHash("tHit");
    private readonly int hashTAttack = Animator.StringToHash("tAttack");
    private readonly int hashFDistanceToPlayer = Animator.StringToHash("fDistanceToPlayer");

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetBool(hashBLive, true);
        //m_animator.SetFloat(hashFSpeed, speed);


    }

    private void Update()
    {
        
    }

    void Action(MONSTER_STATE _state)
    {
        switch(_state)
        {
            case MONSTER_STATE.ATTACK:
                m_animator.SetTrigger(hashTAttack);
                break;

            case MONSTER_STATE.DIE:
                m_animator.SetBool(hashBLive, false);
                break;

            case MONSTER_STATE.APPEAR:
                m_animator.SetBool(hashBAppear, true);
                break;

            case MONSTER_STATE.MOVE:
                m_animator.SetFloat(hashFSpeed, speed);
                break;

            case MONSTER_STATE.STUN:
                break;


        }
    }




}
