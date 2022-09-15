using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Tools.Helpers;
public class StorageManager : Singleton<StorageManager>
{
    [SerializeField] int TotalCoinsCount;
    public int RewardValue;
    public int TotalScore;
    public static int GetTotalCoin() => PlayerPrefs.GetInt("LifeTimeScore");
    public static void SaveTotalCoin(int amount) => PlayerPrefs.SetInt("LifeTimeScore", amount);
    public int currentLevel;
    public int currentLevelText;
    public void SetTotalScore()
    {
        int currentLifetimeScore = PlayerPrefs.GetInt("LifeTimeScore", 0);
        int newLifeTimeScore = currentLifetimeScore + RewardValue;
        PlayerPrefs.SetInt("TotalCoinsCount", newLifeTimeScore + TotalCoinsCount);
        PlayerPrefs.SetInt("LifeTimeScore", newLifeTimeScore);
    }


    public void GetTotalScore()
    {
        TotalScore = PlayerPrefs.GetInt("LifeTimeScore");
       
    }
    public void IncreasePoints(int count)
    {
        RewardValue += count;

    }

}
