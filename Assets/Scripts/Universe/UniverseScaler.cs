using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseScaler : MonoBehaviour
{
    private GameObject player;

    public void ActivateCortoutine()
    {
        StartCoroutine("ChangeUniverseScale");
    }
    private IEnumerator ChangeUniverseScale()
    {
        yield return new WaitForSeconds(0.1f);

        player = GameObject.FindGameObjectWithTag("Player");

        Bounds bounds = Bounds();

        Vector3 newScale = new Vector3(1, 1, 1) * (bounds.size.magnitude / 75f);
        transform.localScale = newScale;

        GameObject[] teleports = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject tp in teleports)
        {
            tp.GetComponent<Teleport>().ChangeAttractionSize();
        }
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
