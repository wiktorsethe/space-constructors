using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShaman : MonoBehaviour
{
    private ExpBar expBar;
    private HpBar hpBar;
    public float health;
    public int experience; // ??
    public int gold;
    public GameManager gameManager;
    public PlayerStats playerStats;
    private GameObject player;
    private float spawnTimer = 0f;
    private float moveSpeed = 2f;
    public float inTarget = 7;
    public GameObject[] spawnPoints;
    public GameObject enemyWarriorPrefab;
    public List<GameObject> minions = new List<GameObject>();
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (health <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject);
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance >= (inTarget - 1f) && distance < 30f && spawnTimer >= 2.5f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(distance < inTarget)
        {
            if(minions.Count == 0)
            {
                SpawnMinions();
            }
        }
    }
    private void SpawnMinions()
    {
        for(int i=0; i<3; i++)
        {
            minions.Add(Instantiate(enemyWarriorPrefab, spawnPoints[i].transform.position, Quaternion.identity));
        }
        spawnTimer = 0f;
    }
}
