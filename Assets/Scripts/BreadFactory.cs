using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class BreadFactory : MonoBehaviour, IClickable
{
    public int capacity = 20;
    public float productionTime = 60f;
    [SerializeField] private int currentBread = 0;
    public int breadCost;
    public GameObject panel;
    private Queue<int> productionQueue = new Queue<int>();
    private bool isProducing = false;
    private int processingCount = 0;
    public TextMeshProUGUI textCurrentBread;

    public void StartProduction()
    {
        int availableFlour = GameManager.Instance.flour;

        if (availableFlour >= breadCost && productionQueue.Count < capacity)
        {
            productionQueue.Enqueue(1);
            GameManager.Instance.AddFlour(-breadCost);

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
                if (breadCost == 1)
                    UIManager.Instance.UpdateBreadV1Progress(progress);
                else
                    UIManager.Instance.UpdateBreadV2Progress(progress);
                await UniTask.Yield();
            }

            if (breadCost == 1)
                UIManager.Instance.UpdateBreadV1Progress(0);
            else
                UIManager.Instance.UpdateBreadV2Progress(0);
            currentBread++;
            textCurrentBread.text = currentBread.ToString();
        }
        isProducing = false;
    }

    public void RemoveLastQueuedProduction()
    {
        if (productionQueue.Count > processingCount)
        {
            productionQueue.Dequeue();
            GameManager.Instance.AddFlour(breadCost);
        }
    }

    public void CollectBread()
    {
        int collected = currentBread;
        currentBread = 0;
        GameManager.Instance.AddBread(collected);
    }

    public void OnClick()
    {
        if (panel.activeSelf == false)
        {
            panel.SetActive(true);
        }
        else
        {
            CollectBread();
        }
    }
}
