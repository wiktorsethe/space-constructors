using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMadMenesAttackState : StateMachineBehaviour
{
    private GameObject ship;
    private float timer = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<MadMenesMinion>().FindClosestObject();
        timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MadMenesMinion>().ChangeRotation();
        timer += Time.deltaTime;
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 10f)
        {
            animator.SetTrigger("Start");
        }
        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 10f)
        {
            if (timer >= 3f)
            {
                animator.transform.GetComponent<MadMenesMinion>().FireBullet();
                timer = 0f;
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
