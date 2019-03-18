using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationDegreeX = 1f;
    public float animationTime = 1f;

    void Start()
    {
        RotateBy();
    }

    void RotateBy()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", rotationDegreeX, "time", animationTime, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear));
    }

}
