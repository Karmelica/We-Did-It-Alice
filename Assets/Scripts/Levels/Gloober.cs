using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gloober : MonoBehaviour
{
    public Queue<RectTransform> points = new Queue<RectTransform>();

    private LvlFourManager lvlManager;
    public float moveSpeed = 2f;
    private RectTransform currentTarget;
    private RectTransform rectTransform;

    // --- Dodane pola do animacji odlotu ---
    private bool isDying = false;
    private Vector2 flyDirection;
    private float flyTimer = 0f;
    private float flyDuration = 2.5f;
    public float flySpeed = 800f;
    public float spinSpeed = 1080f; // stopnie na sekundę

    public void KillYourself()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        // Losowy kierunek na płaszczyźnie 2D
        flyDirection = UnityEngine.Random.insideUnitCircle.normalized;
        isDying = true;
        flyTimer = 0f;

        // Czekaj kilka sekund
        yield return new WaitForSeconds(flyDuration);

        // Wyłącz obiekt
        gameObject.SetActive(false);
    }
    
    private void Start()
    {
        lvlManager = LvlFourManager.Instance;
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
        if (isDying)
        {
            flyTimer += Time.deltaTime;
            rectTransform.anchoredPosition += flyDirection * (flySpeed * Time.deltaTime);
            rectTransform.Rotate(0f, 0f, spinSpeed * Time.deltaTime); // szybki obrót
            return;
        }
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
                Debug.Log("Gloober reached the end of the path.");
                lvlManager.progressBar.fillAmount -= 0.1f;
                currentTarget = null;
                Destroy(gameObject);
            }
        }
    }
}
