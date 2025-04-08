using UnityEngine;
using System.Collections.Generic;

public class HazardPoolSpawner : MonoBehaviour
{
    public GameObject[] hazardPrefabs;
    public int poolSize = 20;
    public float spawnRadius = 15f;
    public float maxDistanceFromPlayer = 25f;
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.8f;
    public float difficultyIncreaseRate = 0.05f;
    public Transform player;

    private List<GameObject> pool = new List<GameObject>();
    private float currentInterval;
    private float timer;

    void Start()
    {
        currentInterval = initialSpawnInterval;
        timer = currentInterval;

        // Llenar el pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = hazardPrefabs[i % hazardPrefabs.Length];
            GameObject hazard = Instantiate(prefab, transform);
            hazard.SetActive(false);
            pool.Add(hazard);
        }
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnHazard();
            currentInterval = Mathf.Max(minSpawnInterval, currentInterval - difficultyIncreaseRate);
            timer = currentInterval;
        }

        // Revisar qué hazards están muy lejos del jugador
        foreach (GameObject hazard in pool)
        {
            if (hazard.activeInHierarchy && Vector3.Distance(player.position, hazard.transform.position) > maxDistanceFromPlayer)
            {
                hazard.SetActive(false);
            }
        }
    }

    void SpawnHazard()
    {
        GameObject hazard = GetAvailableHazard();
        if (hazard != null)
        {
            Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            hazard.transform.position = spawnPos;
            hazard.SetActive(true);
        }
    }

    GameObject GetAvailableHazard()
    {
        foreach (GameObject hazard in pool)
        {
            if (!hazard.activeInHierarchy)
                return hazard;
        }
        return null; // No hay objetos disponibles
    }
}

