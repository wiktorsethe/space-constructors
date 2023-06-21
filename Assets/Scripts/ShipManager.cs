using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private GameObject ship;
    public GameObject activeConstructPoint;
    private Menu menu;

    private void Start()
    {
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
    }
    public void NewPart(int index)
    {
        ship = GameObject.Find("SHIP");
        GameObject shipPart = Instantiate(activeConstructPoint.GetComponent<ShipPart>().shipPrefabList[index], activeConstructPoint.transform.position, Quaternion.identity);
        shipPart.transform.parent = ship.transform;
        Destroy(activeConstructPoint);
        menu.ExitConstructMenu();
    }
}
