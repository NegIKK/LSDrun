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

    [SerializeField] AudioMixer mixer;
    public float BPM = 170f;
    [SerializeField] int maxSteps = 32;
    [SerializeField] int currentStep;
    [SerializeField] List<MusicSnapshot> musicSnapshots = new List<MusicSnapshot>();
    [SerializeField] GameObject audioSourceObject;
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();

    bool musicIsPlaying = false;
    
    int musicPartIndex = -1;

    public float BeatInterval { get; private set; }         // длительность доли
    public float NextBeatTime { get; private set; }          // время следующего бита (по audioSource.time)

    private void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;

        BeatInterval = 60f / BPM;
        NextBeatTime = BeatInterval;  

        AudioSource[] sources = audioSourceObject.GetComponents<AudioSource>();
        audioSources.AddRange(sources);
    }

    void Start()
    {
        // GameHandler.Instance.OnPlayerStatsChange += OnPlayerStatsChange;
        GameHandler.Instance.OnBeatEvent += OnBeat; 
    }

    void Update()
    {
        CheckBeat();
    }

    void CheckBeat()
    {
        float time = Time.time;

        if(time >= NextBeatTime)
        {
            NextBeatTime += BeatInterval;
            
            if(currentStep >= maxSteps)
            {
                currentStep = 0;
            }

            if (!musicIsPlaying)
            {
                musicIsPlaying = true;
                
                foreach(AudioSource source in audioSources)
                {
                    source.Play();
                }
            }

            ++currentStep;
            GameHandler.Instance.OnBeatEvent?.Invoke(currentStep);
            
        }
    }

    void OnBeat(int step)
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

    public int GetMaxSteps()
    {
        return maxSteps;
    }
}
