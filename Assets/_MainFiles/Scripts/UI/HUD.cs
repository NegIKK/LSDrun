using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text obstacles;
    [SerializeField] TMP_Text speed;
    [SerializeField] TMP_Text score;

    void Start()
    {
        SetObstaclesCount(0);
        GameHandler.Instance.OnObstaclesCountUpdate += SetObstaclesCount;   //Подписка на ивент
        GameHandler.Instance.OnPlayerStatsChange += SetScoreCount;
    }

    void OnDestroy()
    {
        GameHandler.Instance.OnObstaclesCountUpdate -= SetObstaclesCount;   //отписка от ивента
    }

    void SetObstaclesCount(int newObstaclesCount)
    {
        obstacles.text = "Obstaclles: " + newObstaclesCount;
    }

    void SetScoreCount()
    {
        int newScore = GameHandler.Instance.mainScore;
        score.text = "Score: " + newScore;
    }
}
