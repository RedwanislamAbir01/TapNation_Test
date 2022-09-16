using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion
}

