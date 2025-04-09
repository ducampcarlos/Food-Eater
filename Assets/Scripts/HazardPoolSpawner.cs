using UnityEngine;
using System.Collections.Generic;

public class HazardPoolSpawner : MonoBehaviour
{
    [Header("Prefabs & Pooling")]
    public GameObject[] hazardPrefabs;
    public int poolSize = 20;

    [Header("Spawn Settings")]
    public float spawnRadius = 15f;
    public float maxDistanceFromPlayer = 25f;

    [Header("Difficulty Settings")]
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.8f;
    public float difficultyIncreaseRate = 0.05f;

    [Header("References")]
    public Transform player;
    private Transform objective;

    private List<GameObject> pool = new List<GameObject>();
    private float currentInterval;
    private float timer;

    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveSpawned += SetObjective;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveSpawned -= SetObjective;
    }

    private void Start()
    {
        currentInterval = initialSpawnInterval;
        timer = currentInterval;
        FillPool();
    }

    private void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnHazard();
            currentInterval = Mathf.Max(minSpawnInterval, currentInterval - difficultyIncreaseRate);
            timer = currentInterval;
        }

        DeactivateFarHazards();
    }

    private void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = hazardPrefabs[i % hazardPrefabs.Length];
            GameObject hazard = Instantiate(prefab, transform);
            hazard.SetActive(false);
            pool.Add(hazard);
        }
    }

    private void DeactivateFarHazards()
    {
        foreach (GameObject hazard in pool)
        {
            if (hazard.activeInHierarchy &&
                Vector3.Distance(player.position, hazard.transform.position) > maxDistanceFromPlayer)
            {
                hazard.SetActive(false);
            }
        }
    }

    private void SpawnHazard()
    {
        GameObject hazard = GetAvailableHazard();
        if (hazard == null) return;

        Vector2 spawnPos;

        if (objective != null && Random.value < 0.6f) // 🎯 60% probabilidad de que sea entre player y objetivo
        {
            Vector2 dirToObjective = (objective.position - player.position).normalized;
            spawnPos = (Vector2)player.position + dirToObjective * Random.Range(spawnRadius * 0.5f, spawnRadius);
        }
        else // 🔁 40% probabilidad de que sea aleatorio como antes
        {
            spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        }

        hazard.transform.position = spawnPos;
        hazard.SetActive(true);
    }

    private GameObject GetAvailableHazard()
    {
        foreach (GameObject hazard in pool)
        {
            if (!hazard.activeInHierarchy)
                return hazard;
        }
        return null;
    }

    private void SetObjective(Transform newObjective)
    {
        objective = newObjective;
    }
}
