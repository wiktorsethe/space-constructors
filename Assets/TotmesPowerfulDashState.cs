using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TotmesPowerfulDashState : StateMachineBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    Vector2 dashPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashPos = animator.GetComponent<TotmesPowerful>().GetRandomPointInCameraView();

        Color initialColor = animator.GetComponentInChildren<SpriteRenderer>().color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0.2f);
        animator.GetComponentInChildren<SpriteRenderer>().DOColor(targetColor, 1f);
        animator.GetComponent<Collider2D>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position, dashPos) > 0.05f)
        {
            //Vector3 vectorToTarget = ship.transform.position - animator.transform.position;
            //animator.transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, dashPos, playerStats.shipSpeedValue * 2f * Time.deltaTime);
        }
        else if (Vector2.Distance(animator.transform.position, dashPos) <= 0.05f)
        {
            //animator.SetTrigger("");
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
