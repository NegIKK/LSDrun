using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text obstacles;
    [SerializeField] TMP_Text speed;

    void Start()
    {
        SetObstaclesCount(0);
        GameHandler.Instance.OnObstaclesCountUpdate += SetObstaclesCount;   //Подписка на ивент
    }

    void OnDestroy()
    {
        GameHandler.Instance.OnObstaclesCountUpdate -= SetObstaclesCount;   //отписка от ивента
    }

    void SetObstaclesCount(int newObstaclesCount)
    {
        obstacles.text = "Obstaclles: " + newObstaclesCount;
    }
}
