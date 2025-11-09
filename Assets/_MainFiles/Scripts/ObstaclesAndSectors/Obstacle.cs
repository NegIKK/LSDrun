using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool canCross;

    [SerializeField] bool jumpPass;
    [SerializeField] bool slidePass;

    public bool GetCanCross()
    {
        return canCross;
    }

    public bool GetJumpPass()
    {
        return jumpPass;
    }

    public bool GetSlidePass()
    {
        return slidePass;
    }
}
