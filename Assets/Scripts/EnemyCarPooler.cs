using UnityEngine;
using System.Collections.Generic;

public class EnemyCarPooler : MonoBehaviour
{
    public static EnemyCarPooler Instance;

    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs; // Prefabs distintos
    public int poolSizePerPrefab = 5;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        foreach (var prefab in enemyPrefabs)
        {
            for (int i = 0; i < poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPooledEnemy()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        return null;
    }



}
