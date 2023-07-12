using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectLoot : MonoBehaviour
{
    [Header("Other Scripts")]
    public ShipPartsDatabase shipPartsDB;
    [Space(20f)]

    [Header("Other GameObjects")]
    private GameObject player;
    [SerializeField] private GameObject miningTextPrefab;
    [Space(20f)]

    [Header("Other")]
    public string lootName;
    public float speed = 2f;
    float timer = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

        Vector3 reverseDirection = transform.position - player.transform.position;
        reverseDirection.Normalize();
        timer += Time.deltaTime;
        if (timer <= 1f)
        {
            transform.position += new Vector3(reverseDirection.x, reverseDirection.y, 0f) * speed * Time.deltaTime;
            speed -= 0.1f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Terrain")
                {
                    Vector3 dirToTarget = transform.position - colliders[i].transform.position;
                    transform.position += new Vector3(dirToTarget.x, dirToTarget.y, 0f) * speed * Time.deltaTime;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (lootName == "Corridor")
            {
                for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
                {
                    if (shipPartsDB.shipParts[i].shipPartType == "corridor")
                    {
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Corridor");
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
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Main");
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
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Normal Gun");
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
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Laser Gun");
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
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Double Gun");
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
                        shipPartsDB.shipParts[i].amount++;
                        ShowMiningText(1, "Big Gun");
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void ShowMiningText(int amount, string type)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+ " + amount.ToString() + " " + type;
    }
}
