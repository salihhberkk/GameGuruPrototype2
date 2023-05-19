using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [SerializeField] float speed;

    private bool move = false;
    private PlayerAnimator animator;
    private float targetX;
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
    public void StartDanceAnim()
    {
        animator.SetDanceAnim();
    }
    public void StartRunAnim()
    {
        animator.SetRunAnim();
    }
    public bool IsMove
    {
        get => move;
        set
        {
            move = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        targetX = collision.gameObject.transform.position.x;
    }
    void Update()
    {
        if (move)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
            transform.position = Vector3.Lerp(transform.position, Helper.Help(targetX, transform.position.y, transform.position.z), Time.deltaTime * 5f);
        }
    }
}
