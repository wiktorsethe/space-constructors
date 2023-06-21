using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public List<GameObject> shipPrefabList = new List<GameObject>();
    private Menu menu;
    private ShipManager shipManager;
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
