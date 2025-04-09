using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

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

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * distance);
        spawnPosition.z = 0;

        currentObjective = Instantiate(objectivePrefab, spawnPosition, Quaternion.identity);

        if (arrow != null)
        {
            arrow.SetTarget(currentObjective.transform);
        }
    }

    public GameObject GetCurrentObjective()
    {
        return currentObjective;
    }
}
