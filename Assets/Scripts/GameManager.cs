using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int wheat;
    public int flour;
    public int bread;

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

    void Start()
    {
        LoadGame();
    }

    public void AddWheat(int amount)
    {
        wheat += amount;
        UIManager.Instance.UpdateWheat(wheat);
    }

    public void AddFlour(int amount)
    {
        flour += amount;
        UIManager.Instance.UpdateFlour(flour);
    }

    public void AddBread(int amount)
    {
        bread += amount;
        UIManager.Instance.UpdateBread(bread);
    }

    public void ConvertWheatToFlour()
    {
        if (wheat > 0)
        {
            wheat--;
            flour++;
            UIManager.Instance.UpdateWheat(wheat);
            UIManager.Instance.UpdateFlour(flour);
        }
    }

    public void ConvertFlourToBreadV1()
    {
        if (flour > 0)
        {
            flour--;
            bread++;
            UIManager.Instance.UpdateFlour(flour);
            UIManager.Instance.UpdateBread(bread);
        }
    }

    public void ConvertFlourToBreadV2()
    {
        if (flour > 1)
        {
            flour -= 2;
            bread++;
            UIManager.Instance.UpdateFlour(flour);
            UIManager.Instance.UpdateBread(bread);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("wheat", wheat);
        PlayerPrefs.SetInt("flour", flour);
        PlayerPrefs.SetInt("bread", bread);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        wheat = PlayerPrefs.GetInt("wheat", 0);
        flour = PlayerPrefs.GetInt("flour", 0);
        bread = PlayerPrefs.GetInt("bread", 0);

        UIManager.Instance.UpdateWheat(wheat);
        UIManager.Instance.UpdateFlour(flour);
        UIManager.Instance.UpdateBread(bread);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

