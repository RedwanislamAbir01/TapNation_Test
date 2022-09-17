using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class EnemyPathFaollower : MonoBehaviour
{
    #region Variables

    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _speed = 5f;

    [SerializeField] private float  _minSpeed , _maxSpeed ;

    private float _currentSpeed;
    private float _distanceTravelled;
    private bool _canFollow;

    #endregion

    #region Unity Methods

    private void Start()
    {
        SetRandomSpeed();
        _canFollow = true;

        GetComponent<Enemy>().OnDeath += DisableFollow ;
        GameManager.Instance.OnLevelEnd += DisableFollow;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnLevelEnd -= DisableFollow;
    }
    private void Update()
    {
        if (_canFollow) FollowPath();
    }



    #endregion

    #region Custom Methods



    private void FollowPath()
    {
        transform.Translate(_moveDirection * Time.deltaTime * _speed);
    }
    void SetRandomSpeed()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
    public void DisableFollow() => _canFollow = false;

 
    #endregion
}

