using UnityEngine;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager Instance;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> explosionPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab,transform);
            explosion.SetActive(false);
            explosionPool.Enqueue(explosion);
        }
    }

    public void SpawnExplosion(Vector3 position)
    {
        GameObject explosion = explosionPool.Count > 0 ? explosionPool.Dequeue() : Instantiate(explosionPrefab);
        explosion.transform.position = position;
        explosion.SetActive(true);
        StartCoroutine(DeactivateAfterSeconds(explosion, 2f));
    }

    private IEnumerator<WaitForSeconds> DeactivateAfterSeconds(GameObject explosion, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        explosion.SetActive(false);
        explosionPool.Enqueue(explosion);
    }
}
