using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ground : MonoBehaviour
{
    [SerializeField] private float moveTime = 2f;
    public void StartMove()
    {
        transform.DOMoveX(-(transform.localScale.x + 1.5f), moveTime).SetLoops(-1, LoopType.Yoyo);
    }
    public void StopMove()
    {
        DOTween.Kill(transform);
    }
}
