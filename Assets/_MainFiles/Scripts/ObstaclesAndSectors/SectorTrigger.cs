using System.Collections.Generic;
using UnityEngine;

public class SectorTrigger : MonoBehaviour
{
    [SerializeField] string sectorType;
    [SerializeField] Transform nextSectorTransform;
    [SerializeField] List<Transform> obstacleSpawnPoints = new List<Transform>();
    [SerializeField] GameObject sector;

    public Transform GetNextSectorTransform()
    {
        return nextSectorTransform;
    }

    public List<Transform> GetObstacleSpawnPoints()
    {
        return obstacleSpawnPoints;
    }

    // public bool CheckIsBackground()
    // {
    //     return isBackground;
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            SectorsHandler handler = GameHandler.Instance.GetSectorHandlerByType(sectorType);
            handler.AddSector();
        }

        if (other.TryGetComponent(out SectorDestroyer sectorDestroyer))
        {
            SectorsHandler handler = GameHandler.Instance.GetSectorHandlerByType(sectorType);
            handler.RemoveSector(sector);
        }
    }
}
