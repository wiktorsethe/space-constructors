using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 2, 0);
    private Vector3 randOffset;
    private void Start()
    {
        Destroy(gameObject, 1f);
        transform.localPosition += offset;
        randOffset = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(0.01f, 0.03f), 0);
    }
    private void Update()
    {
        transform.localPosition += randOffset;
    }
}
