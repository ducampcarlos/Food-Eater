using UnityEngine;
using System.Collections.Generic;

public class HealthPickUpSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject pickupPrefab;
    public int poolSize = 5;
    public float spawnRadius = 15f;
    public float spawnInterval = 10f;

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pickupPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPickup();
            timer = 0f;
        }

        // Desactivar pickups que estén demasiado lejos
        foreach (GameObject obj in pool)
        {
            if (obj.activeInHierarchy && Vector3.Distance(obj.transform.position, player.position) > spawnRadius * 2f)
            {
                obj.SetActive(false);
            }
        }
    }

    void SpawnPickup()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
                obj.transform.position = spawnPos;
                obj.SetActive(true);
                break;
            }
        }
    }
}
