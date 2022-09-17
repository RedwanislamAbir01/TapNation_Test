using _Game.Helpers;
using _Game.Managers;

using _Tools.Helpers;
using DG.Tweening;
using UnityEngine;

namespace _Game.Controllers
{
    public enum ControllerTypes
    {
        Keyboard,
        Mobile
    }
    public class RunnerController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ControllerTypes _controllerTypes = ControllerTypes.Keyboard;
        [SerializeField] private Transform _playerChild;
        [SerializeField] private Transform _playerVisual;
        [Header("Common Control")]
        [SerializeField] [Min(0f)] private float _horizontalMovementRange = 2f;

        [SerializeField] [Range(0f, 360f)] private float _horizontalRotationRange = 10f;
        [SerializeField] [Min(0f)] private float _horizontalRotationDuration = 0.1f;

        [Header("Touch Control")]
        [SerializeField] [Min(0f)] private float _touchSpeed = 0.06f;

        [Header("Keyboard Control")]
        [SerializeField] [Min(0f)] private float _keyboardSpeed = 10f;

        private float _positionX;
        private bool _isTouching;
        private bool _canControl;

        #endregion

        #region Unity Methods

        private void Start()
        {
             GameManager.Instance.OnLevelStart += GameManager_OnLevelStart;
             GameManager.Instance.OnLevelEnd += DisableControl;
            GameManager.Instance.OnLevelFail += DisableControl;
        }
        private void OnDisable()
        {
            GameManager.Instance.OnLevelStart -= GameManager_OnLevelStart;
            GameManager.Instance.OnLevelEnd -= DisableControl;
            GameManager.Instance.OnLevelFail -= DisableControl;
        }

        private void Update()
        {
            if (_canControl) HandleControl();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance) GameManager.Instance.OnLevelStart -= GameManager_OnLevelStart;
        }
  

        #endregion

        #region Custom Methods

        private void GameManager_OnLevelStart() => EnableControl();
        
        private void HandleControl()
        {
            switch (_controllerTypes)
            {
                case ControllerTypes.Mobile:
                    HandleTouchInput();
                    break;

                case ControllerTypes.Keyboard:
                    HandleKeyboardInput();
                    break;

            }
        }

        private void HandleTouchInput()
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        {
                            if (!_isTouching)
                            {
                                _isTouching = true;
                            }

                            break;
                        }
                    case TouchPhase.Moved:
                        {
                            float deltaX = touch.deltaPosition.x;
                            _positionX += (deltaX / Screen.width) / Time.deltaTime * _touchSpeed;
                            _positionX = Mathf.Clamp(_positionX, -_horizontalMovementRange, _horizontalMovementRange);

                            Vector3 playerChildPosition = _playerChild.localPosition;
                            _playerChild.localPosition = new Vector3(_positionX , playerChildPosition.y, playerChildPosition.z);

                            if (deltaX > 2f)
                                _playerVisual.DOLocalRotate(new Vector3(0f, -_horizontalRotationRange, 0f), _horizontalRotationDuration);

                            if (deltaX < -2)
                                _playerVisual.DOLocalRotate(new Vector3(0f, _horizontalRotationRange, 0f), _horizontalRotationDuration);

                            if (deltaX == 0) _playerVisual.DOLocalRotate(Vector3.zero, _horizontalRotationDuration);

                            break;
                        }
                    case TouchPhase.Ended:
                        _isTouching = false;
                        _playerVisual.DOLocalRotate(Vector3.zero, _horizontalRotationDuration);
                        break;
                }
            }
        }
        private void HandleKeyboardInput()
        {
            float horizontalValue = Input.GetAxis("Horizontal") * Time.deltaTime * _keyboardSpeed;
            Vector3 newPosition = _playerChild.localPosition + Vector3.right * horizontalValue;

            newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalMovementRange, _horizontalMovementRange);
            _playerChild.localPosition = newPosition;

            if (Input.GetAxisRaw("Horizontal") > 0.1f)
                _playerVisual.DOLocalRotate(new Vector3(0f, _horizontalRotationRange, 0f), _horizontalRotationDuration);

            if (Input.GetAxisRaw("Horizontal") < -0.1f)
                _playerVisual.DOLocalRotate(new Vector3(0f, -_horizontalRotationRange, 0f), _horizontalRotationDuration);

            if (Input.GetAxisRaw("Horizontal") == 0) _playerVisual.DOLocalRotate(new Vector3(0f, 0f, 0f), _horizontalRotationDuration);
        }


        private void HandleRotation(float inputValue, float threshold)
        {
          
        }

  

        public void EnableControl() => _canControl = true;
#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button]
#endif
        public void DisableControl() => _canControl = false;
        
        #endregion
    }
}