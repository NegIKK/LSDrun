using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SectorsHandler : MonoBehaviour
{
    // public static SectorsHandler Instance { get; private set; }
    [SerializeField] string sectorsHandlerType;
    [SerializeField] bool useDebugPrefabs = false;
    [SerializeField] List<GameObject> sectorDebugPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> sectorPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] float obstacleSpawnChance = 0.8f;

    [SerializeField] List<GameObject> sectors = new List<GameObject>();

    [Tooltip("speed in Km/H")]
    [SerializeField] float runSpeed = 90f;
    
    [Range(-1f, 1f)]
    [SerializeField] float runDirection = -1f;

    private void Awake()
    {
        // if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        // Instance = this;
    }

    void Start()
    {
        // GameHandler.Instance.RegisterSectorHandler(this);
        GameHandler.Instance.OnBuffGet += UpdateSpeed;
        SetRunSpeed(GameHandler.Instance.runSpeed);
    }

    void FixedUpdate()
    {
        foreach (GameObject sector in sectors)
        {
            Vector3 sectorPos = sector.transform.position;

            float runSpeedConverted = runSpeed / 3.6f; //конвертируем километры в час в метры в секунду
            sectorPos.z += runSpeedConverted * runDirection * Time.deltaTime;

            sector.transform.position = sectorPos;
        }
    }

    public string GetSectorsHandlerType()
    {
        return sectorsHandlerType;
    }

    public void SetRunSpeed(float newRunSpeed)
    {
        runSpeed = newRunSpeed;
    }

    public void AddRunSpeed(float newRunSpeed)
    {
        runSpeed += newRunSpeed;
    }

    void UpdateSpeed(BuffStatsSO buff)
    {
        runSpeed = GameHandler.Instance.runSpeed;
    }

    public void AddSector()
    {
        int sectorPrefabIndex = Random.Range(0, sectorPrefabs.Count);
        GameObject sectorToSpawn = sectorPrefabs[sectorPrefabIndex];

        GameObject lastSector = sectors[sectors.Count - 1];
        Transform spawnPoint = lastSector.GetComponent<SectorTrigger>().GetNextSectorTransform();

        GameObject createdSector = Instantiate(sectorToSpawn, spawnPoint.position, Quaternion.identity);
        AddObstacles(createdSector);

        sectors.Add(createdSector);
    }
    
    void AddObstacles(GameObject sector)
    {
        if (sector.TryGetComponent(out SectorTrigger sectorTrigger))
        {
            foreach (Transform spawnPoint in sectorTrigger.GetObstacleSpawnPoints())
            {
                if(Random.Range(0, 1) <= obstacleSpawnChance)
                {
                    int index = Random.Range(0, obstaclePrefabs.Count);
                    GameObject obstacleToSpawn = obstaclePrefabs[index];
                    Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, sector.transform);
                }
            }
        }
    }

    public void RemoveSector(GameObject sectorToRemove)
    {
        sectors.Remove(sectorToRemove);
        Destroy(sectorToRemove);
    }
}
