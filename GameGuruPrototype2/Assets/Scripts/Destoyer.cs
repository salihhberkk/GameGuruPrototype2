using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public Vector3 offset;

    [SerializeField] private float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 desiredPos = Helper.Help(target.transform.position.x, 0, target.transform.position.z) + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseGame();
        }
        if (other.CompareTag("Cube"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
