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
    public SkinsDatabase skinsDB;
    private UniverseMaxSize uniMaxSize;
    [Space(20f)]

    [Header("GameObjects")]
    private GameObject ship;
    public GameObject activeConstructPoint;
    private GameObject player;
    [SerializeField] private CanvasGroup gravityWarning;
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
        uniMaxSize = GameObject.FindObjectOfType(typeof(UniverseMaxSize)) as UniverseMaxSize;
        player = GameObject.FindGameObjectWithTag("Player");
        ship = GameObject.Find("SHIP"/*(Clone)"*/);
        ship.transform.Find("Ship").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteMain;
        ship.transform.Find("NormalGun").transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteNormalGun;
        ship.transform.Find("NormalGun").transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorNormalGun;
        if (shipProgress.shipParts.Count > 0)
        {
            for(int i=0; i<shipProgress.shipParts.Count; i++)
            {
                GameObject shipPart = Instantiate(shipPartsDB.shipParts[shipProgress.shipParts[i].shipPartIndex].shipPart, shipProgress.shipParts[i].position, shipProgress.shipParts[i].rotation);
                shipPart.transform.parent = ship.transform;
                if (shipProgress.shipParts[i].shipPartIndex == 0)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteCorridor;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 1)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteMain;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 2)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteNormalGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorNormalGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 3)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteLaserGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorLaserGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 4)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteBigGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorBigGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 5)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteDoubleGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorDoubleGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 6)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteHealFragment;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorHealFragment;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 7)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteFlameGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorFlameGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 8)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpritePoisonGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorPoisonGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 9)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteStunningGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorStunningGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 10)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteBombGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorBombGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 11)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteDashFragment;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorDashFragment;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 12)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteHommingGun;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorHommingGun;
                }
                else if (shipProgress.shipParts[i].shipPartIndex == 13)
                {
                    shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteShieldFragment;
                    shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorShieldFragment;
                }
            }

            GameObject[] targets = GameObject.FindGameObjectsWithTag("ConstructPoint");
            for (int i = 0; i < targets.Length; i++)
            {
                foreach (Vector2 shipPartPos in shipProgress.usedContstructPoints)
                {
                    //Debug.Log(shipPartPos + ", " + targets[i].transform.position);
                    if ((Vector2)targets[i].transform.position == shipPartPos || Vector2.Distance((Vector2)targets[i].transform.position, shipPartPos) < 0.01f)
                    {
                        Destroy(targets[i].gameObject);
                    }
                }
            }
        }
        Invoke("StartingPos", 0.5f);
        menu.HideConstructPoints();
        gravityWarning.alpha = 0;
    }
    private void Update()
    {
        if(playerStats.shipCurrentHealth <= 0)
        {
            menu.GameOver();
            uniMaxSize.enabled = false;
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

        if(targets.Length > 0)
        {
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
                if (!animate && choosenTarget.GetComponent<Teleport>().gravity > playerStats.shipGravity)
                {
                    AnimateGravityWarning();
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
        
    }
    private void StartingPos()
    {
        ship.gameObject.transform.position = playerStats.shipPosition; /// <--------
        if (playerStats.shipPosition == new Vector3(0f, 0f, 0f))
        {
            int randIndex = Random.Range(0, potentialSpawnPoints.Length);
            transform.position = potentialSpawnPoints[randIndex].transform.position;
        }
    }
    public void NewPart(int index)
    {
        if(playerStats.ore >= shipPartsDB.shipParts[index].oreCost && playerStats.screw >= shipPartsDB.shipParts[index].screwCost) //tu dodac te srubki
        {
            ship = GameObject.Find("SHIP"/*(Clone)"*/); // tu usunac clone jak nie dziala
            GameObject shipPart = Instantiate(shipPartsDB.shipParts[index].shipPart, activeConstructPoint.transform.position, activeConstructPoint.transform.rotation);
            shipPart.transform.parent = ship.transform;
            if(index == 0)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteCorridor;
            }
            else if (index == 1)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteMain;
            }
            else if (index == 2)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteNormalGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorNormalGun;
            }
            else if (index == 3)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteLaserGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorLaserGun;
            }
            else if (index == 4)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteBigGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorBigGun;
            }
            else if (index == 5)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteDoubleGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorDoubleGun;
            }
            else if (index == 6)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteHealFragment;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorHealFragment;
            }
            else if (index == 7)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteFlameGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorFlameGun;
            }
            else if (index == 8)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpritePoisonGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorPoisonGun;
            }
            else if (index == 9)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteStunningGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorStunningGun;
            }
            else if (index == 10)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteBombGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorBombGun;
            }
            else if (index == 11)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteDashFragment;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorDashFragment;
            }
            else if (index == 12)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteHommingGun;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorHommingGun;
            }
            else if (index == 13)
            {
                shipPart.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = skinsDB.skins[playerStats.selectedSkin].skinSpriteShieldFragment;
                shipPart.transform.Find("Skin").GetComponent<Animator>().runtimeAnimatorController = skinsDB.skins[playerStats.selectedSkin].animatorShieldFragment;
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
            //camSize.ChangeCamSize();
            playerStats.ore -= shipPartsDB.shipParts[index].oreCost;
            playerStats.screw -= shipPartsDB.shipParts[index].screwCost;
            shipPartsDB.shipParts[index].ownedAmount--;
            playerStats.shipGravity += shipPartsDB.shipParts[index].gravityBonus;
            playerStats.shipMaxHealth += shipPartsDB.shipParts[index].hpBonus;
            ShipPartInScene newShipPart;
            newShipPart.shipPartIndex = index;
            newShipPart.position = shipPart.transform.localPosition;
            newShipPart.rotation = activeConstructPoint.transform.rotation;
            shipProgress.shipParts.Add(newShipPart);
            shipProgress.usedContstructPoints.Add(new Vector2(newShipPart.position.x, newShipPart.position.y));
            
        }

    }
    public void RotateToCenter()
    {
        Vector3 targetRotation = Vector3.zero;
        transform.DORotate(targetRotation, 0.6f).SetUpdate(UpdateType.Normal, true);
    }
    /*
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
    */
    public void AnimateGravityWarning()
    {
        gravityWarning.DOFade(1f, 1f).From(0f).OnComplete(() =>
        {
            gravityWarning.DOFade(0f, 1f).OnComplete(() => { animate = false; gravityWarning.alpha = 0; });
        });
    }
}
