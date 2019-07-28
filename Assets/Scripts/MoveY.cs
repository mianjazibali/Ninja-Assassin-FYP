using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveY : MonoBehaviour
{
    public float positionY;
    public float animationTime;
    public float delayTime;
    public iTween.LoopType loopType = iTween.LoopType.pingPong;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutSine;

    private readonly bool isLocal = true;

    // Start is called before the first frame update
    void Start()
    {
        MoveTo();
    }

    void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("Y", positionY, "time", animationTime, "delay", delayTime, "looptype", loopType, "easetype", easeType, "isLocal", isLocal));
    }
}
