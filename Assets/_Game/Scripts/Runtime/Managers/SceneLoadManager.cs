using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
namespace _Game.Managers
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        #region Variables
        public int SceneLoadCount;
        [SerializeField, Min(0)] private int _totalSceneCount;
        [SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndexes.GAME;
        [SerializeField] private bool _isTestEnabled;

        #endregion

        #region Properites

        public int TotalSceneCount => _totalSceneCount;
        public int FirstLevelSceneIndex => _firstLevelSceneIndex;

        #endregion

        #region Unity Methods
        private void Awake()
        {
            GameAnalytics.Initialize();
        }
        protected override void OnAwake()
        {
            //base.OnAwake();

            //if (_isTestEnabled) return;

            //if (PlayerPrefs.GetInt(ConstUtils.FIRST_TIME_PLAYING, 0) == 0)
            //{
            //    LoadSpecificScene(1);
            //    PlayerPrefs.SetInt(ConstUtils.FIRST_TIME_PLAYING, 1);
            //}
            //else
            //{
            //    LoadLastPlayedScene();
            //}
        }

        private void Start()
        {
            SceneLoadCount = PlayerPrefs.GetInt("Count", 1);
            SceneManager.sceneLoaded += IncrementSceneLoad;

            LoadSpecificScene(1);
        }
        #endregion

        #region Custom Methods
        private void IncrementSceneLoad(Scene scene, LoadSceneMode mode)
        {

            if (SceneManager.GetActiveScene().buildIndex == SceneLoadCount)
                return;
            else
            {
                SceneLoadCount++;
                PlayerPrefs.SetInt("Count", SceneLoadCount);
            }

        }
        private static void LoadLastPlayedScene()
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndexes.GAME));
        }

        public void LoadSpecificScene(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);

        public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



        public void EnableTestMode() => _isTestEnabled = true;

        #endregion
    }
}