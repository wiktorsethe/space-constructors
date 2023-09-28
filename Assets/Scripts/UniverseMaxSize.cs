using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseMaxSize : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float maxDistance;
    private bool isPosSaved = false;
    private Vector2 playerPos;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) > maxDistance - 10 && maxDistance > 0 && !isPosSaved)
        {
            playerPos = player.transform.position;
            isPosSaved = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) > maxDistance && maxDistance > 0 && isPosSaved)
        {
            player.transform.position = new Vector2(-playerPos.x, -playerPos.y);
            isPosSaved = false;
        }
    }
    public void ActivateCortoutine()
    {
        StartCoroutine("ChangeMaxSize");
    }
    private IEnumerator ChangeMaxSize()
    {
        yield return new WaitForSeconds(0.1f);

        Bounds bounds = Bounds();

        float newScale = 5000f * (1 + bounds.size.magnitude / 400f);
        maxDistance = newScale;
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
