using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

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
    void Start()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        _anim = GetComponentInChildren<Animator>();
        RandomXspawn();
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsPlayerRange();
    }
    void LookTowardsPlayerRange()
    {
        if ((playerCreator.transform.parent.localPosition.z - transform.localPosition.z) >= lockPlayerMinDistance)
        {
           
            directionToFace =  playerCreator.transform.parent.position - _enemyModel.transform.position ;
            _enemyModel.transform.rotation = Quaternion.LookRotation(directionToFace);
         
            lockPlayerChaseStep = lockPlayerChaseSpeed * Time.deltaTime; // calculate distance to move
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerCreator.transform.localPosition, lockPlayerChaseStep);
        }
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
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isKilled)
        {
            isKilled = true;
            playerCreator.players.Remove(collision.gameObject);
            collision.transform.parent = null;
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
