using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossMinionRotatingState : StateMachineBehaviour
{
    private float timer = 0;
    private float randTimer;
    private float randSpeed;
    private GameObject boss;
    private float bossSize;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Find("EnemyShipImage").transform.GetComponent<PolygonCollider2D>().enabled = true;
        timer = 0;
        randTimer = Random.Range(0, 5);
        randSpeed = Random.Range(-1000, 1000) / 100;

        boss = animator.GetComponent<FirstBossMinionScript>().bossObject;

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
        timer += Time.deltaTime;
        if(timer > randTimer)
        {
            float randSpeed = Random.Range(-1000, 1000) / 100;
            randTimer = Random.Range(2, 6);
            timer = 0f;
        }

        if (Vector2.Distance(boss.transform.position, animator.transform.position) < bossSize + 1f || Vector2.Distance(boss.transform.position, animator.transform.position) > bossSize + 1f)
        {
            animator.SetTrigger("Start");
        }

        animator.transform.GetComponent<FirstBossMinionScript>().Pivot(randSpeed);
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
