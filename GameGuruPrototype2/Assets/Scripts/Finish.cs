using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Collider col;

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        GameManager.Instance.WinGame();
    }
}
