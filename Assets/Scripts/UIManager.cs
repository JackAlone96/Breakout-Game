using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public int score;
    public List<GameObject> livesIcons = new List<GameObject>();
    public GameObject gameOverPanel;
    public GameObject startGamePanel;
    public GameObject pausePanel;
    private MoveBall moveBallScript;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        HideGameOverPanel();
        HidePausePanel();
        moveBallScript = GameObject.Find("Ball").GetComponent<MoveBall>();
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = $"x{moveBallScript.combo}";
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int value)
    {
        for (int i = livesIcons.Count - 1; i >= 0; i--)
        {
            livesIcons[i].SetActive(value >= i);
        }
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideStartPanel()
    {
        startGamePanel.SetActive(false);
    }

    public void ShowStartPanel()
    {
        startGamePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
}
