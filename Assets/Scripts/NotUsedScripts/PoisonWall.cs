using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWall : MonoBehaviour
{
    private HpBar hpBar;
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ship")
        {
            hpBar.StartPoison();
        }
    }
}
