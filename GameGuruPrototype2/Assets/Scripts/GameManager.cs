using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    internal int hapticOn;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    internal void StartGame()
    {
        PlayerMovement.Instance.StartMove();
    }
}
