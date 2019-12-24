using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillScriptableObject : ScriptableObject
{
	public string skillName;
	public float collisionSize;
	public float damageRatio;
	public Vector2 damageForce;
	public Sprite skillImage;
	public float coolTime;
	public int levelLimit;
}