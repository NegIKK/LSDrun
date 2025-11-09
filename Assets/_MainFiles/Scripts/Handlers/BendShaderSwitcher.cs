using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BendShaderSwitcher : MonoBehaviour
{
    static GlobalKeyword BENDING_FEATURE;

    void Awake()
    {
        BENDING_FEATURE = GlobalKeyword.Create("ENABLE_BENDING");

        if (Application.isPlaying)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
            Debug.Log(BENDING_FEATURE);
        }
        else
        {
            Shader.DisableKeyword(BENDING_FEATURE);
            Debug.Log(BENDING_FEATURE);
        }
    }
}
