using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDestructed : MonoBehaviour
{
    float time = 0.5f;

    void Start()
    {
        StartCoroutine(SetMaterialTransparent());
    }

    IEnumerator SetMaterialTransparent()
    {
        yield return new WaitForSeconds(time);
        foreach (Transform t in transform)
        {
            foreach(Material m in t.GetComponent<Renderer>().materials)
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
        }
        iTween.FadeTo(gameObject, 0f, 1f);
        StartCoroutine(DestroyAfterFadeOut());
    }

    IEnumerator DestroyAfterFadeOut()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
