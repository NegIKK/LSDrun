using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] BuffStatsSO buffStats;



    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameHandler.Instance.OnBuffGet?.Invoke(buffStats);
        }

        Destroy(gameObject);
    }
}
