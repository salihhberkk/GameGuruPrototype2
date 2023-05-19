using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundManager : MonoSingleton<GroundManager>
{
    [SerializeField] private int startGroundCount = 2;
    [SerializeField] private int totalGroundCount = 10;
    [SerializeField] [Range(0f, 1f)] private float tolerance = 0.2f;
    [SerializeField] private PoolInfoWithPool groundPool;
    [SerializeField] private PoolInfoWithPool finishGroundPool;

    private GameObject ground;
    private GameObject finishGround;
    private List<Ground> grounds = new();
    private bool gameStart = false;
    private PieceController PieceController;
    public int spawnGroundCounter = 0;
    private int levelCount = 1;
    private void Start()
    {
        PieceController = PieceController.Instance;

        StartNewGrounds();
    }
    public void StartNewGrounds()
    {
        for (int i = 0; i < startGroundCount; i++)
        {
            ground = groundPool.Fetch();
            ground.transform.position = Helper.Help(0f, -0.25f, grounds.Count * ground.transform.localScale.z);
            ground.SetActive(true);

            grounds.Add(ground.GetComponent<Ground>());
        }
        PieceController.SetReferenceObject(grounds[grounds.Count - 1].gameObject);

        CreateCube();

        finishGround = finishGroundPool.Fetch();
        finishGround.transform.position = Helper.Help(0f, -0.25f, levelCount * (totalGroundCount) * ground.transform.localScale.z);
        finishGround.SetActive(true);
    }
    public void OpenInputAgain()
    {
        levelCount++;
        StartNewGrounds();
        OpenInput();
        ResetCounter();
    }
    public void ResetCounter()
    {
        spawnGroundCounter = 0;
    }
    public void OpenInput()
    {
        gameStart = true;
    }
    public void CloseInput()
    {
        gameStart = false;
    }
    private void Update()
    {
        if (!gameStart)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            grounds[grounds.Count - 1].StopMove();
            grounds[grounds.Count - 1].gameObject.SetActive(false);

            var distance = grounds[grounds.Count - 2].transform.position.x - grounds[grounds.Count - 1].transform.position.x;

            if (Mathf.Abs(distance) >= grounds[grounds.Count - 1].transform.localScale.x)
            {
                Debug.Log("game over");
                return;
            }
            if (Mathf.Abs(distance) < tolerance)// tolerans
            {
                PieceController.DivideObject(0f);
            }
            else
                PieceController.DivideObject(distance * -1);

            grounds.RemoveAt(grounds.Count - 2);
            if (spawnGroundCounter >= totalGroundCount) // toplam ground sayýsýna ulaþýldý
            {
                gameStart = false;
                return;
            }
            CreateCube();

        }
    }
    public void AddDivideObject(GameObject newObject)
    {
        grounds.Add(newObject.GetComponent<Ground>());
        spawnGroundCounter++;
    }
    public void CreateCube()
    {
        ground = groundPool.Fetch();

        ground.transform.localScale = Helper.Help(grounds[grounds.Count - 1].gameObject.transform.localScale.x
            , grounds[grounds.Count - 1].gameObject.transform.localScale.y
                , grounds[grounds.Count - 1].gameObject.transform.localScale.z);

        ground.transform.position = Helper.Help(ground.transform.localScale.x + 0.5f, -0.25f, grounds.Count * ground.transform.localScale.z);
        ground.SetActive(true);
        ground.GetComponent<Ground>().StartMove();

        grounds.Add(ground.GetComponent<Ground>());
        spawnGroundCounter++;
    }
}
