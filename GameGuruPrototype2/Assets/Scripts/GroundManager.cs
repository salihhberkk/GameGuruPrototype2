using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundManager : MonoSingleton<GroundManager>
{
    [SerializeField] private int startGroundCount = 2;
    [SerializeField] private PoolInfoWithPool groundPool;

    private GameObject ground;
    private List<Ground> grounds = new();
    private bool gameStart = false;
    private int groundCount = 0;
    private void Start()
    {
        for (int i = 0; i < startGroundCount + 1; i++)
        {
            CreateCube();
        }
    }

    public void OpenInput()
    {
        gameStart = true;
    }
    private void Update()
    {
        if (!gameStart)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            grounds[grounds.Count - 1].StopMove();
            CreateCube();
        }
    }
    public void CreateCube()
    {
        ground = groundPool.Fetch();
        if (groundCount >= startGroundCount)
        {
            ground.transform.position = Helper.Help(3.5f, -0.25f, groundCount * 4f);
            ground.GetComponent<Ground>().StartMove();
        }
        else
        {
            ground.transform.position = Helper.Help(0f, -0.25f, groundCount * 4f);
        }
        ground.SetActive(true);
        grounds.Add(ground.GetComponent<Ground>());
        groundCount++;
    }
}
