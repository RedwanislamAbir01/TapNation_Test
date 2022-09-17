using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using _Game.Managers;
using UnityEngine.Pool;
public class PlayerCreator : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI playerCountText;
    public List<GameObject> players = new List<GameObject>();
    [SerializeField] public bool holdoff = false;

    private ObjectPool<GameObject> _pool;
    private void Start()
    {
        _pool = new ObjectPool<GameObject>(() =>
        {
            return (Instantiate(player, PlayerPosition(), Quaternion.identity, transform));
        }, g =>
       {
           g.gameObject.SetActive(true);
       }, g =>
       {
           g.gameObject.SetActive(false);
       }, g =>
       {
           Destroy(g.gameObject);
       }, false, 100, 150);

        _Game.Managers.GameManager.Instance.OnLevelEnd += MoveToEnd;
        CountInitialPlayers();
    }
    private void OnDisable()
    {
        _Game.Managers.GameManager.Instance.OnLevelEnd -= MoveToEnd;
    }
    private void CountInitialPlayers()
    {
        players.Add(GetComponentInChildren<Player>().gameObject);
    }

    private void Update()
    {
        playerCountText.text = players.Count.ToString();
    }

    public void SpawnPlayer(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newPlayer = _pool.Get();
            newPlayer.GetComponent<Player>().PlayAimAnim();
            newPlayer.GetComponentInChildren<Gun>().StartShooting();
            players.Add(newPlayer);
        }
    }

    public Vector3 PlayerPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;
        return newPos;
    }

    void MoveToEnd()
    {
        for (int i = 0; i < players.Count; i++)
        {
              players[i].transform.parent = FindObjectOfType<FinishLine>()._container.transform;
              players[i].transform.DOLocalMove(FindObjectOfType<FinishLine>()._container.transform.localPosition, .5f).SetDelay(.05f * i).SetEase(Ease.InSine).OnComplete(() => {
                  for (int i = 0; i < players.Count; i++)
                  players[i].transform.DOLocalMove(FindObjectOfType<FinishLine>().EndPoses[i].transform.localPosition, .05f * i).SetDelay(.05f * i).OnComplete(() => {
                         
                  });
              


              });
                 
        } 
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Portal>())
        {
            Portal _portal = other.gameObject.GetComponent<Portal>();
            _portal.DisableGate();

            switch (_portal.currentMathState)
            {
                case Portal.SpawnerState.additive:
                    SpawnPlayer(_portal.size);
                    break;
                case Portal.SpawnerState.multiplier:
                    int multiplierSize = players.Count * _portal.size - players.Count;
                   SpawnPlayer(multiplierSize);
                    break;
            }

        }


    }

}
