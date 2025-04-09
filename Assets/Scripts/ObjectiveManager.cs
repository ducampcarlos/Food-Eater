using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public static event Action<Transform> OnObjectiveSpawned;

    [Header("Objective Settings")]
    public GameObject objectivePrefab;
    private Transform player;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 40f;
    [SerializeField] ObjectiveArrow arrow;

    private GameObject currentObjective;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    public void SpawnNewObjective()
    {
        if (currentObjective != null)
        {
            Destroy(currentObjective);
        }

        arrow.GetComponent<Image>().enabled = true;

        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * distance);
        spawnPosition.z = 0;

        currentObjective = Instantiate(objectivePrefab, spawnPosition, Quaternion.identity);

        if (arrow != null)
        {
            arrow.SetTarget(currentObjective.transform);
        }

        OnObjectiveSpawned?.Invoke(currentObjective.transform);
    }

    public GameObject GetCurrentObjective()
    {
        return currentObjective;
    }
}
