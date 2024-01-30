using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ForgeManSlider : MonoBehaviour
{
    public ShipPartsDatabase shipPartsDB;
    public int shipPartNumber;
    [SerializeField] public TMP_Text amountText;
    [SerializeField] public Slider sliderBar;
    [SerializeField] private AudioSource buttonSound;
    private void Start()
    {
        PlayerPrefs.SetInt("BossPoints", 10);//do usuniêcia
        buttonSound = GameObject.Find("Button").GetComponent<AudioSource>();
        if (shipPartsDB.shipParts[shipPartNumber].isOwned)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().onClick.AddListener(() => AddToForge(shipPartNumber));
        }
        SetSliderValue();
    }
    private void AddToForge(int shipPartNumber)
    {
        buttonSound.Play();
        int bossPoints = PlayerPrefs.GetInt("BossPoints");
        if (bossPoints > 0)
        {
            PlayerPrefs.SetInt("BossPoints", bossPoints -= 1); //dodaæ bosspoint po zabiciu bossa
            //Debug.Log(bossPoints);
            shipPartsDB.shipParts[shipPartNumber].produceAmount++;
            SetSliderValue();
            if(shipPartsDB.shipParts[shipPartNumber].produceAmount >= shipPartsDB.shipParts[shipPartNumber].neededToProduceAmount)
            {
                shipPartsDB.shipParts[shipPartNumber].isOwned = true;
                GetComponent<Button>().interactable = false;
            }
        }
  
    }
    public void SetSliderValue()
    {
        sliderBar.maxValue = shipPartsDB.shipParts[shipPartNumber].neededToProduceAmount;
        sliderBar.value = shipPartsDB.shipParts[shipPartNumber].produceAmount;
        StartCoroutine(AnimateNumberIteration((int)sliderBar.value, (int)shipPartsDB.shipParts[shipPartNumber].produceAmount));
    }
    private IEnumerator AnimateNumberIteration(int startNumber, int targetNumber)
    {
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            float t = (Time.time - startTime) / 1f;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startNumber, targetNumber, t));
            amountText.text = shipPartsDB.shipParts[shipPartNumber].produceAmount.ToString() + " / " + shipPartsDB.shipParts[shipPartNumber].neededToProduceAmount.ToString();

            yield return null;
        }
        amountText.text = shipPartsDB.shipParts[shipPartNumber].produceAmount.ToString() + " / " + shipPartsDB.shipParts[shipPartNumber].neededToProduceAmount.ToString();
    }
}
