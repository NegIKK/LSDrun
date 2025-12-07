using System.Collections;
using TMPro;
using UnityEngine;

public class BeatListener : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] TMP_Text stepText;
    [SerializeField] float flashTime = 0.1f;

    void Start()
    {
        GameHandler.Instance.OnBeatEvent += OnBeat;
        target.SetActive(false);
    }

    void OnBeat(int step)
    {
        stepText.text = "" + step;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        target.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        target.SetActive(false);
    }
}
