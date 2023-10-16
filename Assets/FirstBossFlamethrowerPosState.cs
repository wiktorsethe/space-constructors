using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossFlamethrowerPosState : StateMachineBehaviour
{
    private Vector2 specialAttackPos;
    int randomBorder; // 0: top, 1: right, 2: bottom, 3: left

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PolygonCollider2D>().enabled = false;
        randomBorder = Random.Range(0, 4);
        specialAttackPos = animator.GetComponent<FirstBossScript>().GetRandomPointOnBorder(randomBorder);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position, specialAttackPos) > 0.05f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, specialAttackPos, 10f * Time.deltaTime);

            switch (randomBorder)
            {
                case 0: // Top border
                    animator.GetComponent<FirstBossScript>().ChangeRotBorder(Quaternion.Euler(0f, 0f, 180f));
                    break;
                case 1: // Right border
                    animator.GetComponent<FirstBossScript>().ChangeRotBorder(Quaternion.Euler(0f, 0f, 180f));
                    break;
                case 2: // Bottom border
                    animator.GetComponent<FirstBossScript>().ChangeRotBorder(Quaternion.Euler(0f, 0f, 0f));
                    break;
                case 3: // Left border
                    animator.GetComponent<FirstBossScript>().ChangeRotBorder(Quaternion.Euler(0f, 0f, 0f));
                    break;
            }
        }
        else
        {
            animator.SetTrigger("FlamethrowerAttack");
        }
    }

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
