using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _minX, _maxX;
    // Start is called before the first frame update
    void Start()
    {
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

}
