using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    [SerializeField] private Image losePanel;

    [SerializeField] private int lifeCount = 3;
    [SerializeField] private GameObject hearthPrefab;
    [SerializeField] private Vector3 hearthPosition;
    [SerializeField] private Vector3 offset;

    private List<GameObject> hearths = new List<GameObject>();

    private void Awake()
    {
        InitSingleton();
    }
    private void Start()
    {
        RenderHearths();
    }

    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void LoseLife()
    {
        lifeCount--;
        if (lifeCount < 0)
        {
            losePanel.gameObject.SetActive(true);
            PlayerInputController.Instance.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < hearths.Count; i++)
            {
                Destroy(hearths[i]);
            }
            hearths.Clear();
            RenderHearths();
        }


    }

    private void RenderHearths()
    {
        for (int i = 0; i < lifeCount; i++)
        {
            hearths.Add(Instantiate(hearthPrefab, hearthPosition + (offset * i), Quaternion.identity));
        }
    }
}
