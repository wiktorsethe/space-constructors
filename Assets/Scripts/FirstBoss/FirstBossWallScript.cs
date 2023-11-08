using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossWallScript : MonoBehaviour
{
    private void Start()
    {
        float newWidth = CalculateTotalChildRendererWidthRecursive(GameObject.FindGameObjectWithTag("Player").transform) * 0.25f;
        transform.localScale = new Vector2(newWidth, newWidth/1.3f);
        //Debug.Log(newWidth);
        //ResizeToPlayerBoundsWithChildren();
    }
    private float CalculateTotalChildRendererWidthRecursive(Transform parent)
    {
        float totalWidth = 0f;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                totalWidth += bounds.size.x;
            }

            // If the child has more children, calculate their widths recursively
            if (child.childCount > 0)
            {
                totalWidth += CalculateTotalChildRendererWidthRecursive(child);
            }
        }

        return totalWidth;
    }
    public void ResizeToPlayerBoundsWithChildren()
    {
        // Get the bounds of the player object including its children renderers
        Bounds playerBounds = new Bounds(GameObject.FindGameObjectWithTag("Player").transform.position, Vector3.zero);
        Renderer[] playerRenderers = GameObject.FindGameObjectWithTag("Player").transform.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in playerRenderers)
        {
            playerBounds.Encapsulate(renderer.bounds);
        }

        // Set the scale of the other object to match the bounds of the player object
        Vector3 newScale = playerBounds.size;
        transform.localScale = newScale;
    }
}
