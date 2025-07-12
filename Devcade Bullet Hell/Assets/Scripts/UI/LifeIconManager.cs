using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeIconManager : MonoBehaviour
{
    private List<Image> lifeIcons = new List<Image>();

    [SerializeField] GameObject lifeIconPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GamePlayManager.Instance.OnLifeUpdate += OnLifeUpdate;
    }

    private void OnLifeUpdate(object sender)
    {
        for (int i = lifeIcons.Count - 1; i >= 0; i--)
        {
            Destroy(lifeIcons[i].gameObject);
        }

        for (int i = 0; i < GamePlayManager.Instance.currLives; i++)
        {
            Instantiate(lifeIconPrefab, gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
