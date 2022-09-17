using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _Game.Managers;
public class LevelNo : MonoBehaviour
{
    public TextMeshProUGUI LevelNoText;
    public int currentNo;
    void Start()
    {
        currentNo = PlayerPrefs.GetInt("current_scene_text", 0);
        LevelNoText.text = "LEVEL "+(currentNo + 1).ToString();

        GameManager.Instance.OnLevelCompleted += DisableObj;
    }


  void DisableObj()
    {
        gameObject.SetActive(false);
    }
}
