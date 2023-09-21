using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseScaler : MonoBehaviour
{
    private GameObject player;

    public void ChangeUniverseScale()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Bounds bounds = Bounds();

        Vector3 newScale = transform.localScale * (bounds.size.magnitude / 50f);
        transform.localScale = newScale;
    }
    private Bounds Bounds()
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }

        return new Bounds(Vector3.zero, Vector3.zero);
    }
}
