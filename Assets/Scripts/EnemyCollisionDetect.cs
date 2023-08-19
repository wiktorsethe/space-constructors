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
                if(collision.GetComponent<ShootingBullet>().type == "PoisonBullet")
                {
                    transform.parent.GetComponent<EnemyShip>().StartPoison();

                }
                if (collision.GetComponent<ShootingBullet>().type == "NormalBullet" || collision.GetComponent<ShootingBullet>().type == "HomingBullet")
                {
                    transform.parent.GetComponent<EnemyShip>().CollisionDetected((int)collision.GetComponent<ShootingBullet>().damage);
                }
                if(collision.GetComponent<ShootingBullet>().type == "FlameBullet")
                {
                    transform.parent.GetComponent<EnemyShip>().StartFlame();
                }
                if(collision.GetComponent<ShootingBullet>().type == "StunningBullet")
                {
                    transform.parent.GetComponent<EnemyShip>().moveSpeed = 0f;
                }
            }
        }
    }
}
