using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPoint : MonoBehaviour
{
    [Header("Other Scripts")]
    private Menu menu;
    private ShipManager shipManager;
    public string shipPartType;

    private void Start()
    {
        shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
    }
    public void ActiveConstructMenu()
    {
        shipManager.activeConstructPoint = gameObject;
        menu.ConstructMenu();

    }
}
