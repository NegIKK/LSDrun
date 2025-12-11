using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public Action<int> OnObstaclesCountUpdate;
    public Action<PlayerSettingsSO> OnPlayerSettingsUpdate;
    public Action<BuffStatsSO> OnBuffGet;
<<<<<<< Updated upstream
=======
    public Action<int> OnBeatEvent;
    public Action OnStartRun;
>>>>>>> Stashed changes

    [SerializeField] PlayerSettingsSO playerSettings;

    [SerializeField] float playerSpeed = 0f;
    [SerializeField] int crossedObstaclesCount = 0;
    
    [SerializeField] List<SectorsHandler> sectorsHandlers = new List<SectorsHandler>();

    void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
<<<<<<< Updated upstream
        // sectorsHandler = SectorsHandler.Instance;
=======
        OnMovementStatsUpdate += UpdatePlayerSettings;
        OnBuffGet += GetBuff;

        OnPlayerStatsChange?.Invoke();
    }

    void FixedUpdate()
    {
        sceneTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            mainScore += 1;
            OnPlayerStatsChange?.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            mainScore -= 1;
            OnPlayerStatsChange?.Invoke();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            OnStartRun?.Invoke();
        }
>>>>>>> Stashed changes
    }

    public void RegisterSectorHandler(SectorsHandler sectorsHandler)
    {
        sectorsHandlers.Add(sectorsHandler);
    }

    public SectorsHandler GetSectorHandlerByType(string sectorsHandlerType)
    {
        foreach (SectorsHandler handler in sectorsHandlers)
        {
            if (sectorsHandlerType == handler.GetSectorsHandlerType())
            {
                return handler;
            }
        }

        Debug.Log("NO HANDLER TYPE!");
        return null;
    }

    public PlayerSettingsSO GetPlayerSettings()
    {
        return playerSettings;
    }

    public void AddCrossedObstacleCount()
    {
        crossedObstaclesCount++;
        OnObstaclesCountUpdate?.Invoke(crossedObstaclesCount);
    }
}
