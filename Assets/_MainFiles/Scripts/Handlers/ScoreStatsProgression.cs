using UnityEngine;

[System.Serializable]
public class ScoreCurveProgress
{
    public float minScore;
    public float maxScore;
    public AnimationCurve curve;      // вход 0..1
    public float minValue;
    public float maxValue;

    public float Evaluate(float score)
    {
        if (score <= minScore) return minValue;
        if (score >= maxScore) return maxValue;

        float t = (score - minScore) / (maxScore - minScore);
        float c = curve.Evaluate(t);
        return Mathf.Lerp(minValue, maxValue, c);
    }
}


public class ScoreStatsProgression : MonoBehaviour
{
    [SerializeField] ScoreCurveProgress runSpeedProgress;
    [SerializeField] ScoreCurveProgress strafeProgress;
    [SerializeField] ScoreCurveProgress bpmProgress;
    [SerializeField] ScoreCurveProgress shakeMultiplierProgress;
    [SerializeField] ScoreCurveProgress fovProgress;
    // [SerializeField] ScoreCurveProgress strafeProgress;

    void Start()
    {
        GameHandler.Instance.OnPlayerStatsChange += PlayerStatsUpdate;
    }

    void Update()
    {

    }

    void PlayerStatsUpdate()
    {
        int score = GameHandler.Instance.mainScore;

        GameHandler.Instance.runSpeed = runSpeedProgress.Evaluate(score);
        GameHandler.Instance.strafeSpeed = strafeProgress.Evaluate(score);
        GameHandler.Instance.bpm = bpmProgress.Evaluate(score);
        GameHandler.Instance.currentFov = fovProgress.Evaluate(score);
    }
}
