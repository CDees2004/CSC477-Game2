using UnityEngine;

public class Timer : MonoBehaviour
{
    public float gameTimer = 180f;

    private void Update()
    {
        // only counting down if we are playing
        if (GameManager.Instance.GameState != FsmGameState.Playing)
            return;

        gameTimer -= Time.deltaTime; 
        
        if(gameTimer < 0)
        {
            GameManager.Instance.ChangeState(FsmGameState.GameOver); 
        }
    }
}
