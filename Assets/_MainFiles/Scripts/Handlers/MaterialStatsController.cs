using System.Collections;
using UnityEngine;

public class MaterialStatsController : MonoBehaviour
{
    [SerializeField] float changeDirectionTime = 5f;
    float currentTime;
    [SerializeField] float changingSpeed = 2f;
    [SerializeField] float minXBend;
    [SerializeField] float maxXBend;
    [SerializeField] float minYBend;
    [SerializeField] float maxYBend;
    [SerializeField] float xBend;
    [SerializeField] float yBend;
    float currentXBend;
    float currentYBend;

    static readonly int XBendID = Shader.PropertyToID("_XBendAmount");
    static readonly int YBendID = Shader.PropertyToID("_YBendAmount");

    void Update()
    {
        currentXBend = Mathf.Lerp(currentXBend, xBend, changingSpeed * Time.deltaTime);
        currentYBend = Mathf.Lerp(currentYBend, yBend, changingSpeed * Time.deltaTime);


        Shader.SetGlobalFloat(XBendID, currentXBend);
        Shader.SetGlobalFloat(YBendID, currentYBend);

        // StartCoroutine(switchBendDirection(changeDirectionTime));

        currentTime += Time.deltaTime;
        if(currentTime >= changeDirectionTime)
        {
            currentTime = 0f;
            SetBendValues();
        }
    }

    // IEnumerator switchBendDirection(float secondsToWait)
    // {
    //     yield return new WaitForSeconds(secondsToWait);
    //     SetBendValues();
    // }

    void SetBendValues()
    {
        xBend = Random.Range(minXBend, maxXBend);
        yBend = Random.Range(minYBend, maxYBend);
    }
}
