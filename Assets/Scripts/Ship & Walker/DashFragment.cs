using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFragment : MonoBehaviour
{
    public PlayerStats playerStats;
    private Menu menu;
    private ShipMovement shipMovement;
    [SerializeField] private Sprite buttonOn;
    [SerializeField] private Sprite buttonOff;
    private void Start()
    {
        menu = GameObject.FindObjectOfType<Menu>() as Menu;
        shipMovement = GameObject.FindObjectOfType<ShipMovement>() as ShipMovement;
        menu.dashButton.SetActive(true);
        menu.dashButton.GetComponent<Button>().onClick.AddListener(() => OnClickDash());
    }
    private void OnClickDash()
    {
        StartCoroutine(Dash());
    }
    public IEnumerator Dash()
    {
        menu.dashButton.GetComponent<Image>().sprite = buttonOff;
        shipMovement.isDashActivated = true;
        yield return new WaitForSeconds(1f);
        menu.dashButton.GetComponent<Image>().sprite = buttonOn;
        shipMovement.isDashActivated = false;

    }

}
