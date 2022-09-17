using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GunUpdater : MonoBehaviour
{

    #region Events

    public event Action OnGunUpdate;
    int count;
    #endregion
    private void Start()
    {
        count =  PlayerPrefs.GetInt("GunNo", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Gun"))
        {
            UpdateGun();
            count++;
            PlayerPrefs.SetInt("GunNo", count);
            Destroy(other.gameObject);
           
        }
    }
    void UpdateGun()
    {
        OnGunUpdate?.Invoke();
    }
}  
