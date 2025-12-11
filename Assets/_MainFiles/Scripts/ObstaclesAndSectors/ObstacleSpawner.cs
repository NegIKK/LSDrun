using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

[RequireComponent(typeof(SectorsHandler))]
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] float obstacleSpawnChance = 0.5f;
    [SerializeField] int spawnStep = 2;
    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] List<Transform> obstacleSpawnPoints = new List<Transform>();

    SectorsHandler sectorsHandler;

    void Start()
    {
        sectorsHandler = GetComponent<SectorsHandler>();

        GameHandler.Instance.OnBeatEvent += AddObstacles;
    }
    void FixedUpdate()
    {
        // currentTime += Time.deltaTime;
        // if(currentTime >= obstacleDelayTime)
        // {
        //     AddObstaclesByTime();
        //     currentTime = 0f;
        // }
    }

    void AddObstacles(int step)
    {
        if (step % spawnStep != 0) return;

        List<GameObject> currentSectors = sectorsHandler.currentSectors;
        foreach (Transform spawnPoint in obstacleSpawnPoints)
        {
            if(Random.Range(0f, 1f) <= obstacleSpawnChance)
            {
                GameObject lastSector = currentSectors[currentSectors.Count - 1];
                int index = Random.Range(0, obstaclePrefabs.Count);
                GameObject obstacleToSpawn = obstaclePrefabs[index];
                Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, lastSector.transform);
            }
        }  
    }
}
