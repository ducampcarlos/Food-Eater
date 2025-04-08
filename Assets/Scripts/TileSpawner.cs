using UnityEngine;
using System.Collections.Generic;

public class TileSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject tilePrefab;
    public int tileSize = 10;

    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentPlayerTile;

    void Start()
    {
        currentPlayerTile = new Vector2Int(
            Mathf.FloorToInt(player.position.x / tileSize),
            Mathf.FloorToInt(player.position.y / tileSize)
        );

        UpdateTilesAroundPlayer(); // <- Esto inicializa el piso al arrancar
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        Vector2Int playerTile = new Vector2Int(
            Mathf.FloorToInt(player.position.x / tileSize),
            Mathf.FloorToInt(player.position.y / tileSize)
        );

        if (playerTile != currentPlayerTile)
        {
            currentPlayerTile = playerTile;
            UpdateTilesAroundPlayer();
        }
    }

    void UpdateTilesAroundPlayer()
    {
        List<Vector2Int> neededTiles = new List<Vector2Int>();

        for (int x = -2; x <= 2; x++) // ahora son 5 tiles horizontales
        {
            for (int y = -1; y <= 1; y++) // 3 tiles verticales
            {
                Vector2Int tileCoord = currentPlayerTile + new Vector2Int(x, y);
                neededTiles.Add(tileCoord);

                if (!spawnedTiles.ContainsKey(tileCoord))
                {
                    Vector3 spawnPos = new Vector3(tileCoord.x * tileSize, tileCoord.y * tileSize, 0);
                    GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
                    spawnedTiles.Add(tileCoord, tile);
                }
            }
        }

        // Eliminar tiles fuera del rango
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var coord in spawnedTiles.Keys)
        {
            if (!neededTiles.Contains(coord))
            {
                Destroy(spawnedTiles[coord]);
                toRemove.Add(coord);
            }
        }

        foreach (var coord in toRemove)
        {
            spawnedTiles.Remove(coord);
        }
    }
}
