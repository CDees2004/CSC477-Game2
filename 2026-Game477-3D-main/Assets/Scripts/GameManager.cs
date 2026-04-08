using NUnit.Framework;
using UnityEngine;
using GameState = FsmGameState;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 

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
    // singleton because management script
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    // set in inspector 
    public GameObject pausedPanelUI;
    public int totalPuzzles = 1; // change to 4 after all added 

    private static HashSet<string> completedPuzzles = null;

    private void Awake()
    {
        Instance = this;
        if(completedPuzzles == null)
        {
            completedPuzzles = new(); 
        }
    }

    private void Start()
    {
        GameState = GameState.MainMenu;
        DontDestroyOnLoad(gameObject); // making the GameManager persist across scenes
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
                Time.timeScale = 1.0f;
                pausedPanelUI.SetActive(false); 
                break;

            case GameState.Paused:
                Time.timeScale = 0.0f;
                pausedPanelUI.SetActive(true); 
                break;

            case GameState.GameOver:
                // do whatever and go to the gameover screen
                SceneManager.LoadScene("GameOver"); 
                break;

            case GameState.Win:
                // do whatever and go to the win screen
                SceneManager.LoadScene("Escaped");
                break;
        }
    }


    public void ChangeState(GameState newState)
    {
        GameState = newState;
    }


    public void MarkPuzzleComplete(string puzzleName)
    {
        completedPuzzles.Add(puzzleName);
        CheckWinCondition(); 
    }


    public void CheckWinCondition()
    {
        if(completedPuzzles.Count >= totalPuzzles)
        {
            ChangeState(FsmGameState.Win); 
        }
    }
}
