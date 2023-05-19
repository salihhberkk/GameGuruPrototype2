using TMPro;
using UnityEngine;

public class WinPanel : Panel
{
    [SerializeField] private TextMeshProUGUI levelText;
    public WinPanel()
    {
        panelType = PanelType.Win;
    }

    private void OnEnable()
    {
        if (levelText != null)
        {
            levelText.text = $"LEVEL {LevelManager.Instance.LevelIndicatorIndex}";
        }
    }

    public void Continue()
    {
        GameManager.Instance.RestartGame();
        //UIManager.Instance.LoadNextLevel();
    }
}