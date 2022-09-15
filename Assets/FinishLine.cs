using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public List<Transform> EndPoses;
    public GameObject _container;
    void Start()
    {
        foreach (Transform child in _container.transform)
        {
            EndPoses.Add(child.transform);
        }

    }


}
