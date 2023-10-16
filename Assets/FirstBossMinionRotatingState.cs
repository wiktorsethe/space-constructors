using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossMinionRotatingState : StateMachineBehaviour
{
    private float posTimer = 0;
    private float healTimer = 0;
    private float randTimer;
    private float randSpeed;
    private GameObject player;
    private float playerSize;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Find("EnemyShipImage").transform.GetComponent<PolygonCollider2D>().enabled = true;
        posTimer = 0;
        healTimer = 0;
        randTimer = Random.Range(0, 5);
        randSpeed = Random.Range(-1000, 1000) / 10;

        player = GameObject.FindGameObjectWithTag("Player");

        Renderer[] bossRenderers = player.GetComponentsInChildren<Renderer>();
        if (bossRenderers.Length > 0)
        {
            Bounds combinedBounds = bossRenderers[0].bounds;
            for (int i = 1; i < bossRenderers.Length; i++)
            {
                combinedBounds.Encapsulate(bossRenderers[i].bounds);
            }
            Vector3 size = combinedBounds.size;
            playerSize = size.magnitude;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        posTimer += Time.deltaTime;
        healTimer += Time.deltaTime;
        if(posTimer > randTimer)
        {
            randSpeed = Random.Range(-1000, 1000) / 10;
            randTimer = Random.Range(2, 6);
            posTimer = 0f;
        }

        if (healTimer > 4f)
        {
            animator.transform.GetComponent<FirstBossMinionScript>().HealBoss();
            healTimer = 0f;
        }

        if (Vector2.Distance(animator.transform.position, player.transform.position) > playerSize + 2f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position, 4f * Time.deltaTime);
        }
        else if (Vector2.Distance(animator.transform.position, player.transform.position) < playerSize - 2f)
        {
            Vector3 direction = animator.transform.position - player.transform.position;
            direction.Normalize();

            animator.transform.Translate(direction * 14f * Time.deltaTime);
            //animator.transform.position = Vector2.MoveTowards(animator.transform.position, direction, 14f * Time.deltaTime);
        }
        else
        {
            animator.transform.GetComponent<FirstBossMinionScript>().Pivot(randSpeed);
        }

        /*
        if (Vector2.Distance(boss.transform.position, animator.transform.position) < bossSize + 1f || Vector2.Distance(boss.transform.position, animator.transform.position) > bossSize + 1f)
        {
            Vector3 direction = animator.transform.position - boss.transform.position;
            direction.Normalize();

            animator.transform.Translate(direction * 14f * Time.deltaTime);
        }

        animator.transform.GetComponent<FirstBossMinionScript>().Pivot(randSpeed);
        */
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
