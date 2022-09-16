using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Managers;
public class Player : MonoBehaviour
{
    [SerializeField] Animator _anim;
    private static readonly int Shoot = Animator.StringToHash("Shoot");



    private PlayerCreator playerCreator;
    private Transform Center;
    [SerializeField] private float speed;

    private void Awake()
    {
        playerCreator = GetComponentInParent<PlayerCreator>();
        Center = playerCreator.transform;
       
    }

    void Start()
    {
        GameManager.Instance.OnLevelEnd += HoldOffFalse;
        _anim = GetComponent<Animator>();
        PlayAimAnim();
    }
    void FixedUpdate()
    {
        if (!playerCreator.holdoff)
        {
            transform.position = Vector3.MoveTowards(transform.position, Center.position, Time.fixedDeltaTime * speed);
        }
    }
    // Update is called once per frame
    void PlayAimAnim() => _anim.SetTrigger(Shoot);

    void HoldOffFalse() {
        GetComponent<Collider>().isTrigger = true;
        playerCreator.holdoff = true;
    }



}
