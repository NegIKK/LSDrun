using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public Action<int> OnObstaclesCountUpdate;
    public Action<PlayerSettingsSO> OnPlayerSettingsUpdate;
    public Action<BuffStatsSO> OnBuffGet;

    [SerializeField] PlayerSettingsSO playerSettings;

    [SerializeField] float playerSpeed = 0f;
    [SerializeField] int crossedObstaclesCount = 0;
    

    Player player;
    [SerializeField] List<SectorsHandler> sectorsHandlers = new List<SectorsHandler>();

    void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
        // sectorsHandler = SectorsHandler.Instance;
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
