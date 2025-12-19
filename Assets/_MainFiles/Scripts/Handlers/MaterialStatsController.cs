using UnityEngine;

public class MaterialStatsController : MonoBehaviour
{
    [SerializeField] float xBend;
    [SerializeField] float yBend;

    static readonly int XBendID = Shader.PropertyToID("_XBendAmount");
    static readonly int YBendID = Shader.PropertyToID("_YBendAmount");

    void Update()
    {
        Shader.SetGlobalFloat(XBendID, xBend);
        Shader.SetGlobalFloat(YBendID, yBend);
    }
}
