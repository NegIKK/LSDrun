using UnityEngine;

[CreateAssetMenu(fileName = "New MusicLoop", menuName = "LSDrun/MusicLoop")]
public class LoopSO : ScriptableObject
{
    public float activationSpeed;
    public int activationSection;
    public AudioClip audioLoop;
}
