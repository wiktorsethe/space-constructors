using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire2Anim : StateMachineBehaviour
{
    private float timer;
    private float shootTimer = 0f;
    [SerializeField] private GameObject bulletPrefab;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer >= 4)
        {
            timer = 0f;
            animator.GetComponent<LuciusMaximus>().isFirstGunUsed = false;
            animator.GetComponent<LuciusMaximus>().isSecondGunUsed = false;
            animator.SetTrigger("Idle");
        }
        else
        {
            timer += Time.deltaTime;
            shootTimer += Time.deltaTime;

            if (shootTimer >= 0.5f)
            {
                animator.GetComponent<LuciusMaximus>().FireBullet(0);
                animator.GetComponent<LuciusMaximus>().FireBullet(1);
                shootTimer = 0f;
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
