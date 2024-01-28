using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAnim : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;
    public float choosenTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        choosenTime = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer >= choosenTime)
        {
            timer = 0f;
            animator.GetComponent<LuciusMaximus>().isFirstGunUsed = false;
            animator.GetComponent<LuciusMaximus>().isSecondGunUsed = false;
            animator.SetTrigger("Reset");
        }
        else
        {
            timer += Time.deltaTime;

            if ((int)timer % 2 == 0 && !animator.GetComponent<LuciusMaximus>().isFirstGunUsed)
            {
                animator.GetComponent<LuciusMaximus>().FireBullet(0);
                animator.GetComponent<LuciusMaximus>().isFirstGunUsed = true;
                animator.GetComponent<LuciusMaximus>().isSecondGunUsed = false;
            }
            else if ((int)timer % 2 == 1 && !animator.GetComponent<LuciusMaximus>().isSecondGunUsed)
            {
                animator.GetComponent<LuciusMaximus>().FireBullet(1);
                animator.GetComponent<LuciusMaximus>().isFirstGunUsed = false;
                animator.GetComponent<LuciusMaximus>().isSecondGunUsed = true;
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
