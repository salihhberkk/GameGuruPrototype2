using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    internal int hapticOn;

    private PlayerMovement PlayerMovement;
    private GroundManager GroundManager;
    private CameraFollower CameraFollower;
    private UIManager UIManager;

    
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        PlayerMovement = PlayerMovement.Instance;
        GroundManager = GroundManager.Instance;
        CameraFollower = CameraFollower.Instance;
        UIManager = UIManager.Instance;
    }
    internal void StartGame()
    {
        PlayerMovement.StartMove();
        GroundManager.OpenInput();
    }
    public void RestartGame()
    {
        PlayerMovement.StartMove();
        PlayerMovement.StartRunAnim();
        CameraFollower.StopRotate();
        UIManager.ShowPanel(PanelType.Game);
        GroundManager.OpenInputAgain();
    }
    public void WinGame()
    {
        PlayerMovement.StopMove();
        PlayerMovement.StartDanceAnim();
        CameraFollower.StartRotate();
        UIManager.ShowPanel(PanelType.Win);
    }
    public void LoseGame()
    {
        PlayerMovement.StopMove();
        CameraFollower.StopFollow();
        GroundManager.CloseInput();
        UIManager.ShowPanel(PanelType.Lose);
    }
}
