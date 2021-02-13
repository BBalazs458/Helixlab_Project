using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIKControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform leftHandOBJ = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (animator)
        {
            if (leftHandOBJ != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.identity);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandOBJ.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandOBJ.rotation);
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }

}
