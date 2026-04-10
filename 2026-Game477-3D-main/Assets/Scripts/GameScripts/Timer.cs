using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public static float gameTimer = 240f; //240
    // set in inspector
    [SerializeField] private TextMeshPro timerText; 

    private void Update()
    {
        // only counting down if we are playing
        if (GameManager.Instance.GameState != FsmGameState.Playing)
            return;

        gameTimer -= Time.deltaTime;
        UpdateTimerDisplay(); 


        if(gameTimer < 0)
        {
            GameManager.Instance.ChangeState(FsmGameState.GameOver); 
        }
    }

    private void UpdateTimerDisplay()
    {
        float time = Mathf.Max(gameTimer, 0);

        int minutes = Mathf.FloorToInt(time / 60.0f);
        int seconds = Mathf.FloorToInt(time % 60.0f);

        timerText.text = $"{minutes} : {seconds}"; 
    }
}
