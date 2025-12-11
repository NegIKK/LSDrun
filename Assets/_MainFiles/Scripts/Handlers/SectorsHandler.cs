using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SectorsHandler : MonoBehaviour
{
    [SerializeField] string sectorsHandlerType;
    [SerializeField] List<GameObject> sectorPrefabs = new List<GameObject>();

    public List<GameObject> currentSectors = new List<GameObject>();
    [Tooltip("speed in Km/H")]
    [SerializeField] float runSpeed = 90f;    
    [Range(-1f, 1f)]
    [SerializeField] float runDirection = -1f;


    void Start()
    {
        GameHandler.Instance.OnPlayerStatsChange += UpdateSpeed;
        SetRunSpeed(GameHandler.Instance.runSpeed);
    }

    void FixedUpdate()
    {
        runSpeed = GameHandler.Instance.runSpeed;

        foreach (GameObject sector in currentSectors)
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

    void UpdateSpeed()
    {
        runSpeed = GameHandler.Instance.runSpeed;
    }

    public void AddSector()
    {
        int sectorPrefabIndex = Random.Range(0, sectorPrefabs.Count);
        GameObject sectorToSpawn = sectorPrefabs[sectorPrefabIndex];

        GameObject lastSector = currentSectors[currentSectors.Count - 1];
        Transform spawnPoint = lastSector.GetComponent<SectorTrigger>().GetNextSectorTransform();

        GameObject createdSector = Instantiate(sectorToSpawn, spawnPoint.position, Quaternion.identity);
        // AddObstacles(createdSector);

        currentSectors.Add(createdSector);
    }

    public void RemoveSector(GameObject sectorToRemove)
    {
        currentSectors.Remove(sectorToRemove);
        Destroy(sectorToRemove);
    }
}
