using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeatListener : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] GameObject target;
    [SerializeField] TMP_Text stepText;
    [SerializeField] float flashTime = 0.1f;

    [Header("Sound")]
    [SerializeField] bool useMetronomeSound = false;
    [SerializeField] bool useMetronomePitch = true;
    [SerializeField] int pitchStep = 4;
    [SerializeField] AudioClip metronomeSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameHandler.Instance.OnBeatEvent += OnBeat;
        target.SetActive(false);
    }

    void OnBeat(int step)
    {
        stepText.text = "" + step;
        StartCoroutine(Flash());

        PlayMetronomeSound(step);
    }

    IEnumerator Flash()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        target.SetActive(false);
    }

    void PlayMetronomeSound(int step)
    {
        if (useMetronomeSound)
        {
            // if (step % pitchStep != 0) return;

            audioSource.PlayOneShot(metronomeSound);
        }
    }
}
