using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ground : MonoBehaviour
{
    [SerializeField] private float moveTime = 2f;
    [SerializeField] private PoolInfoWithPool particlePool;

    private GameObject particle;
    public void StartMove()
    {
        transform.DOMoveX(-(transform.localScale.x + 1.5f), moveTime).SetLoops(-1, LoopType.Yoyo);
    }
    public void StopMove()
    {
        DOTween.Kill(transform);
    }
    public void PlayParticle()
    {
        particle = particlePool.Fetch();
        particle.transform.position = transform.position;
        particle.SetActive(true);
    }
}
