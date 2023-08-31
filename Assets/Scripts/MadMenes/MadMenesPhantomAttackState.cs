using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MadMenesPhantomAttackState : StateMachineBehaviour
{
    private float timer = 2f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 2f;

        SpriteRenderer[] sprites = animator.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            Color initialColor = sprite.color;
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            sprite.DOColor(targetColor, 1f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MadMenesPhantomMinion>().ChangeRotation();
        timer += Time.deltaTime;

        if(timer >= 3f)
        {
            animator.GetComponent<MadMenesPhantomMinion>().FireBullet();
            timer = 0f;
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
