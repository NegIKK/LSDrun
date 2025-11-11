using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Stats", menuName = "LSDrun/Buff Stats")]
public class BuffStatsSO : ScriptableObject
{
    public int score = 0;
    
    [Header("Move")]
    public float runSpeed = 0f;
    public float strafeSpeed = 0f;
    public float sideLimit = 0f;
    public float stepsPerMinute = 0f;

    [Header("Jump")]
    public float jumpDuration = 0f;
    public float jumpOffset = 0f;

    [Header("Slide")]
    public float slideDuration = 0f;
    public float slideOffset = 0f;
}
