using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashRetreatState : StateMachineBehaviour
{
    private GameObject ship;
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = animator.GetComponent<EnemyShip>().FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku
        animator.transform.GetComponent<EnemyShip>().retreatVector = animator.transform.position - ship.transform.position; // Obliczenie odpowiedniego wektora statku wroga do ataku
        animator.transform.GetComponent<EnemyShip>().retreatVector.Normalize(); // Normalizacja ruchu dla jednostkowej d³ugoœci
        speed = animator.transform.GetComponent<EnemyShip>().moveSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Przesuwanie pozycji wroga w kierunku obliczonego wektora z zadan¹ prêdkoœci¹
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, animator.transform.GetComponent<EnemyShip>().retreatVector, speed * Time.deltaTime);

        // Sprawdzenie, czy odleg³oœæ miêdzy wrogiem a celem jest wiêksza ni¿ 12
        if (Vector2.Distance(animator.transform.position, ship.transform.position) > 12f)
        {
            animator.SetTrigger("Start"); // Powtórzenie ataku
        }
    }
}
