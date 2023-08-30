using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotmesPowerfulAttack1State : StateMachineBehaviour
{
    private float timer = 0f;
    private float shootTimer = 0f;
    private float minTimer = 10f;
    private float maxTimer = 17f;
    private float currentTimerStomp;
    private GameObject ship;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        shootTimer = 0f;
        currentTimerStomp = Random.Range(minTimer, maxTimer);
        ship = animator.GetComponent<TotmesPowerful>().FindClosestObject();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        animator.GetComponent<TotmesPowerful>().ChangeRotation();

        if (shootTimer >= 4f)
        {
            animator.GetComponent<TotmesPowerful>().FireBullet(0);
            animator.GetComponent<TotmesPowerful>().FireBullet(1);
            shootTimer = 0f;
        }

        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 10f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, 4f * Time.deltaTime);
        }

        if (timer >= currentTimerStomp)
        {
            animator.SetTrigger("Start");
        }
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
