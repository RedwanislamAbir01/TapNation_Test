using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteUI : MonoBehaviour
{
   [SerializeField] private _Game.Managers.GameManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        DisablePanel();
        _manager.OnLevelCompleted += EnablePanel;
    }

    private void OnDisable()
    {
        _manager.OnLevelEnd -= EnablePanel;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    void EnablePanel()
    {
        gameObject.SetActive(true);
    }

   public void NextButtonActionListner()
    {
        _manager.NextLevelLoad();
    }
}
