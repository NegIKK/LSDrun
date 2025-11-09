using System.Collections;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public ShakeData runShake;
    public ShakeData stepShake;

    [SerializeField] float stepsPerMinute = 130f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraShakerHandler.Shake(runShake);
        StartCoroutine(Step());
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     CameraShakerHandler.Shake(stepShake);
        // }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     CameraShakerHandler.Shake(runShake);
        // }
    }

    public void SetStepsPerMinute(float newStepsPerMinute)
    {
        stepsPerMinute = newStepsPerMinute;
    }

    IEnumerator Step()
    {
        while (true)
        {
            float interval = 60f / stepsPerMinute;
            CameraShakerHandler.Shake(stepShake);
            yield return new WaitForSeconds(interval);
        }
    }
}
