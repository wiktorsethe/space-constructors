using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TextScript : MonoBehaviour
{
    public Text unvisibleText;
    public TMP_Text visibleText;
    void Update()
    {
        visibleText.text = String.Format("{0:C}", unvisibleText.text);
    }
}
