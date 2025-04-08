using UnityEngine;
using System.Collections.Generic;

public class TileSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject tilePrefab;
    public int tileSize = 10;

    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentPlayerTile;

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
        // Solo mantener tiles en un área de 3x3 alrededor del jugador
        List<Vector2Int> neededTiles = new List<Vector2Int>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int tileCoord = currentPlayerTile + new Vector2Int(x, y);
                neededTiles.Add(tileCoord);

                if (!spawnedTiles.ContainsKey(tileCoord))
                {
                    Vector3 spawnPos = new Vector3(tileCoord.x * tileSize, tileCoord.y * tileSize, 0);
                    GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                    spawnedTiles.Add(tileCoord, tile);
                }
            }
        }

        // Eliminar tiles que ya no se necesitan
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
