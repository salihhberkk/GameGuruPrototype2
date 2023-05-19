using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;
public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [SerializeField] float speed;
    [SerializeField] float swipeSensitivity;

    private bool move = false;

    public void StartHandlePlayer()
    {
        LeanTouch.OnFingerUpdate += HandlePlayer; 
    }
    public void StopHandlePlayer()
    {
        LeanTouch.OnFingerUpdate -= HandlePlayer;
    }
    public void StartMove()
    {
        IsMove = true;
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
    private void HandlePlayer(LeanFinger obj)
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.position += Vector3.right * (obj.ScaledDelta.x * swipeSensitivity);
            var clampXPos = Mathf.Clamp(gameObject.transform.position.x, -2f, 2f);
            gameObject.transform.position = new Vector3(clampXPos, gameObject.transform.position.y,
                  gameObject.transform.position.z);
        } 
    }  
}
