using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text timer = null;
    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private GameObject blackPanel = null;
    [SerializeField]
    private Text finalScoreText = null;

    public void updateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timer.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void EndScreen()
    {
        blackPanel.SetActive(true);
    }

    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Your Final Score Was: \n" + score;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
