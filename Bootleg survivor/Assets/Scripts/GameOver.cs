using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text ScoreTMP;
    public GameObject ScorePanel;

    void Start()
    {
        Player.PlayerDied += ShowEndScreen;
    }

    private void ShowEndScreen()
    {
        ScorePanel.SetActive(true);
        ScoreTMP.text = $"SCORE: {State.instance.Score}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
