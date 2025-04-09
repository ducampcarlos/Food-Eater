using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    private Transform player;
    public float spawnInterval = 3f;
    public float spawnDistance = 15f;
    public int maxEnemiesInScene = 5;

    private float timer = 0f;

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountActiveEnemies() < maxEnemiesInScene)
        {
            timer = 0f;
            SpawnEnemyCar();
        }
    }

    void SpawnEnemyCar()
    {
        GameObject enemy = EnemyCarPooler.Instance.GetPooledEnemy();
        if (enemy != null && player != null)
        {
            Vector2 spawnOffset = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPos = player.position + (Vector3)spawnOffset;
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
        }
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
}
