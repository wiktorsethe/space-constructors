using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAttackState : StateMachineBehaviour
{
    private GameObject ship;
    private float timer = 3f;
    private float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyShip>().FindClosestObject();
        speed = animator.transform.GetComponent<EnemyShip>().moveSpeed;
        if(PlayerPrefs.HasKey("enemyShipTimer")) timer = PlayerPrefs.GetFloat("enemyShipTimer");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyShip>().ChangeRotation();
        timer += Time.deltaTime;
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 10f)
        {
            PlayerPrefs.SetFloat("enemyShipTimer", timer);
            animator.SetTrigger("Start");
        }

        else if (Vector2.Distance(animator.transform.position, animator.transform.GetComponent<EnemyShip>().savedPos) < 6.5f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 15f)
        {
            if(timer >= 2.5f)
            {
                animator.transform.GetComponent<EnemyShip>().FireBullet();
                timer = 0f;
            }
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
