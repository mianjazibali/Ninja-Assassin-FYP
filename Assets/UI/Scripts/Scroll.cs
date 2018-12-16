using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public RectTransform panel;
    public Button[] bttn;
    public RectTransform center;

    public float[] distance;
    public float[] distReposition;
    private bool dragging = false;
    private int bttnDistance;
    private int minButtonNum;
    private int bttnLength;

    private void Start()
    {
        bttnLength = bttn.Length;
        distance = new float[bttnLength];

        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);

    }

    private void Update()
    {

        for(int i = 0; i < bttn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x); 
        }

        

        float minDistance = Mathf.Min(distance);

        for(int i = 0; i < bttn.Length; i++)
        {
            if(minDistance == distance[i])
            {
                minButtonNum = i;
            }
        }

        if (!dragging)
        {
            LerpToBtn(minButtonNum * -bttnDistance);
        }
    }

    void LerpToBtn(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 5f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
