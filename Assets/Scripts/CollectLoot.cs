using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CollectLoot : MonoBehaviour
{
    [Header("Other Scripts")]
    public ShipPartsDatabase shipPartsDB;
    public PlayerStats playerStats;
    private GameManager gameManager;
    [Space(20f)]

    [Header("Other GameObjects")]
    private GameObject player;
    [SerializeField] private GameObject textPrefab;
    [Space(20f)]

    [Header("Other")]
    public string lootName;
    private float speed = 2f;
    private float timer = 0f;

    private void Start()
    {
        // ZnajdŸ gracza i GameManagera na scenie
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }
    private void Update()
    {
        // Obliczanie kierunku przeciwnego do gracza i poruszanie siê w tym kierunku
        Vector3 reverseDirection = transform.position - player.transform.position;
        reverseDirection.Normalize();
        timer += Time.deltaTime;
        if (timer <= 0.5f)
        {
            transform.position += new Vector3(reverseDirection.x, reverseDirection.y, 0f) * speed * Time.deltaTime;
            speed -= 0.1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Sprawdzanie nazwy ³upu i dodawanie odpowiedniej czêœci do bazy danych statku
            if (lootName == "Corridor")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "corridor")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Corridor");
                    }
                    Destroy(this.gameObject);
                }
            }
            if (lootName == "Main")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "main")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Main");
                    }
                    Destroy(this.gameObject);
                }
            }
            if (lootName == "NormalGun")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "normalGun")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Normal Gun");
                    }
                    Destroy(this.gameObject);
                }
            }
            if (lootName == "LaserGun")
            {
                for(int i=0; i<shipPartsDB.shipParts.Length; i++)
                {
                    if(shipPartsDB.shipParts[i].shipPartType == "laserGun")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Laser Gun");
                    }
                    Destroy(this.gameObject);
                }
            }
            if (lootName == "DoubleGun")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "doubleGun")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Double Gun");
                    }
                    Destroy(this.gameObject);
                }
            }
            if (lootName == "BigGun")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "bigGun")
                    {
                        shipPartsDB.shipParts[i].ownedAmount++;
                        ShowText(1, "Big Gun");
                    }
                    Destroy(this.gameObject);
                }
            }
            
        }
        // Przeniesienie statku do Mysterious Place po zebraniu Orb z bossa
        if (other.gameObject.tag == "Ship")
        {
            if (lootName == "Orb")
            {
                ShowText(1, "Orb");
                playerStats.shipPosition = transform.position;
                PlayerPrefs.SetFloat("BestTimeTimer", gameManager.bestTimeTimer);
                PlayerPrefs.SetInt("Kills", gameManager.kills);
                PlayerPrefs.SetInt("GoldEarned", gameManager.goldEarned);
                SceneManager.LoadScene("MisteriousPlace");
                Destroy(this.gameObject);
            }
        }
    }
    private void ShowText(int amount, string type)
    {
        // Wyœwietlenie tekstu informuj¹cego o zebraniu ³upu
        var text = Instantiate(textPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+ " + amount.ToString() + " " + type;
    }
}
