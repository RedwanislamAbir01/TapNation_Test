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

    public float Speed => _speed;
    #endregion

    #region Unity Methods

    private void Start()
    {
        SetRandomSpeed();
        _canFollow = true;

        GetComponent<Enemy>().OnDeath += DisableFollow ;
     
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


    public void SetSpeed(float number)
    {
       _speed = number;
    }
    private void FollowPath()
    {
        transform.localPosition += transform.forward * Time.deltaTime * _speed;
       
    }
    void SetRandomSpeed()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
    public void DisableFollow() => _canFollow = false;

 
    #endregion
}

