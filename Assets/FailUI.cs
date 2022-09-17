using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FailUI : MonoBehaviour
{
    [SerializeField] private _Game.Managers.GameManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        DisablePanel();
        _manager.OnLevelFail += EnablePanel;
    }

    private void OnDisable()
    {
        _manager.OnLevelFail -= EnablePanel;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
