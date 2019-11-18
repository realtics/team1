using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToAttackManager : MonoBehaviour
{
    public AttackManager attackManager;
    
    public void ColliderLifeCycleOn(float _time)
    {
        Debug.Log("On");
        attackManager.ColliderLifeCycleOn(_time);
    }
}
