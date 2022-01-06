using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class WindowGraph : MonoBehaviour
{
    public Sprite CircleSprite; 
    public RectTransform GraphContainer;
    public DateItem LabelTemplateX;
    public RectTransform LabelTemplateY;
    public int MaximumY;
    private GameObject lastCircleGameObject = null;

    private int xPosition = 0;
    public int XPosition => xPosition += 50;

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(GraphContainer, false);
        gameObject.GetComponent<Image>().sprite = CircleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
    }

    public void ShowGraph(int value, DateTime dateTime)
    {
        float graphHeight = GraphContainer.sizeDelta.y;
        {
            float xPosition = XPosition;
            float yPosition = value * (graphHeight / MaximumY);
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            DateItem labelX = Instantiate(LabelTemplateX);
            labelX.transform.SetParent(GraphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, -20f);
            labelX.SetDate(
                dateTime.Year.ToString().Substring(2) + "년", 
                dateTime.Month.ToString() + "월", 
                dateTime.Day.ToString() + "일");
        }

        int separatorCount = 10;
        for (int i = 0; i < separatorCount; i++)
        {
            RectTransform labelY = Instantiate(LabelTemplateY);
            labelY.SetParent(GraphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = graphHeight / MaximumY;
            labelY.anchoredPosition = new Vector2(-7f, (normalizedValue * MaximumY * i) / separatorCount);
            labelY.GetComponent<Text>().text =  string.Format("{0:#,0}", Mathf.RoundToInt(MaximumY * i / separatorCount)) + "천원";
        } 
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(GraphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f; 

        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}