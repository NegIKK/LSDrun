using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] GameObject bonus;
    [Range(1,32)]
    [SerializeField] int spawnStep;
    int maxSteps;
    int lastSpawnStep = -1;
    HashSet<int> spawnedSteps = new HashSet<int>();
    
    [SerializeField] float minSpawnDistance = 75f;

    SectorsHandler sectorsHandler;
    Transform player;

    void Start()
    {
        player = GameHandler.Instance.player.transform;

        GameHandler.Instance.OnBeatEvent += SpawnBonus;

        maxSteps = MusicHandler.Instance.GetMaxSteps();
        sectorsHandler = GameHandler.Instance.GetSectorHandlerByType("Main");
    }

    // void LateUpdate()
    // {
    //     foreach (var r in FindObjectsOfType<Renderer>())
    //     {
    //         Bounds b = r.bounds;

    //         if (!IsFinite(b.center) || !IsFinite(b.extents))
    //         {
    //             Debug.LogError(
    //                 $"INVALID AABB: {r.name}\n" +
    //                 $"Pos: {r.transform.position}\n" +
    //                 $"Scale: {r.transform.lossyScale}",
    //                 r.gameObject
    //             );
    //         }
    //     }
    // }

    // bool IsFinite(Vector3 v)
    // {
    //     return float.IsFinite(v.x) && float.IsFinite(v.y) && float.IsFinite(v.z);
    // }

    void SpawnBonus(int step)
    {
        // if (step == 1) {spawnedSteps.Clear(); Debug.Log("ClearBonusHash");}
        if (step <= 0) return;
        if (step % spawnStep != 0) return;
        if(lastSpawnStep == step) return;
        lastSpawnStep = step;

        // if(spawnedSteps.Contains(step)) return;
        // spawnedSteps.Add(step);
        
        float spawnDistance = GetBeatAlignedSpawnDistance(spawnStep, minSpawnDistance);

        GameObject lastSector = sectorsHandler.currentSectors[sectorsHandler.currentSectors.Count - 1];
        // Vector3 spawnPoint = new Vector3(0, 0, spawnDistance);
        Vector3 spawnPoint = Vector3.forward * spawnDistance;
        
        Instantiate(bonus, spawnPoint, Quaternion.identity, lastSector.transform);
        GameHandler.Instance.OnBonusSpawn?.Invoke(step);
        Debug.Log(this + " Spawned Bonus on " + step + " step");
    }

    float GetBeatAlignedSpawnDistance(int _spawnStep, float _minSpawnDistance)
    {
        float bpm = MusicHandler.Instance.BPM;
        float runSpeedKMH = GameHandler.Instance.runSpeed;
        float runSpeed = runSpeedKMH * 1000f / 3600f;

        float beatInterval = 60f / bpm;
        float stepDistance = runSpeed * (beatInterval * _spawnStep);

        int slot = Mathf.RoundToInt(_minSpawnDistance / stepDistance);
        if(slot < 1) slot = 1;
        
        float spawnDistance = slot * stepDistance;
        
        return spawnDistance;
    }
}
