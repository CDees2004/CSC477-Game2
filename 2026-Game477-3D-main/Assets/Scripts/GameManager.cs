using UnityEngine;
using GameState = FsmGameState;

public enum FsmGameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Win,
}

// overall game manager done as a singleton 
// handles game state and other managers 
public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    private void Start()
    {
        GameState = GameState.MainMenu;
    }

    private void Update()
    {
        switch (GameState)
        {
            case GameState.MainMenu:
                // main menu logic 
                // initial scene shouldn't need anything
                break;

            case GameState.Playing:
                break; 
        }
    }
}
