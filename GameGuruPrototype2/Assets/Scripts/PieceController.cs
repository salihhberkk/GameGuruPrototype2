using System;
using TMPro;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
}
public class PieceController : MonoSingleton<PieceController>
{
    [SerializeField] private Transform reference;
    [SerializeField] private MeshRenderer referenceMesh;
    [SerializeField] private PoolInfoWithPool fallingObjectPool;
    [SerializeField] private PoolInfoWithPool standObjectPool;

    private GroundManager GroundManager;
    private void Start()
    {
        GroundManager = GroundManager.Instance;
    }
    public void SetReferenceObject(GameObject newReferenceObject)
    {
        reference = newReferenceObject.transform;
        referenceMesh = newReferenceObject.GetComponent<MeshRenderer>();
    }
    public void DivideObject(float value)
    {
        bool isFirtFalling = value > 0;

        var falling = fallingObjectPool.Fetch();
        var stand = standObjectPool.Fetch();

        //size
        var fallingSize = reference.localScale;
        fallingSize.x = Math.Abs(value);
        falling.transform.localScale = fallingSize;

        var standSize = reference.localScale;
        standSize.x = reference.localScale.x - Math.Abs(value);
        stand.transform.localScale = standSize;

        //position
        var fallingPosition = GetPositionEdge(referenceMesh, isFirtFalling ? Direction.Right : Direction.Left);
        fallingPosition.x += (fallingSize.x / 2f) * (isFirtFalling ? 1 : -1);
        falling.transform.position = fallingPosition + Helper.Help(0, 0, falling.transform.localScale.z);

        var standPosition = GetPositionEdge(referenceMesh, !isFirtFalling ? Direction.Left : Direction.Right);
        standPosition.x += (standSize.x / 2f) * (!isFirtFalling ? 1 : -1);
        stand.transform.position = standPosition + Helper.Help(0, 0, falling.transform.localScale.z);

        stand.SetActive(true);
        stand.GetComponent<Renderer>().material.color = GroundManager.Instance.GetLastColor();

        if (value != 0)
        {
            falling.SetActive(true);
            falling.GetComponent<Rigidbody>().isKinematic = false;
            falling.GetComponent<Renderer>().material.color = GroundManager.Instance.GetLastColor();
        }
        else
        {
            falling.SetActive(false);
            stand.GetComponent<Ground>().PlayParticle();
        }

        reference = stand.transform;
        referenceMesh = stand.GetComponent<MeshRenderer>();
        GroundManager.AddDivideObject(stand);
    }
    private Vector3 GetPositionEdge(MeshRenderer mesh, Direction direction)
    {
        var extents = mesh.bounds.extents;
        var position = mesh.transform.position;

        switch (direction)
        {
            case Direction.Left:
                position.x += -extents.x;
                break;
            case Direction.Right:
                position.x += extents.x;
                break;
        }
        return position;
    }
}
