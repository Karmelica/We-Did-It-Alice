using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gloober : MonoBehaviour
{
    public Queue<RectTransform> points = new Queue<RectTransform>();

    public float moveSpeed = 2f;
    private RectTransform currentTarget;
    private RectTransform rectTransform;

    public void KillYourself()
    {
        Destroy(gameObject);
    }
    
    private void Start()
    {
        RectTransform[] checkPoints = LvlFourManager.Instance.trailPoints;
        rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < checkPoints.Length; i++)
        {
            points.Enqueue(checkPoints[i]);
        }
        if (points.Count > 0)
            currentTarget = points.Dequeue();
    }

    private void Update()
    {
        MoveThroughPoints();
    }

    private void MoveThroughPoints()
    {
        if (currentTarget == null)
            return;

        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, currentTarget.anchoredPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(rectTransform.anchoredPosition, currentTarget.anchoredPosition) < 0.1f)
        {
            if (points.Count > 0)
            {
                currentTarget = points.Dequeue();
            }
            else
            {
                currentTarget = null;
            }
        }
    }
}
