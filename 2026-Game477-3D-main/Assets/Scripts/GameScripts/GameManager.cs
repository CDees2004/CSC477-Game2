using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using GameState = FsmGameState;
using StarterAssets;

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
    // WHY DOES UNITY IGNORE HARD CODED VALUES IN FAVOR OF INSPECTOR VALUES 
    // I JUST SPENT AN HOUR WONDERING WHY THIS WAS STUCK AT VALUE OF 1
    private int totalPuzzles = 4; // change to 4 after all added 
    public PlayerInput playerInput;

    public bool overrideCursorLock = false;

    [Header("Game Over")]
    public GameObject decorationObject;
    public GameObject interactablesObject;
    public GameObject door;
    public GameObject portal;

    private static HashSet<string> completedPuzzles = null;

    private void TriggerGameOverEffects()
    {
        SceneManager.LoadScene("MainRoom");
        if (decorationObject != null) Destroy(decorationObject);
        if (interactablesObject != null) Destroy(interactablesObject);

        RenderSettings.skybox = null;
        RenderSettings.ambientLight = new Color(0.2f, 0f, 0f);
        RenderSettings.fogColor = Color.black;
        RenderSettings.fog = true;
    }

    private IEnumerator LoadGameOverDelayed()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameOver");
    }

    private void Awake()
    {
        // check if an instance already exists 
        if (Instance != null && Instance != this)
        {
            // grab duplicate pause picked up through scenes 
            PauseUI duplicatePause = GetComponentInChildren<PauseUI>();
            if (duplicatePause != null)
            {
                duplicatePause.CleanupForDestroy();
            }

            // get rid of the all scenes items if there is already one
            Destroy(transform.root.gameObject);
            return;
        }

        // if its the only instance set it up 
        Instance = this;
        // and make it persist 
        DontDestroyOnLoad(transform.root.gameObject);


        if (completedPuzzles == null)
        {
            completedPuzzles = new();
        }

        // trying to make puzzles reset upon replay 
        if (completedPuzzles == null) completedPuzzles = new();
        else completedPuzzles.Clear();

        // possibly dangerous line
        GameState = GameState.Playing;
    }

    private void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        portal.SetActive(false);

        // trying to maintain cursor focus upon replays 
        if (GameState == GameState.Playing)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (GameState == GameState.Playing)
        {
            PlayingLogic(); // might not need to be in update
        }
    }

    // helper functions for state logic  
    private void PlayingLogic()
    {
        Time.timeScale = 1.0f;

        if (!overrideCursorLock)
        {
            if (playerInput != null)
                playerInput.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void PausedLogic()
    {
        Time.timeScale = 0.0f;
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // not currently working
    private void ResetGameProgress()
    {
        if (completedPuzzles != null)
        {
            completedPuzzles.Clear();
        }
    }

    public void ChangeState(GameState newState)
    {
        // prevent redundant changes
        if (GameState == newState) return;

        GameState = newState;

        switch (GameState)
        {
            // main menu needs to wipe progress
            case GameState.MainMenu:
                ResetGameProgress();
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("MainMenu");
                break;

            case GameState.Playing:
                Time.timeScale = 1.0f;
                if (playerInput != null)
                    playerInput.enabled = true;
                var fpsPlay = FindAnyObjectByType<StarterAssets.FirstPersonController>();
                if (fpsPlay != null) fpsPlay.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case GameState.Paused:
                Time.timeScale = 0.0f;
                if (playerInput != null)
                    playerInput.enabled = false;
                var fps = FindAnyObjectByType<StarterAssets.FirstPersonController>();
                if (fps != null) fps.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case GameState.GameOver:
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                TriggerGameOverEffects();
                StartCoroutine(LoadGameOverDelayed());
                break;

            case GameState.Win:
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked; // this used to be none Why????
                Cursor.visible = true;
                Destroy(door);
                portal.SetActive(true);
                //SceneManager.LoadScene("Escaped");
                break;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        overrideCursorLock = false;
        playerInput = FindAnyObjectByType<PlayerInput>();

        // re-find portal and door since scene reloaded
        if (portal != null) portal = null;
        if (door != null) door = null;

        portal = GameObject.Find("Portal"); // must match exact name
        door = GameObject.Find("FullDoor"); // must match exact name

        if (portal != null)
            portal.SetActive(GameState == FsmGameState.Win);
        if (door != null)
            door.SetActive(GameState != FsmGameState.Win);
    }

    public bool IsPuzzleComplete(string puzzleName)
    {
        return completedPuzzles.Contains(puzzleName);
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
            print($"Amount of completed puzzles is {completedPuzzles.Count}");
            print($"Amount of puzzles is {totalPuzzles}");
            SceneController.LoadMainRoom();
            ChangeState(FsmGameState.Win);
        }

        // if they complete a puzzle but not all of them, 
        // return them to the hub room 
        else
        {
            SceneController.LoadMainRoom();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && overrideCursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
