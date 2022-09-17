using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] _guns;
    int index;
    void Start()
    {
        index = PlayerPrefs.GetInt("GunNo", 0);
        Updater();
        GetComponentInParent<GunUpdater>().OnGunUpdate += Updater;
    }

   void Updater ()
    {
     for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].gameObject.SetActive(false);
        }
       _guns[index].gameObject.SetActive(true);
       _guns[index].gameObject.GetComponent<Gun>().StartShooting();
    }
}
