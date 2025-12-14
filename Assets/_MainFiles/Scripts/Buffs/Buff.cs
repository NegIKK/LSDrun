using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] BuffStatsSO buffStats;

    bool isCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.TryGetComponent(out Player player))
        {
            isCollected = true;
            gameObject.GetComponent<Collider>().enabled = false;
            GameHandler.Instance.OnBuffGet?.Invoke(buffStats);
            Destroy(gameObject);
            
            return;
        }

        if(other.TryGetComponent(out BonusDestroyer bonusDestroyer))
        {
            isCollected = true;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }

        
    }
}
