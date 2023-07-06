using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(transform.parent.tag == "EnemyRanger")
            {
                transform.parent.GetComponent<EnemyRanger>().CollisionDetected();
            }
            if (transform.parent.tag == "EnemyWarrior")
            {
                transform.parent.GetComponent<EnemyWarrior>().CollisionDetected();
            }
            if (transform.parent.tag == "EnemyShaman")
            {
                transform.parent.GetComponent<EnemyShaman>().CollisionDetected();
            }
            if (transform.parent.tag == "EnemyShip")
            {
                transform.parent.GetComponent<EnemyShip>().CollisionDetected();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.parent.tag == "EnemyWarrior")
        {
            if (collision.transform.tag == "Player")
            {
                transform.parent.GetComponent<EnemyWarrior>().attackTimer = 0f;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (transform.parent.tag == "EnemyWarrior")
        {
            if (collision.transform.tag == "Player" && transform.parent.GetComponent<EnemyWarrior>().attackTimer > 2f)
            {

                transform.parent.GetComponent<EnemyWarrior>().hpBar.SetHealth(5f);
                transform.parent.GetComponent<EnemyWarrior>().attackTimer = 0f;
            }
        }
    }

}
