using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public enum PanelType
{
    Start,
    Game,
    Win,
    Lose,
    Draw,
    AdNotReady,
    NewObjectPanel
}
public class UIManager : MonoSingleton<UIManager>
{
    private List<Panel> allPanels;

    //[SerializeField] private TextMeshProUGUI barStartLevelText;
    //[SerializeField] private TextMeshProUGUI barNextLevelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private AttributeSC money;

    private void Awake()
    {
        allPanels = GetComponentsInChildren<Panel>(true).ToList();
        money.Value = PlayerPrefs.GetFloat("TotalMoney", money.Value);
    }
    private void Start()
    {
        SetMoneyText();
    }
    public void HideAllPanels()
    {
        allPanels.ForEach(panel => panel.gameObject.SetActive(false));
    }
    public void ShowPanel(PanelType type)
    {
        HideAllPanels();
        allPanels.Find(panel => panel.PanelType == type).gameObject.SetActive(true);
    }
    public void HidePanel(PanelType type)
    {
        allPanels.Find(panel => panel.PanelType == type).gameObject.SetActive(false);
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        ShowPanel(PanelType.Game);
    }

    public void RetryLevel()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
    public void SetMoneyText()
    {
        moneyText.text = money.Value.ToString("0");
    }
    
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
            return;
        PlayerPrefs.SetFloat("TotalMoney", money.Value);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalMoney", money.Value);
    }
    public void AddMoney(float addMoney)
    {
        money.Value += addMoney;
        SetMoneyText();
    }
}
