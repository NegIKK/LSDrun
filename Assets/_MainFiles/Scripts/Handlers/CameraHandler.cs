using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] Camera palyerCamera;
    [SerializeField] Transform mirror;
    
    [SerializeField] Transform minMirrorTransform;
    [SerializeField] Transform maxMirrorTransform;
    [SerializeField] GameObject particles;
    [SerializeField] float particlesEnablingScore;
    float currentFov;

    [SerializeField] ScoreCurveProgress mirrorPositionProgress;

    void Start()
    {
        currentFov = GameHandler.Instance.currentFov;
        palyerCamera.fieldOfView = currentFov;

        particles.SetActive(false);
        
        GameHandler.Instance.OnPlayerStatsChange += FovUpdate;
    }

    void FovUpdate()
    {
        float score = GameHandler.Instance.mainScore;
        
        palyerCamera.fieldOfView = GameHandler.Instance.currentFov;
        
        float position = mirrorPositionProgress.Evaluate(score);
        mirror.position = Vector3.Lerp(minMirrorTransform.position, maxMirrorTransform.position, position);

        if(score >= particlesEnablingScore)
        {
            particles.SetActive(true);
        }
    }

}
