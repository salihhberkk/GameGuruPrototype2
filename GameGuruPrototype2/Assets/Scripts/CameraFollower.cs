using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : MonoSingleton<CameraFollower>
{
    [SerializeField] private GameObject player;
    [SerializeField] private float rotateTime;
    public Vector3 offset;
    private bool isFollow = true;
    private Vector3 mainCamStartRotation;

    [SerializeField] private float smoothSpeed = 0.125f;
    private void Start()
    {
        mainCamStartRotation = Camera.main.transform.rotation.eulerAngles;

    }
    void LateUpdate()
    {
        if (!isFollow)
            return;
        Vector3 desiredPos = player.transform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
    }
    public void StartRotate()
    {
        transform.DORotate(Helper.Help(0, 360f, 0), rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
    public void StopRotate()
    {
        DOTween.Kill(transform);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        Camera.main.transform.rotation = Quaternion.Euler(mainCamStartRotation);
    }
    public void StopFollow()
    {
        isFollow = false;
    }
}