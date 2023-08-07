using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ShipManager : MonoBehaviour
{
    [Header("Other Scripts")]
    private Menu menu;
    private CameraSize camSize;
    public PlayerStats playerStats;
    public ShipPartsDatabase shipPartsDB;
    public ShipProgress shipProgress;
    [Space(20f)]

    [Header("GameObjects")]
    private GameObject ship;
    public GameObject activeConstructPoint;
    private GameObject player;
    [SerializeField] private TMP_Text gravityWarningText;
    [SerializeField] private GameObject[] potentialSpawnPoints;

    private float distanceThreshold = 1000f;
    [SerializeField] private GameObject arrowPrefab;
    public GameObject arrow;
    [SerializeField] private float arrowDistance = 2f;
    private bool animate = false;
    private float choosenDistance = 9999999;
    private GameObject choosenTarget;
    private void Start()
    {
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        player = GameObject.FindGameObjectWithTag("Player");
        ship = GameObject.Find("SHIP"/*(Clone)"*/);
        if (shipProgress.shipParts.Count > 0)
        {
            for(int i=0; i<shipProgress.shipParts.Count; i++)
            {
                GameObject shipPart = Instantiate(shipPartsDB.shipParts[shipProgress.shipParts[i].shipPartIndex].shipPart, shipProgress.shipParts[i].position, shipProgress.shipParts[i].rotation);
                shipPart.transform.parent = ship.transform;
            }

            GameObject[] targets = GameObject.FindGameObjectsWithTag("ConstructPoint");
            for (int i = 0; i < targets.Length; i++)
            {
                foreach (Vector3 shipPartPos in shipProgress.usedContstructPoints)
                {
                    if (targets[i].transform.position == shipPartPos)
                    {
                        Destroy(targets[i].gameObject);
                    }
                }
            }
        }
        ship.gameObject.transform.position = playerStats.shipPosition;
        menu.HideConstructPoints();
        if(playerStats.shipPosition == new Vector3(0f, 0f, 0f))
        {
            int randIndex = Random.Range(0, potentialSpawnPoints.Length);
            transform.position = potentialSpawnPoints[randIndex].transform.position;
        }
    }
    private void Update()
    {
        if(playerStats.shipCurrentHealth <= 0)
        {
            menu.GameOver();
            Destroy(gameObject);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject target in targets)
        {
            if(choosenDistance > Vector3.Distance(target.transform.position, transform.position))
            {
                choosenDistance = Vector3.Distance(target.transform.position, transform.position);
                choosenTarget = target;
            }
        }

        if (Vector3.Distance(choosenTarget.transform.position, transform.position) <= distanceThreshold)
        {
            Vector3 direction = (choosenTarget.transform.position - transform.position).normalized;
            //Debug.DrawRay(transform.position, direction * 10f, Color.red);
            if (!arrow)
            {
                arrow = Instantiate(arrowPrefab, transform);
            }
            arrow.transform.position = transform.position + direction * arrowDistance;

            Vector3 targetDir = direction;
            targetDir.z = 0f;
            arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDir);
            if (!animate)
            {
                AnimateGravityWarningText();
                animate = true;
            }


            if (playerStats.shipGravity > choosenTarget.GetComponent<Teleport>().gravity + 10)
            {
                Color col = new Color(0.3f, 1f, 0);
                arrow.GetComponent<SpriteRenderer>().color = col;
            }
            else if (playerStats.shipGravity <= choosenTarget.GetComponent<Teleport>().gravity + 10 && playerStats.shipGravity == choosenTarget.GetComponent<Teleport>().gravity)
            {
                Color col = new Color(1f, 0.7f, 0);
                arrow.GetComponent<SpriteRenderer>().color = col;
            }
            else if (playerStats.shipGravity < choosenTarget.GetComponent<Teleport>().gravity)
            {
                Color col = new Color(1f, 0.1f, 0);
                arrow.GetComponent<SpriteRenderer>().color = col;
            }
        }
        else if (Vector3.Distance(choosenTarget.transform.position, transform.position) > distanceThreshold && arrow != null)
        {
            Destroy(arrow);
            arrow = null;
        }
    }
    public void NewPart(int index)
    {
        if(playerStats.ore >= shipPartsDB.shipParts[index].oreCost && playerStats.screw >= shipPartsDB.shipParts[index].screwCost) //tu dodac te srubki
        {
            ship = GameObject.Find("SHIP"/*(Clone)"*/); // tu usunac clone jak nie dziala
            GameObject shipPart = Instantiate(shipPartsDB.shipParts[index].shipPart, activeConstructPoint.transform.position, activeConstructPoint.transform.rotation);
            shipPart.transform.parent = ship.transform;
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
            playerStats.ore -= shipPartsDB.shipParts[index].oreCost;
            playerStats.screw -= shipPartsDB.shipParts[index].screwCost;
            shipPartsDB.shipParts[index].amount--;
            playerStats.shipGravity += shipPartsDB.shipParts[index].gravityBonus;
            ShipPartInScene newShipPart;
            newShipPart.shipPartIndex = index;
            newShipPart.position = shipPart.transform.localPosition;
            newShipPart.rotation = activeConstructPoint.transform.rotation;
            shipProgress.shipParts.Add(newShipPart);
            shipProgress.usedContstructPoints.Add(new Vector3(activeConstructPoint.transform.position.x - 3f, activeConstructPoint.transform.position.y, activeConstructPoint.transform.position.z));
        }

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
    public void MoveObjBack()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x - 3f, player.transform.position.y, player.transform.position.z);
        transform.DOMove(targetPosition, 1f).SetUpdate(UpdateType.Normal, true);
    }
    public void AnimateGravityWarningText()
    {
        gravityWarningText.DOFade(1f, 1f).From(0f).OnComplete(() =>
        {
            gravityWarningText.DOFade(0f, 1f).OnComplete(() => { animate = false; });
        });
    }


}
