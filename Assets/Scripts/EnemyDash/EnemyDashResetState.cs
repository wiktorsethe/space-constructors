using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashResetState : StateMachineBehaviour
{
    private float timer;
    private CameraShake camShake;
    private GameObject ship;
    private HpBar hpBar;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // Zresetowanie timera
        ship = animator.GetComponent<EnemyShip>().FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;

        // Sprawdzenie, czy uda³o siê trafiæ w gracza 
        if (Vector2.Distance(animator.transform.position, ship.transform.position) < 5f)
        {
            camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
            camShake.ShakeCamera(0.2f, 1f, 3); // Wywo³anie trzêsienia kamery
            animator.GetComponent<EnemyShip>().Dash();
            hpBar.SetHealth(5); // Zadanie obra¿eñ  
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        // Sprawdzenie, czy timer przekroczy³ 2 sekundy
        if (timer > 2f)
        {
            animator.SetTrigger("Retreat"); // Przygotowanie do nastêpnego ataku
        }
    }
}
