using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPoint : MonoBehaviour
{
    [Header("Other Scripts")]
    private Menu menu;
    private ShipManager shipManager;
    [Space(20f)]

    [Header("List")]
    public List<GameObject> shipPrefabList = new List<GameObject>();

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
