using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossFlamethrowerAttackState : StateMachineBehaviour
{
    Vector2 newPos;
    private float timer = 0f;
    private float playerSize;
    public PlayerStats playerStats;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        newPos = animator.GetComponent<FirstBossScript>().nextCorner;
        animator.GetComponent<FirstBossScript>().FlameThrower();
        playerSize = animator.GetComponent<FirstBossScript>().ObjectSize();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer >= 4f)
        {
            if (Vector2.Distance(animator.transform.position, newPos) > playerSize * 1.1f)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, newPos, (playerStats.shipSpeedValue * 1.5f) * Time.deltaTime);
            }
            else
            {
                animator.GetComponent<FirstBossScript>().FlameThrowerEnd();
                animator.GetComponent<PolygonCollider2D>().enabled = true;
                animator.SetTrigger("Start");
            }
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
