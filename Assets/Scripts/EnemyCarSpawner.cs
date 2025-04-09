using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    private Transform player;
    public Transform objective;

    public float spawnInterval = 3f;
    public float spawnDistanceMin = 10f;
    public float spawnDistanceMax = 20f;
    public int maxEnemiesInScene = 5;

    private float timer = 0f;

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
        player = GameManager.Instance.player.transform;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted || objective == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountActiveEnemies() < maxEnemiesInScene)
        {
            timer = 0f;
            SpawnEnemyNearPath();
        }
    }

    void SpawnEnemyNearPath()
    {
        GameObject enemy = EnemyCarPooler.Instance.GetPooledEnemy();
        if (enemy == null || player == null || objective == null) return;

        // Dirección hacia el objetivo
        Vector2 dirToObjective = (objective.position - player.position).normalized;

        // Desviación aleatoria en ángulo
        float angleOffset = Random.Range(-30f, 30f);
        Vector2 rotatedDir = Quaternion.Euler(0, 0, angleOffset) * dirToObjective;

        float spawnDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector3 spawnPos = player.position + (Vector3)(rotatedDir * spawnDistance);
        spawnPos.z = 0;

        enemy.transform.position = spawnPos;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
    }

    int CountActiveEnemies()
    {
        int count = 0;
        foreach (Transform child in EnemyCarPooler.Instance.transform)
        {
            if (child.gameObject.activeInHierarchy)
                count++;
        }
        return count;
    }

    public void SetObjective(Transform newObjective)
    {
        objective = newObjective;
    }
}
