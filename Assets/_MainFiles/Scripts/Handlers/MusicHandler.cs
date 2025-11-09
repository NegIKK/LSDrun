using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; }
    [Range(0f,1f)]
    [SerializeField] float maxVolume = .5f;
    [SerializeField] List<LoopSO> musicLoops = new List<LoopSO>();
    
    AudioSource audioSource;
    List<AudioSource> sources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance != null) Debug.LogError("More than one " + this + " on Scene!");
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerSettingsSO playerSettings = GameHandler.Instance.GetPlayerSettings();
        
        foreach(LoopSO loop in musicLoops)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            src.clip = loop.audioLoop;
            src.loop = true;
            src.playOnAwake = false;
            src.volume = 0f;            // всё стартует на mute
            src.Play();

            sources.Add(src);

            if (loop.activationSpeed >= playerSettings.runSpeed)
            {
                src.volume = maxVolume;

            }
        }
    }
}
