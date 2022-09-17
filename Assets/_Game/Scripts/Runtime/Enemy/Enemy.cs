using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using _Game.Managers;
public class Enemy : MonoBehaviour
{
    #region Events
    public event Action OnDeath;
    #endregion


    #region Variables

    public enum eEnemyType
    {
        Normal,
        Big
    }
    [SerializeField] private PlayerCreator playerCreator;
    [SerializeField] private eEnemyType _enemyType;
    [SerializeField] private float _minX, _maxX;
    [SerializeField] private Material _enemyDeathMat;
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private float lockPlayerChaseSpeed = 3;
    [SerializeField] private float lockPlayerMinDistance = 20;


    Animator _anim;
    private bool isKilled = false;
    private Vector3 directionToFace;

    private float lockPlayerChaseStep;

    #endregion

    #region Unity Methods
    void Start()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        _anim = GetComponentInChildren<Animator>();
        RandomXspawn();
    }

  
    void Update()
    {
        LookTowardsPlayerRange();
    }
    #endregion

    #region Custom methods
    void LookTowardsPlayerRange()
    {
        if ((transform.position.z - playerCreator.transform.parent.position.z) <= lockPlayerMinDistance)
        {

            directionToFace = playerCreator.transform.position - transform.position;
            transform.LookAt(new Vector3(playerCreator.transform.position.x , .5f , playerCreator.transform.position.z));
            GetComponent<EnemyPathFaollower>().SetSpeed(lockPlayerChaseSpeed);

        }
        else
            transform.localEulerAngles = new Vector3(0, -180, 0);
    }
    void RandomXspawn()
    {
        transform.localPosition = new Vector3(Random.Range(_minX, _maxX), transform.localPosition.y, transform.localPosition.z);
    }
    public void KillEnemy()
    {
        OnDeath?.Invoke();
        if (isKilled)
            return;

        isKilled = true;
        GetComponentInChildren<Collider>().enabled = true;
        _anim.SetTrigger("Death");
        GetComponentInChildren<SkinnedMeshRenderer>().material = _enemyDeathMat;
        Invoke("DisappearTween", 1f);

    }

    private void DisappearTween()
    {
        _enemyModel.transform.DOLocalMoveY(-1, 2.5f);
        _enemyModel.transform.DOScale(0, 2.5f).OnComplete(() => DisableEnemyCompletely());
    }

    private void DisableEnemyCompletely()
    {

        this.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            GetComponentInParent<EnemySpawner>().Enemies.Remove(gameObject);
            KillEnemy();
            CheckIfAllEnnimiesDead();
        }
        if (other.gameObject.CompareTag("Player") && !isKilled)
        {
            isKilled = true;
            playerCreator.players.Remove(other.gameObject);
            other.transform.parent = null;
            other.gameObject.SetActive(false);
            GetComponentInParent<EnemySpawner>().Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void CheckIfAllEnnimiesDead()
    {
        if (GetComponentInParent<EnemySpawner>().Enemies.Count <= 0  && GetComponentInParent<EnemySpawner>().IsSpawningStopped)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
    #endregion
}
