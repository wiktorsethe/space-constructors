using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ShipManager : MonoBehaviour
{
    [Header("Other Scripts")]
    private Menu menu;
    private CameraSize camSize;
    private Shooting shooting;
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("GameObjects")]
    private GameObject ship;
    public GameObject activeConstructPoint;
    private GameObject player;
    public TMP_Text gravityWarningText;

    public string targetTag;
    public float distanceThreshold = 10f;
    public GameObject arrowPrefab;
    private GameObject arrow;
    public float arrowDistance = 2f;
    private bool animate = false;
    private void Start()
    {
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        shooting = GameObject.FindObjectOfType(typeof(Shooting)) as Shooting;
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        if(playerStats.currentHealth <= 0)
        {
            menu.GameOver();
            Destroy(gameObject);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag); 

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position); 

            if (distance <= distanceThreshold)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
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


                if (playerStats.gravity > target.GetComponent<Teleport>().gravity + 10)
                {
                    Color col = new Color(0.3f, 1f, 0);
                    arrow.GetComponent<SpriteRenderer>().color = col;
                }
                else if(playerStats.gravity <= target.GetComponent<Teleport>().gravity + 10 && playerStats.gravity == target.GetComponent<Teleport>().gravity)
                {
                    Color col = new Color(1f, 0.7f, 0);
                    arrow.GetComponent<SpriteRenderer>().color = col;
                }
                else if(playerStats.gravity < target.GetComponent<Teleport>().gravity)
                {
                    Color col = new Color(1f, 0.1f, 0);
                    arrow.GetComponent<SpriteRenderer>().color = col;
                }
            }
            else if (arrow != null)
            {
                Destroy(arrow);
                arrow = null;
            }
        }
    }
    public void NewPart(int index)
    {
        if(playerStats.gold >= activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList[index].GetComponent<ShipPart>().cost)
        {
            ship = GameObject.Find("SHIP");
            GameObject shipPart = Instantiate(activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList[index], activeConstructPoint.transform.position, activeConstructPoint.transform.rotation);
            shipPart.transform.parent = ship.transform;
            Transform child = shipPart.transform.Find("ShootingPoint");
            if (child != null)
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
            playerStats.gold -= activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList[index].GetComponent<ShipPart>().cost;
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
