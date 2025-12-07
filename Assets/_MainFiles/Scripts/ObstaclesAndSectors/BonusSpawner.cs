using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] GameObject bonus;
    [Range(1,32)]
    [SerializeField] int spawnStep;
    int maxSteps;
    [SerializeField] float minSpawnDistance = 75f;

    SectorsHandler sectorsHandler;

    void Start()
    {
        GameHandler.Instance.OnBeatEvent += SpawnBonus;

        maxSteps = MusicHandler.Instance.GetMaxSteps();
        sectorsHandler = GameHandler.Instance.GetSectorHandlerByType("Main");
    }
    void SpawnBonus(int step)
    {
        if (step % spawnStep != 0) return;
        
        float bpm = MusicHandler.Instance.BPM;
        float runSpeedKMH = GameHandler.Instance.runSpeed;
        float runSpeed = runSpeedKMH * 1000f / 3600f;

        float beatInterval = 60f / bpm;
        float distancePerSpawnStep = runSpeed * (beatInterval * spawnStep);
        
        float spawnDistance = distancePerSpawnStep * Mathf.Ceil(minSpawnDistance / distancePerSpawnStep);

        Debug.Log(step);

        GameObject lastSector = sectorsHandler.currentSectors[sectorsHandler.currentSectors.Count - 1];
        Vector3 spawnPoint = new Vector3(0, 0, spawnDistance);

        Instantiate(bonus, spawnPoint, Quaternion.identity, lastSector.transform);
    }

}
