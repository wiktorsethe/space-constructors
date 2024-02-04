using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Lista obiektów w puli
    public List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject prefab; // Prefabrykat obiektu do instancjonowania
    public string type; // Typ obiektu w puli (np. "bullet", "enemy", "explosion")
    void Start()
    {
        // Na starcie utwórz pocz¹tkow¹ pulê obiektów
        for (int i = 0; i < 1; i++)
        {
            InstantiateObject();
        }
    }

    // Metoda do pobierania obiektu z puli
    public GameObject GetPooledObject()
    {
        bool active = false;

        // SprawdŸ, czy istnieje nieaktywny obiekt w puli
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

        // Jeœli wszystkie obiekty s¹ aktywne, utwórz nowy obiekt
        if (active)
        {
            InstantiateObject();
        }

        Next:
        // ZnajdŸ i zwróæ pierwszy nieaktywny obiekt w puli
        for (int i=0; i<pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

         
        return null; // Zwróæ null, jeœli nie ma dostêpnego obiektu w puli
    }
    private void InstantiateObject()
    {
        GameObject bullet = Instantiate(prefab);
        bullet.SetActive(false); // Ustawienie obiektu jako nieaktywnego
        pooledObjects.Add(bullet); // Dodanie obiektu do puli
    }
}
