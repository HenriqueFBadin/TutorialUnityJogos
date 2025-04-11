using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEnd : MonoBehaviour
{
    public TMP_Text timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float totalTime = BattingManager.GetTotalTime();
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        timeText.text = $"Congratulations! You won in {minutes:00}:{seconds:00}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        BattingManager.ResetTime();
        SceneManager.LoadSceneAsync(0);
    }
}
