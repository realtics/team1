using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFuntion : AnimFuntion
{
	public readonly int hashBMove = Animator.StringToHash("bMove");
	public readonly int hashBJump = Animator.StringToHash("tJump");
	public readonly int hashTLend = Animator.StringToHash("tLend");
	public readonly int hashTFall = Animator.StringToHash("tFall");
	public readonly int hashTNormalAttack = Animator.StringToHash("tNormalAttack");
	public readonly int hashTUpper = Animator.StringToHash("tUpper");
	public readonly int hasTDownsmash = Animator.StringToHash("tDownsmash");
	public readonly int hasTDie = Animator.StringToHash("tDie");
	public readonly int hasTHit = Animator.StringToHash("tHit");
	public readonly int hashTEvasionAir = Animator.StringToHash("tEvasionAir");
	public readonly int hashTEvasion = Animator.StringToHash("tEvasion");
}
