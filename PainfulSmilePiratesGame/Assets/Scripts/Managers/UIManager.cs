using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text timer = null;
    [SerializeField]
    private Text scoreText = null;

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
}
