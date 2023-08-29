using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TotmesPowerfulDashAttackState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SpriteRenderer[] sprites = animator.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            Color initialColor = sprite.color;
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            sprite.DOColor(targetColor, 1f);
        }
        animator.GetComponent<Collider2D>().enabled = false;

        animator.GetComponent<TotmesPowerful>().FireBullet(0);
        animator.GetComponent<TotmesPowerful>().FireBullet(1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<TotmesPowerful>().ChangeRotation();

        if (PlayerPrefs.GetInt("AmountOfDash") < 4)
        {
            animator.SetTrigger("Dash");
        }
        else
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
