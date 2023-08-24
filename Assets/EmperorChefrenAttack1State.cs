using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorChefrenAttack1State : StateMachineBehaviour
{
    private float timer = 0f;
    private float shootTimer = 0f;
    private float minTimer = 9f;
    private float maxTimer = 17f;
    private float currentTimerStomp;
    private GameObject ship;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerPrefs.HasKey("EmperorChefrenTimerAttack1"))
        {
            timer = PlayerPrefs.GetFloat("EmperorChefrenTimerAttack1");
        }
        else
        {
            timer = 0f;
        }
        shootTimer = 3.5f;
        currentTimerStomp = Random.Range(minTimer, maxTimer);
        ship = animator.GetComponent<EmperorChefren>().FindClosestObject();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        animator.GetComponent<EmperorChefren>().ChangeRotation();

        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 20f)
        {
            if(timer >= currentTimerStomp)
            {
                PlayerPrefs.DeleteKey("EmperorChefrenTimerAttack1");
                animator.SetTrigger("Start");
            }
            else
            {
                animator.SetTrigger("Movement");
                PlayerPrefs.SetFloat("EmperorChefrenTimerAttack1", timer);
            }
        }
        else
        {
            if (timer >= currentTimerStomp)
            {
                PlayerPrefs.DeleteKey("EmperorChefrenTimerAttack1");
                animator.SetTrigger("Start");
            }
            

            if(shootTimer >= 4f)
            {
                animator.GetComponent<EmperorChefren>().FireBullet(0);
                animator.GetComponent<EmperorChefren>().FireBullet(1);
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
