using UnityEngine;
using UnityEngine.Android;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "LSDrun/Player Settings")]
public class PlayerSettingsSO : ScriptableObject
{
    [Header("Move")]
    public float runSpeed = 90f;
    public float strafeSpeed = 12f;
    public float sideLimit = 10f;
    public float stepsPerMinute = 170f;

    [Header("Jump")]
    public AnimationCurve jumpCurve;
    public float jumpDuration = .6f;
    public float jumpOffset = 2f;

    [Header("Slide")]
    public AnimationCurve slideCurve;
    public float slideDuration = 1f;
    public float slideOffset = -1.5f;
}