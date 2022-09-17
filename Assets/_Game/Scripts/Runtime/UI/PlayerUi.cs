using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class PlayerUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnLevelEnd += DisableObjects;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnLevelEnd -= DisableObjects;
    }
    // Update is called once per frame

    void DisableObjects()
    {
        gameObject.SetActive(false);
    }
}
