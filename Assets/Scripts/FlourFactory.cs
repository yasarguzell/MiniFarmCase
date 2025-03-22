using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class FlourFactory : MonoBehaviour, IClickable
{
    public int capacity = 10;
    public float productionTime = 40f;
    [SerializeField] private int currentFlour = 0;
    public int wheatCost = 1;
    public GameObject panel;
    private Queue<int> productionQueue = new Queue<int>();
    private bool isProducing = false;
    private int processingCount = 0;
    public TextMeshProUGUI textCurrentFlour;

    public void StartProduction()
    {
        if (GameManager.Instance.wheat >= wheatCost && productionQueue.Count < capacity)
        {
            productionQueue.Enqueue(1);
            GameManager.Instance.AddWheat(-wheatCost);

            if (!isProducing)
            {
                isProducing = true;
                ProcessQueueAsync().Forget();
            }
        }
    }

    private async UniTaskVoid ProcessQueueAsync()
    {
        while (productionQueue.Count > 0)
        {
            processingCount++;
            productionQueue.Dequeue();
            float elapsedTime = 0f;
            while (elapsedTime < productionTime)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / productionTime;
                UIManager.Instance.UpdateFlourProgress(progress);
                await UniTask.Yield();
            }

            UIManager.Instance.UpdateFlourProgress(0);
            currentFlour++;
            textCurrentFlour.text = currentFlour.ToString();
        }
        isProducing = false;
    }

    public void RemoveLastQueuedProduction()
    {
        if (productionQueue.Count > processingCount)
        {
            productionQueue.Dequeue();
            GameManager.Instance.AddWheat(wheatCost);
        }
    }

    public void CollectFlour()
    {
        int collected = currentFlour;
        currentFlour = 0;
        GameManager.Instance.AddFlour(collected);
    }

    public void OnClick()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
        else
        {
            CollectFlour();
        }
    }
}