using System;
using System.Collections;
using UnityEngine;

public class GlooberSpawner : MonoBehaviour
{
    public RectTransform parent;
    public LvlFourManager lvlManager;
    public GameObject glooberz;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (lvlManager.progressBar.fillAmount < 1f)
        {
            Instantiate(glooberz, parent);
            yield return new WaitForSeconds(2f);
        }
    }
}
