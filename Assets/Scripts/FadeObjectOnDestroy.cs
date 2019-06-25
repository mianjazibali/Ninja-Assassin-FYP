using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectOnDestroy : MonoBehaviour
{
    public GameObject objectToFadeOnDestroy;

    void OnDestroy()
    {
        foreach (Material m in objectToFadeOnDestroy.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Mode", 2);
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }
        iTween.FadeTo(objectToFadeOnDestroy, 0f, 1f);
    }
}
