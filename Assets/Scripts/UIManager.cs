using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI wheatText;
    public TextMeshProUGUI flourText;
    public TextMeshProUGUI breadText;

    private int wheat = 0;
    private int flour = 0;
    private int bread = 0;

    public Image wheatProgressBar;
    public Image flourProgressBar;
    public Image breadV1ProgressBar;
    public Image breadV2ProgressBar;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateWheat(int amount)
    {
        wheat = amount;
        wheatText.text = wheat.ToString();
    }

    public void UpdateFlour(int amount)
    {
        flour = amount;
        flourText.text = flour.ToString();
    }

    public void UpdateBread(int amount)
    {
        bread = amount;
        breadText.text = bread.ToString();
    }

    public void UpdateWheatProgress(float progress)
    {
        if (wheatProgressBar != null)
        {
            wheatProgressBar.fillAmount = progress;
        }
    }
    
    public void UpdateFlourProgress(float progress)
    {
        if (flourProgressBar != null)
        {
            flourProgressBar.fillAmount = progress;
        }
    }
    
    public void UpdateBreadV1Progress(float progress)
    {
        if (breadV1ProgressBar != null)
        {
            breadV1ProgressBar.fillAmount = progress;
        }
    }
    
    public void UpdateBreadV2Progress(float progress)
    {
        if (breadV2ProgressBar != null)
        {
            breadV2ProgressBar.fillAmount = progress;
        }
    }
}