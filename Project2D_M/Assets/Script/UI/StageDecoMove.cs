using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDecoMove : MonoBehaviour
{
    public GameObject decoObject;
    public void MoveForwardObject()
    {
        Animator animator = decoObject.GetComponent<Animator>();

        if (decoObject != null)
        {
            animator.SetBool("bMoveForward", true);
        }
    }

    public void MoveBackObject()
    {
        Animator animator = decoObject.GetComponent<Animator>();

        if (decoObject != null)
        {
            animator.SetBool("bMoveForward", false);
        }
    }
}
