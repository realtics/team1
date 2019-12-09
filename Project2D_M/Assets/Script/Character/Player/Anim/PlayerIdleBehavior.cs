using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleBehavior : StateMachineBehaviour
{
	private PlayerState m_playerState;
	private PlayerAnimFuntion m_playerAnim;
	private Rigidbody2D m_rigidbody2D = null;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_playerState == null)
			m_playerState = animator.gameObject.transform.root.GetComponent<PlayerState>();

		if (m_rigidbody2D == null)
			m_rigidbody2D = animator.transform.root.GetComponent<Rigidbody2D>();

		if (m_playerAnim == null)
			m_playerAnim = animator.gameObject.GetComponent<PlayerAnimFuntion>();

		m_playerAnim.ResetTrigger(m_playerAnim.hasTDownsmash);
		m_playerAnim.ResetTrigger(m_playerAnim.hashTUpper);
		m_playerAnim.ResetTrigger(m_playerAnim.hashTNormalAttack);
		m_playerAnim.SetBool(m_playerAnim.hashBMove,false);
		m_playerState.PlayerStateReset();

		m_rigidbody2D.velocity = Vector2.zero;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
