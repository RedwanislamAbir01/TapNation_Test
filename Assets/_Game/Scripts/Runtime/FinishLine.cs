using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class FinishLine : MonoBehaviour
{
    public List<Transform> EndPoses;
    public GameObject _container;
    void Start()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerBase"))
        {
         GameManager.Instance.LevelEnd();
        }
    }

}
