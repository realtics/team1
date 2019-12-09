using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLendBehavior : StateMachineBehaviour
{
	private Rigidbody2D m_rigidbody2D = null;
	private PlayerCrowdControlManager m_crowdControlManager;
	public float stiffenTime;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_rigidbody2D == null)
			m_rigidbody2D = animator.transform.root.GetComponent<Rigidbody2D>();

		if(m_crowdControlManager == null)
			m_crowdControlManager = animator.transform.root.GetComponent<PlayerCrowdControlManager>();

		m_rigidbody2D.velocity = new Vector2(0,0);
		m_crowdControlManager.Stiffen(stiffenTime);
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
