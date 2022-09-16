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
    Animator _anim;
    private bool isKilled = false;
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
