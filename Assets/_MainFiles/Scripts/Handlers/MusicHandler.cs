using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class MusicSnapshot
{
    public AudioMixerSnapshot snapshot;
    public int scoreToChange;
    public float smoothChangeTime = 0f;

    public List<AudioGroupByScore> audioGroups = new List<AudioGroupByScore>();
}

[System.Serializable]
public class AudioGroupByScore
{
    public AudioMixerGroup group;
    public ScoreCurveProgress groupProgress;
}
public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] float maxVolume = .5f;
    // [SerializeField] List<LoopSO> musicLoops = new List<LoopSO>();

    [SerializeField] AudioMixer mixer;
    [SerializeField] List<MusicSnapshot> musicSnapshots = new List<MusicSnapshot>();
    int musicPartIndex = -1;
    
    // AudioSource audioSource;
    // List<AudioSource> sources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // PlayerSettingsSO playerSettings = GameHandler.Instance.GetPlayerSettings();

        // foreach(LoopSO loop in musicLoops)
        // {
        //     AudioSource src = gameObject.AddComponent<AudioSource>();
        //     src.clip = loop.audioLoop;
        //     src.loop = true;
        //     src.playOnAwake = false;
        //     src.volume = 0f;            // всё стартует на mute
        //     src.Play();

        //     sources.Add(src);

        //     if (loop.activationSpeed >= playerSettings.runSpeed)
        //     {
        //         src.volume = maxVolume;

        //     }
        // }
        GameHandler.Instance.OnPlayerStatsChange += OnPlayerStatsChange;
        
    }

    void OnPlayerStatsChange()
    {
        int score = GameHandler.Instance.mainScore;

        

        if (musicPartIndex >= musicSnapshots.Count - 1)
        {
            return;
        }

        MusicSnapshot nextSnapshot = musicSnapshots[musicPartIndex + 1];

        if(score >= nextSnapshot.scoreToChange)
        {
            ActivateSnapshot(musicPartIndex + 1);
        }
    }
    
    void ActivateSnapshot(int index)
    {
        musicPartIndex = index;

        MusicSnapshot musicPart = musicSnapshots[index];

        float timeToChange = musicPart.smoothChangeTime;
        musicPart.snapshot.TransitionTo(timeToChange);
    }
}
