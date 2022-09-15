using _Game.Managers;

using PathCreation;
using UnityEngine;

namespace _Game.Controllers
{
    public class PathFollower : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private float _speed = 5f;

        [SerializeField] [Tooltip("How fast full speed is achieved")] [Range(1f, 10f)]
        private float _speedMultiplier = 5f;

        private float _currentSpeed;
        private float _distanceTravelled;
        private bool _canFollow;

        #endregion

        #region Unity Methods

        private void Start()
        {
           GameManager.Instance.OnLevelStart += GameManager_OnLevelStart;
            GameManager.Instance.OnLevelEnd += StopFollowing;
        }

        private void Update()
        {
            if (_canFollow) FollowPath();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance) GameManager.Instance.OnLevelStart -= GameManager_OnLevelStart;
            
        }

        #endregion

        #region Custom Methods
        
        private void GameManager_OnLevelStart() => StartFollowing();

        private void FollowPath()
        {
            transform.Translate(_moveDirection * Time.deltaTime * _speed);
        }

      


        public void StartFollowing() => _canFollow = true;
        

        public void StopFollowing() => _canFollow = false;
        
        #endregion
    }
}