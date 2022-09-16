using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float waveSpawnDelay = 20f;
    [SerializeField] private int waveAmount = 5;
    [SerializeField] private float distanceFromPlayer = 19f;

    public PlayerCreator playerCreator;

    private bool isSpawnerStopped = false;
    private WaitForSeconds enemySpawnDelay;
    void Start()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        enemySpawnDelay = new WaitForSeconds(waveSpawnDelay);

        GameManager.Instance.OnLevelStart += StartSpawningEnemyWaves;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnLevelStart -= StartSpawningEnemyWaves;
    }
    public void StartSpawningEnemyWaves()
    {
        StartCoroutine(EnemySpawnRoutine());
    }
    // Update is called once per frame
    IEnumerator EnemySpawnRoutine()
    {
        while (!isSpawnerStopped)
        {
            yield return enemySpawnDelay;

            for (int i = 0; i < waveAmount; i++)
            {
                //Debug.Log("SPAWN...");
                GameObject _enemyGO = Instantiate(_enemy, Vector3.zero, Quaternion.identity, this.transform);

                Enemy enemyController = _enemyGO.GetComponent<Enemy>();

                enemyController.transform.localPosition = Vector3.zero;
                enemyController.transform.position = new Vector3(playerCreator.gameObject.transform.position.x, .5f
                    , playerCreator.gameObject.transform.position.z - distanceFromPlayer);



                //enemyController.InitEnemyAttributes();
            }

            //yield return enemySpawnDelay;
        }
    }
}
