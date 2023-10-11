using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossMinionStartState : StateMachineBehaviour
{
    private GameObject boss;
    private float bossSize;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Find("EnemyShipImage").transform.GetComponent<PolygonCollider2D>().enabled = false;
        boss = animator.GetComponent<FirstBossMinionScript>().pivotObject;

        Renderer[] bossRenderers = boss.GetComponentsInChildren<Renderer>();
        if (bossRenderers.Length > 0)
        {
            Bounds combinedBounds = bossRenderers[0].bounds;
            for (int i = 1; i < bossRenderers.Length; i++)
            {
                combinedBounds.Encapsulate(bossRenderers[i].bounds);
            }
            Vector3 size = combinedBounds.size;
            bossSize = size.magnitude;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(boss.transform.position, animator.transform.position) < bossSize)
        {
            Vector3 direction = animator.transform.position - boss.transform.position;
            direction.Normalize();

            animator.transform.Translate(direction * 14f * Time.deltaTime);
            //animator.transform.position = Vector2.MoveTowards(animator.transform.position, -boss.transform.position, 4f * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("Rotating");
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
