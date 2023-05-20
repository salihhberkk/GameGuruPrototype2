using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Collider col;
    [SerializeField] private PoolInfoWithPool particlePool;

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        GameManager.Instance.WinGame();
        PlayParticle(other.transform);
    }
    private void PlayParticle(Transform other)
    {
        var particle = particlePool.Fetch();
        particle.transform.position = other.position;
        particle.SetActive(true);
    }
}
