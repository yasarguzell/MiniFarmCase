using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HayFactory : MonoBehaviour, IClickable
{
    public int capacity = 5;
    public float productionTime = 20f;
    [SerializeField] private int currentWheat = 0;
    private float timer = 0;
    public TextMeshProUGUI textCurrentWheat;

    void Update()
    {
        if (currentWheat < capacity)
        {
            timer += Time.deltaTime;
            UIManager.Instance.UpdateWheatProgress(timer / productionTime);
            if (timer >= productionTime)
            {
                currentWheat++;
                textCurrentWheat.text = currentWheat.ToString();
                UIManager.Instance.UpdateWheatProgress(0);
                timer = 0;
            }
        }
    }

    public void CollectWheat()
    {
        int collected = currentWheat;
        currentWheat = 0;
        GameManager.Instance.AddWheat(collected);
    }

    public void OnClick()
    {
        CollectWheat();
    }
}