using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Portal : MonoBehaviour
{
    public enum SpawnerState
    {
        additive,
        multiplier
    }
    public TextMeshProUGUI sizeText;
    public SpawnerState currentMathState;
    private PlayerCreator playerCreator;

    private MeshRenderer meshRenderer;
    public int size;
    public static bool isGateActive = true;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        sizeText = GetComponentInChildren<TextMeshProUGUI>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        switch (currentMathState)
        {
            case SpawnerState.additive:
                sizeText.text = "+" + size.ToString();
                break;
            case SpawnerState.multiplier:
                sizeText.text = "x" + size.ToString();
                break;
        }
    }


   public void DisableGate()
    {
        meshRenderer.enabled = false;
        sizeText.gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.parent.DOLocalMoveY(-1.5f, 1);
    }
    public IEnumerator GateActive()
    {
        isGateActive = false;
        yield return new WaitForSeconds(1.7f);
        isGateActive = true;
    }
}
