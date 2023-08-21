using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashStartState : StateMachineBehaviour
{
    private GameObject ship;
    private float timer = 0f;
    [SerializeField] private PlayerStats playerStats;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyDash>().FindClosestObject();
        timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 15f)
        {
            if(timer < 4f)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, 5f * Time.deltaTime);
            }
            else if(timer >= 4f)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, (playerStats.shipSpeedValue * 1.5f) * Time.deltaTime);
            }
        }
        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 15f)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
