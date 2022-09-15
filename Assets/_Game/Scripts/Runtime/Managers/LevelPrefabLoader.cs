using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Tools.Helpers;
using UnityEngine.SceneManagement;
namespace _Game.Managers {
    public class LevelPrefabLoader : Singleton<LevelPrefabLoader>
    {
        [SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
        [SerializeField] private GameObject currentLvlPrefab;

        private int levelNo;

        int currentLevel;
        public List<GameObject> LevelPrefabs => _levelPrefabs;
        private void Start()
        {
            currentLevel = PlayerPrefs.GetInt("current_level");
            LoadLvlPrefab();
            _Game.Managers.GameManager.Instance.OnNextLevelLoad += LoadNextLevel;
        }
        public void LoadLvlPrefab()
        {

            levelNo = PlayerPrefs.GetInt("current_level", 0);

            /*#if UNITY_EDITOR

                    levelNo = amarIcchaLevel;
                    PlayerPrefs.SetInt("current_level", levelNo);

            #endif*/

            currentLvlPrefab = Instantiate(LevelPrefabManager.Instance.GetCurrentLevelPrefab());

           
        }

        void LoadNextLevel()
        {

            if (currentLevel + 1 >= _levelPrefabs.Count)
            {
                PlayerPrefs.SetInt("current_level", 0);
                print("Reload");

            }
            else
            {
                print("next");
                PlayerPrefs.SetInt("current_level", currentLevel + 1);

            }
            SceneManager.LoadScene("Game");
        }
    }
}
