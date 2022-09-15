
using _Game.Managers;
using UnityEngine;

namespace _Game.UI
{
    public class LevelStartPanel : MonoBehaviour
    {
        #region Custom Methods

        public void LevelStart()
        {
            GameManager.Instance.LevelStart();
            DisablePanel();
        }

        private void DisablePanel() => gameObject.SetActive(false);

        #endregion
    }
}