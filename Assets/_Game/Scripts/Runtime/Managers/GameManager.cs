using System;
using _Tools.Helpers;

namespace _Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public event Action OnLevelStart;
        public event Action OnLevelEnd;
        public event Action OnLevelFail;
        public event Action OnNextLevelLoad;
        
        #endregion

        #region Custom Methods

        public void LevelStart()
        {
            OnLevelStart?.Invoke();
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
        #endregion
    }
}