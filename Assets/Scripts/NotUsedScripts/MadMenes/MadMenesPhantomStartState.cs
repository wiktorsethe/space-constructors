using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MadMenesPhantomStartState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private float timer = 0f;
    private float minTimer = 10f;
    private float maxTimer = 20f;
    private float timerStomp;
    private bool isSpawned = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        timerStomp = Random.Range(minTimer, maxTimer);
        isSpawned = false;

        SpriteRenderer[] sprites = animator.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            Color initialColor = sprite.color;
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            sprite.DOColor(targetColor, 1f);
        }
        animator.GetComponent<Collider2D>().enabled = false;
        PlayerPrefs.SetString("PhantomAttack", "true");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > 1f && !isSpawned)
        {
            animator.GetComponent<MadMenes>().SpawnPhantoms();
            isSpawned = true;
        }

        if(timer >= timerStomp)
        {
            animator.SetTrigger("PhantomEnd");
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
