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
    public Vector3 mainCamStartRotation;
    public Vector3 mainCamStartPos;

    [SerializeField] private float smoothSpeed = 0.125f;
    private void Start()
    {
        mainCamStartRotation = Camera.main.transform.localRotation.eulerAngles;
        mainCamStartPos = Camera.main.transform.localPosition;
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopRotate();
        }
        if (!isFollow)
            return;
        Vector3 desiredPos = player.transform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, Helper.Help(desiredPos.x, transform.position.y, desiredPos.z), smoothSpeed * Time.deltaTime);

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
        Camera.main.transform.localRotation = Quaternion.Euler(mainCamStartRotation);
        Camera.main.transform.localPosition = Helper.Help(mainCamStartPos.x, mainCamStartPos.y, mainCamStartPos.z);
    }
    public void StopFollow()
    {
        isFollow = false;
    }
}