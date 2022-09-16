using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class Gun : MonoBehaviour
{
    [SerializeField]
     private Transform _shootPos;
 
     [SerializeField]
    private int _numberOfBullets;

    [SerializeField]
    private GameObject _bullet;

    [SerializeField] 
    private float _delay;

    float timer;
    [SerializeField]
    private float _minDelay, _maxDelay;

    bool canShoot = false;
     // Update is called once per frame
     private void Start()
    {
        _delay = Random.Range(_minDelay, _maxDelay);
        GameManager.Instance.OnLevelStart += StartShooting;
    }
    void Update()
    {
        if (canShoot)
        {
            timer += Time.deltaTime;
            if (timer > _delay)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        for (int i = 0; i <= _numberOfBullets; i++)
        {
            GameObject g = Instantiate(_bullet, _shootPos.transform.position, Quaternion.identity);

            switch(i)
            {
                case 0:
                    g.transform.rotation = Quaternion.Euler(0f, 0, 0f);
                    break;

                case 1:
                    g.transform.rotation = Quaternion.Euler(0f, -1, 0f);
                    break;

                case 2:
                    g.transform.rotation = Quaternion.Euler(0f, +1, 0f);
                    break;
            }
        }
    }
  public void StopShooting() => canShoot = false;
    public void StartShooting() => canShoot = true;
}
