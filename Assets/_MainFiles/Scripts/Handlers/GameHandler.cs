using System;
using System.Collections.Generic;
using UnityEngine;



public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public Action<int> OnObstaclesCountUpdate;
    public Action<PlayerSettingsSO> OnMovementStatsUpdate;
    public Action OnPlayerStatsChange;
    public Action<BuffStatsSO> OnBuffGet;
    public Action<int> OnBeatEvent;
    public Action<int> OnBonusSpawn;

    [SerializeField] PlayerSettingsSO playerSettings;
    [SerializeField] float sceneTime = 0f;

    [Header("Player Movement")]
    [SerializeField] CameraShake cameraShake;
    [SerializeField] public float bpm = 170f;
    [SerializeField] public float runSpeed = 90f;
    [SerializeField] public float strafeSpeed = 5f;
    [SerializeField] public float sideLimit = 5f;

    [SerializeField] public AnimationCurve jumpCurve;
    [SerializeField] public float jumpDuration = 3f;
    [SerializeField] public float jumpOffset = 2f;

    [SerializeField] public AnimationCurve slideCurve;
    [SerializeField] public float slideDuration = 3f;
    [SerializeField] public float slideOffset = -1.5f;

    [Header("Camera Settings")]
    [SerializeField] public Camera palyerCamera;
    [SerializeField] public float currentFov;
    [SerializeField] public float minFov;
    [SerializeField] public float maxFov;

    [Header("Scores")]
    [SerializeField] int crossedObstaclesCount = 0;
    public int mainScore = 0;
    

    [SerializeField] public Player player;
    [SerializeField] List<SectorsHandler> sectorsHandlers = new List<SectorsHandler>();

    void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
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

    void UpdatePlayerSettings(PlayerSettingsSO playerSettings)
    {
        runSpeed += playerSettings.runSpeed;
        strafeSpeed = playerSettings.strafeSpeed;
        sideLimit = playerSettings.sideLimit;
        jumpCurve = playerSettings.jumpCurve;
        jumpDuration = playerSettings.jumpDuration;
        jumpOffset = playerSettings.jumpOffset;
        slideCurve = playerSettings.slideCurve;
        slideDuration = playerSettings.slideDuration;
        slideOffset = playerSettings.slideOffset;

        cameraShake.SetStepsPerMinute(playerSettings.stepsPerMinute);

        SectorsHandler sectorsHandler = GameHandler.Instance.GetSectorHandlerByType("Main");
        sectorsHandler.SetRunSpeed(playerSettings.runSpeed);
    }

    void GetBuff(BuffStatsSO buffStats)
    {
        runSpeed += buffStats.runSpeed;
        strafeSpeed += buffStats.strafeSpeed;
        sideLimit += buffStats.sideLimit;        
        jumpDuration += buffStats.jumpDuration;
        jumpOffset += buffStats.jumpOffset;        
        slideDuration += buffStats.slideDuration;
        slideOffset += buffStats.slideOffset;

        mainScore += buffStats.score;
        OnPlayerStatsChange?.Invoke();
    }

    public void AddCrossedObstacleCount()
    {
        crossedObstaclesCount++;
        OnObstaclesCountUpdate?.Invoke(crossedObstaclesCount);
    }
}
