using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float tileLength = 12.0f;
    private float spawnZ = -12.0f;
    private float safeZone = 16.5f;
    private int amountOfTilesOnScreen = 7;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if (i < 5)
            {
                SpawnTiles(0);
            }
            else
            {
                SpawnTiles();
            }
        }
    }
    
    void Update()
    {
        if (playerTransform.position.z - safeZone > spawnZ - amountOfTilesOnScreen * tileLength)
        {
            SpawnTiles();
            DeleteTiles();
        }
    }

    void SpawnTiles(int prefabIndex = -1)
    {
        GameObject go;

        if (prefabIndex == -1)
        {
            go = (GameObject)Instantiate(tilePrefabs[RandomPrefabIndex()]);
        }
        else
        {
            go = (GameObject)Instantiate(tilePrefabs[prefabIndex]);
        }

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    void DeleteTiles()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        int randomIndex = lastPrefabIndex;
        if (tilePrefabs.Length <= 1)
        {
            return lastPrefabIndex;
        }

        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return lastPrefabIndex;
    }
}
