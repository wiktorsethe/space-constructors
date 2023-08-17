using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject[] prefabs;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            InstantiateObject(i);
        }
    }

    public GameObject GetPooledObject(int i)
    {
        bool active = false;
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeSelf)
            {
                goto Next;
            }
            else if (obj.activeSelf)
            {
                active = true;
            }
        }

        if (active)
        {
            InstantiateObject(i);
        }

        Next:
        for (int j=0; i<pooledObjects.Count; j++)
        {
            if (!pooledObjects[j].activeInHierarchy)
            {
                return pooledObjects[j];
            }
        }


        return null;
    }
    private void InstantiateObject(int i)
    {
        GameObject obj = Instantiate(prefabs[i]);
        obj.SetActive(false);
        pooledObjects.Add(obj);
    }
}
