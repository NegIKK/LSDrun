using System.Collections;
using UnityEngine;

public class BeatListener : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float flashTime = 0.1f;

    void Start()
    {
        GameHandler.Instance.OnBeatEvent += OnBeat;
        target.SetActive(false);
    }

    void OnBeat()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        target.SetActive(false);
    }
}
