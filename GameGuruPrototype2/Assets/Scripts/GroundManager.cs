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
    private PieceController PieceController;
    private AudioPlayer audioPlayer;
    private bool gameStart = false;
    public int spawnGroundCounter = 0;
    private int levelCount = 1;
    private void Start()
    {
        PieceController = PieceController.Instance;
        audioPlayer = GetComponent<AudioPlayer>();
        StartNewGrounds();
    }
    public void StartNewGrounds()
    {
        for (int i = 0; i < startGroundCount; i++)
        {
            ground = groundPool.Fetch();
            ground.transform.position = Helper.Help(0f, -0.25f, (grounds.Count * ground.transform.localScale.z) + ((levelCount - 1) * ground.transform.localScale.z));
            ground.SetActive(true);

            grounds.Add(ground.GetComponent<Ground>());
            AnimateLightingInRainbow();
            spawnGroundCounter++;
        }
        PieceController.SetReferenceObject(grounds[grounds.Count - 1].gameObject);

        CreateCube();

        finishGround = finishGroundPool.Fetch();
        finishGround.transform.position = Helper.Help(0f, -0.25f
            , (levelCount * (totalGroundCount) * ground.transform.localScale.z) + (levelCount - 1 == 0 ? 0 : (ground.transform.localScale.z * (levelCount - 1))));
        finishGround.SetActive(true);
    }
    public void OpenInputAgain()
    {
        levelCount++;
        ResetCounter();
        StartNewGrounds();
        OpenInput();
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
                audioPlayer.PlayAudio();
            }
            else
            {
                PieceController.DivideObject(distance * -1);
                audioPlayer.ResetAudioFrequency();
            }

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
    }
    public void CreateCube()
    {
        ground = groundPool.Fetch();

        ground.transform.localScale = Helper.Help(grounds[grounds.Count - 1].gameObject.transform.localScale.x
            , grounds[grounds.Count - 1].gameObject.transform.localScale.y
                , grounds[grounds.Count - 1].gameObject.transform.localScale.z);

        ground.transform.position = Helper.Help(ground.transform.localScale.x + 0.5f, -0.25f, (grounds.Count * ground.transform.localScale.z) + ((levelCount - 1) * ground.transform.localScale.z));
        ground.SetActive(true);
        ground.GetComponent<Ground>().StartMove();

        grounds.Add(ground.GetComponent<Ground>());
        spawnGroundCounter++;
        AnimateLightingInRainbow();
    }

    byte red = 0;
    byte green = 0;
    byte blue = 0;
    byte alpha = 255;

    private void AnimateLightingInRainbow()
    {
        if (red == 0 && green == 0 && blue == 0)
            red = 255;
        else if (red == 0 && green < 255 && blue == 255)
            green += 51;
        else if (red == 0 && green == 255 && blue > 0)
            blue -= 51;
        else if (red == 255 && green == 0 && blue < 255)
            blue += 51;
        else if (red == 255 && green > 0 && blue == 0)
            green -= 51;
        else if (red > 0 && green == 0 && blue == 255)
            red -= 51;
        else if (red < 255 && green == 255 && blue == 0)
            red += 51;

        grounds[grounds.Count - 1].GetComponent<Renderer>().material.color = new Color32(red, green, blue, alpha);

    }
    public Color GetLastColor()
    {
        return grounds[grounds.Count - 1].GetComponent<Renderer>().material.color;
    }
}
