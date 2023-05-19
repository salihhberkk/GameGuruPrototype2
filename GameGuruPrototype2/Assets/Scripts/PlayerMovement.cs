using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [SerializeField] float speed;

    private bool move = false;
    private PlayerAnimator animator;

    private void Start()
    {
        animator = GetComponentInChildren<PlayerAnimator>();
    }
    public void StartMove()
    {
        IsMove = true;
        animator.SetRunAnim();
    }
    public void StopMove()
    {
        IsMove = false;
    }
    public bool IsMove
    {
        get => move;
        set
        {
            move = value;
        }
    }
    void Update()
    {
        if (move)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }
}
