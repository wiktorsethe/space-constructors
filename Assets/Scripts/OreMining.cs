using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class OreMining : MonoBehaviour
{
    [Header("Other Objects")]
    private GameObject player;
    public PlayerStats playerStats;
    [SerializeField] private GameObject miningTextPrefab;
    [Space(20f)]

    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float distance = 6;
    private bool attacked = false;
    private float attackTimer = 0;


    private void Start()
    {
        // Ustawienie kamery dla Canvasu
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // Inicjalizacja referencji do obiektu gracza
        player = GameObject.FindGameObjectWithTag("Player");
        // Ustawienie pocz¹tkowej wartoœci zdrowia
        currentHealth = maxHealth;
        // Inicjalizacja paska zdrowia
        SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        // Sprawdzenie odleg³oœci miêdzy obiektem a graczem i ukrycie paska zdrowia, jeœli jest za daleko
        if (Vector2.Distance(transform.position, player.transform.position) > distance)
        {
            healthBarCanvas.SetActive(false);
        }
        // Aktualizacja koloru paska zdrowia na podstawie jego normalizowanej wartoœci
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        // Zwiêkszenie czasu od ostatniego ataku
        attackTimer += Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Sprawdzenie kolizji z graczem
        if (collision.gameObject.tag == "Player")
        {
            healthBarCanvas.SetActive(true); // Wyœwietlenie paska zdrowia
            // Sprawdzenie, czy up³yn¹³ odpowiedni czas od ostatniego ataku i czy obiekt nie zosta³ ju¿ zaatakowany
            if (attackTimer > 2f && !attacked)
            {
                attacked = true; // Ustawienie flagi na true, aby unikn¹æ wielokrotnego ataku w jednym cyklu
                currentHealth -= 10; // Zmniejszenie zdrowia obiektu
                SetHealth(); // Aktualizacja paska zdrowia 
                playerStats.ore += (int)(2 * playerStats.oreMiningBonusValue); // Dodanie rudy do statystyk gracza
                if (miningTextPrefab)
                {  
                    ShowMiningText((int)(2 * playerStats.oreMiningBonusValue)); // Wyœwietlenie tekstu informuj¹cego o wydobyciu rudy
                }
                // Zniszczenie obiektu po osi¹gniêciu zerowego zdrowia
                if (currentHealth <= 0)
                {
                    Destroy(gameObject, 1.5f);
                }
                attackTimer = 0f; // Zresetowanie licznika czasu od ostatniego ataku
            }
            // Reset flagi po up³ywie 2 sekund od ostatniego ataku
            if (attackTimer < 2f)
            {
                attacked = false;
            }
        }
    }
    // Metoda ustawiaj¹ca maksymaln¹ wartoœæ zdrowia na pasku
    private void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        fillBar.color = healthGradient.Evaluate(1f);
    }
    // Metoda aktualizuj¹ca wartoœæ zdrowia na pasku z efektem animacji
    private void SetHealth()
    {
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
    }
    // Metoda wyœwietlaj¹ca tekst informuj¹cy o iloœci wydobytej rudy
    private void ShowMiningText(int amount)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+ " + amount.ToString();
    }
}
