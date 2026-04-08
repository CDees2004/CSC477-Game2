using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
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
    // singleton because management script
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    public int totalPuzzles = 1; // change to 4 after all added 
    public PlayerInput playerInput;

    private static HashSet<string> completedPuzzles = null;

    private void Awake()
    {
        Instance = this;
        if (completedPuzzles == null)
        {
            completedPuzzles = new();
        }

        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    private void Start()
    {
        GameState = GameState.MainMenu;
        DontDestroyOnLoad(transform.root.gameObject); // making the GameManager persist across scenes
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
                PlayingLogic();
                break;

            case GameState.Paused:
                PausedLogic();
                break;

            case GameState.GameOver:
                // go back to hub room, do whatever, go to end screen 
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("GameOver");
                break;

            case GameState.Win:
                // go back to hub room, do whatever, go to end screen
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("Escaped");
                break;
        }
    }

    // helper functions for state logic  
    public void PlayingLogic()
    {
        Time.timeScale = 1.0f;
        if (playerInput != null)
            playerInput.enabled = true;
    }

    public void PausedLogic()
    {
        Time.timeScale = 0.0f;
        if (playerInput != null)
            playerInput.enabled = false;
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        if(newState != GameState.Playing)
        {
            // letting the user have their mouse back
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }

    public void MarkPuzzleComplete(string puzzleName)
    {
        completedPuzzles.Add(puzzleName);
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (completedPuzzles.Count >= totalPuzzles)
        {
            ChangeState(FsmGameState.Win);
        }
    }
}
