using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance { get { return instance; } }

    private List<GameObject> pool;

    public GameObject projectile;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        pool = new List<GameObject>();
    }

    public GameObject GetProjectile()
    {
        if (pool.Count > 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
        }

        GameObject projectileGO = Instantiate(projectile);
        projectileGO.SetActive(false);
        pool.Add(projectileGO);
        return projectileGO;
    }
}
