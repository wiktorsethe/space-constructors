using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShipManager : MonoBehaviour
{
    [Header("Other Scripts")]
    private Menu menu;
    private CameraSize camSize;
    private Shooting shooting;
    [Space(20f)]

    [Header("GameObjects")]
    private GameObject ship;
    public GameObject activeConstructPoint;
    private GameObject player;

    private void Start()
    {
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        shooting = GameObject.FindObjectOfType(typeof(Shooting)) as Shooting;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void NewPart(int index)
    {
        ship = GameObject.Find("SHIP");
        GameObject shipPart = Instantiate(activeConstructPoint.GetComponent<ShipPart>().shipPrefabList[index], activeConstructPoint.transform.position, Quaternion.identity);
        shipPart.transform.parent = ship.transform;
        Transform child = shipPart.transform.Find("ShootingPoint");
        if(child != null)
        {
            shooting.AddToList(child);
        }
        menu.constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        for (int i = 0; i < menu.constructPoints.Length; i++)
        {
            if (menu.constructPoints[i].transform.position == activeConstructPoint.transform.position)
            {
                Destroy(menu.constructPoints[i]);
            }
        }
        Destroy(activeConstructPoint);
        menu.ExitConstructMenu();
        camSize.ChangeCamSize();
    }
    public void RotateToCenter()
    {
        Vector3 targetRotation = Vector3.zero;
        transform.DORotate(targetRotation, 0.6f).SetUpdate(UpdateType.Normal, true);
    }
    public void MoveObj()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x + 3f, player.transform.position.y, player.transform.position.z);
        transform.DOMove(targetPosition, 1f).SetUpdate(UpdateType.Normal, true);
    }
}
