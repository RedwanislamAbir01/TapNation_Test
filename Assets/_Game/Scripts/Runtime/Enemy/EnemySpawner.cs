using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
using DG.Tweening;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float waveSpawnDelay = 20f;
    [SerializeField] private int waveAmount = 5;
    [SerializeField] private float distanceFromPlayer = 19f;
    List<GameObject> enemies = new List<GameObject>();
    public PlayerCreator playerCreator;

    private bool isSpawnerStopped = false;
    private WaitForSeconds enemySpawnDelay;


    public List<GameObject> Enemies => enemies;
    public bool IsSpawningStopped => isSpawnerStopped;
    void Start()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        enemySpawnDelay = new WaitForSeconds(waveSpawnDelay);
        GameManager.Instance.OnLevelEnd += StopSpwaner;
        GameManager.Instance.OnLevelStart += StartSpawningEnemyWaves;
        GameManager.Instance.OnLevelStart += InitialSpawn;
        GameManager.Instance.OnLevelFail += StopSpwaner;

    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelEnd -= StopSpwaner;
        GameManager.Instance.OnLevelStart -= StartSpawningEnemyWaves;
        GameManager.Instance.OnLevelStart -= InitialSpawn;
    }
    public void StartSpawningEnemyWaves()
    {
        StartCoroutine(EnemySpawnRoutine());
    }
   

    IEnumerator EnemySpawnRoutine()
    {
        while (!isSpawnerStopped)
        {
            yield return enemySpawnDelay;

            Spawn(waveAmount);

        }
    }
    void InitialSpawn()
    {
        Spawn(10);
    }
    private void Spawn(int no)
    {
        for (int i = 0; i < no; i++)
        {
            //Debug.Log("SPAWN...");
            GameObject _enemyGO = Instantiate(_enemy, Vector3.zero, Quaternion.identity, this.transform);
            enemies.Add(_enemyGO);
            Enemy enemyController = _enemyGO.GetComponent<Enemy>();
            _enemyGO.transform.DOLocalRotate(new Vector3(0, -180, 0), 0);
            enemyController.transform.localPosition = Vector3.zero;
            enemyController.transform.position = new Vector3(playerCreator.gameObject.transform.position.x, .5f
            , playerCreator.gameObject.transform.position.z - distanceFromPlayer);

        }
    }

    void StopSpwaner() { isSpawnerStopped = true;
    
        StopAllCoroutines(); }


}
