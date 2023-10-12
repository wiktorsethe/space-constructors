using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossSimpleAttackState : StateMachineBehaviour
{
    private float timer = 0f;
    private float shootTimer = 0f;
    private float minTimer = 10f;
    private float maxTimer = 17f;
    private float currentTimerStomp;
    private GameObject ship;
    private float randTimer;
    private float randSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        shootTimer = 0f;
        currentTimerStomp = Random.Range(minTimer, maxTimer);
        ship = animator.GetComponent<FirstBossScript>().FindClosestObject();

        randSpeed = Random.Range(-1000, 1000) / 100;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<FirstBossScript>().FindClosestObject();

        timer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        animator.GetComponent<FirstBossScript>().ChangeRotation();
        if (shootTimer >= 4f)
        {
            randSpeed = Random.Range(-1000, 1000) / 50;
            animator.GetComponent<FirstBossScript>().FireBullet(0);
            animator.GetComponent<FirstBossScript>().FireBullet(1);
            shootTimer = 0f;
        }

        if (Vector2.Distance(animator.transform.position, ship.transform.position) >= 16f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, 4f * Time.deltaTime);
        }
        else if(Vector2.Distance(animator.transform.position, ship.transform.position) < 15.9f)
        {
            Vector3 direction = animator.transform.position - ship.transform.position;
            direction.Normalize();

            animator.transform.Translate(direction * 14f * Time.deltaTime);
            //animator.transform.position = Vector2.MoveTowards(animator.transform.position, direction, 14f * Time.deltaTime);
        }
        else
        {
            animator.transform.GetComponent<FirstBossScript>().Pivot(randSpeed);
        }

        if (timer >= currentTimerStomp)
        {
            animator.SetTrigger("Start");
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
