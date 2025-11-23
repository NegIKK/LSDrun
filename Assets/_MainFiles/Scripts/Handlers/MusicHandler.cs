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
    [SerializeField] float BPM = 170f;
    [SerializeField] List<MusicSnapshot> musicSnapshots = new List<MusicSnapshot>();
    
    int musicPartIndex = -1;

    float beatInterval;          // длительность доли
    float nextBeatTime;          // время следующего бита (по audioSource.time)

    private void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
        // GameHandler.Instance.OnPlayerStatsChange += OnPlayerStatsChange;
        GameHandler.Instance.OnBeatEvent += OnBeat; 

        beatInterval = 60f / BPM;
        nextBeatTime = beatInterval;   
    }

    void Update()
    {
        CheckBeat();
    }

    void CheckBeat()
    {
        float time = Time.time;

        if(time >= nextBeatTime)
        {
            nextBeatTime += beatInterval;
            GameHandler.Instance.OnBeatEvent?.Invoke();
        }
    }

    void OnBeat()
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
