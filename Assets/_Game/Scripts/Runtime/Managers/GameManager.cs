using System;
using _Tools.Helpers;
using GameAnalyticsSDK;
namespace _Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public event Action OnLevelStart;
        public event Action OnLevelEnd;
        public event Action OnLevelFail;
        public event Action OnNextLevelLoad;
        public event Action OnLevelCompleted;
        #endregion

        #region Custom Methods

        public void LevelStart()
        {
            OnLevelStart?.Invoke();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "World_01", "Stage_01", "Level_Progress");
        }
        public void LevelEnd()
        {
            OnLevelEnd?.Invoke();
        }
        public void LevelFail()
        {
            OnLevelFail?.Invoke();
        }

        public void NextLevelLoad()
        {
            OnNextLevelLoad?.Invoke();
        }

        public void LevelCompleted()
        {
            OnLevelCompleted?.Invoke();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "World_01", "Stage_01", "Level_End");
        }

        #endregion
    }
}