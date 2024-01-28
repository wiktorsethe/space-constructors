using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCatchAnim : StateMachineBehaviour
{
    private GameObject player;
    [SerializeField] private PlayerStats playerStats;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //choosenTime = Random.Range(minTime, maxTime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.position.x != player.transform.position.x)
        {
            Vector2 vectorToTarget = new Vector2(player.transform.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, vectorToTarget, (playerStats.shipSpeedValue * 1.5f) * Time.deltaTime);
        }

        float distance = Mathf.Abs(animator.transform.position.x - player.transform.position.x);
        if (distance <= 0.05f)
        {
            animator.SetTrigger("Attack");
        }


        /*
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

            Vector2 vectorToTarget = new Vector2(player.transform.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, vectorToTarget, 2f * Time.deltaTime);

            
        }
        */
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
