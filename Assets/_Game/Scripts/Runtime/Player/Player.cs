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
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        Center = GameObject.FindGameObjectWithTag("PlayerBase").transform;
    }

    void Start()
    {
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





}
