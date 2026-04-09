using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject overPlayButton;
    public GameObject overQuitButton;


    private void Start()
    {
        // wiring the game over buttons 
        if (overPlayButton != null)
        {
            Button overPlay = overPlayButton.GetComponent<Button>();
            overPlay.onClick.AddListener(StartGame);
        }

        if (overQuitButton != null)
        {
            Button overQuit = overQuitButton.GetComponent<Button>();
            overQuit.onClick.AddListener(QuitGame);
        }

    }


    // methods to add functionality to menu buttons 
    private void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void QuitGame()
    {
        // hacky work around to make quitting function 
        // while playing in the editor 
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
        Application.Quit(); 
#endif
    }

}

