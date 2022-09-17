using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    private PlayerCreator playerCreator;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.SetActive(false);
            playerCreator.players.Remove(collision.gameObject);
            collision.transform.parent = null;
            playerCreator.UpdateText(); 
            playerCreator.CheckPlayerExist();
            StartCoroutine(HoldOff());
        }
    }

    IEnumerator HoldOff()
    {
        playerCreator.holdoff = true;
        yield return new WaitForSeconds(0.75f);
        playerCreator.holdoff = false;
    }
}
