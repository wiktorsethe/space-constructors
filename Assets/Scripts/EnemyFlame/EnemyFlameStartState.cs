using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlameStartState : StateMachineBehaviour
{
    private GameObject ship;
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyShip>().FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku
        speed = animator.transform.GetComponent<EnemyShip>().moveSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Poruszanie sie w kierunku celu
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 10f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, speed * Time.deltaTime);
        }
        //Zmiana stanu na atak celu
        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 10f)
        {
            animator.SetTrigger("Attack");
        }
    }
}
