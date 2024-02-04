using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashStartState : StateMachineBehaviour
{
    private GameObject ship;
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyShip>().FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku
        animator.transform.GetComponent<EnemyShip>().retreatVector = Vector2.zero; // Reset wektora statku wroga do ataku
        speed = animator.transform.GetComponent<EnemyShip>().moveSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Poruszanie sie w kierunku celu
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 15f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, ship.transform.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(animator.transform.position, ship.transform.position) <= 15f)
        {
            // Ustawienie pozycji, któr¹ wrogi statek bêdzie atakowa³
            animator.transform.GetComponent<EnemyShip>().savedPos = ship.transform.position;
            animator.SetTrigger("Attack");
        }
    }
}
