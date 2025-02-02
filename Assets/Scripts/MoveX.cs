﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveX : MonoBehaviour
{
    public float positionX;
    public float animationTime;
    public float delayTime;

    private readonly bool isLocal = true;

    // Start is called before the first frame update
    void Start()
    {
        MoveTo();
    }

    void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", positionX, "time", animationTime, "delay", delayTime, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine, "isLocal", isLocal));
    }
}
