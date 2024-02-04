using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlameAttackState : StateMachineBehaviour
{
    private GameObject ship;
    private float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyShip>().FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku
        timer = 0f; // Zresetowanie timera
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        // Sprawdzenie, czy cel nie uciek³
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 10f)
        {
            animator.SetTrigger("Start");
        }
        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 10f)
        {
            // Atak celu co 5 sekund
            if (timer >= 5f)
            {
                animator.transform.GetComponent<EnemyShip>().FireBullet(); // Wywo³anie funkcji Strza³u
                timer = 0f; // Zresetowanie timera
            }
        }
    }
}
