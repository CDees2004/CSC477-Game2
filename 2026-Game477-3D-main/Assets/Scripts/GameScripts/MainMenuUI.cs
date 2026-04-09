using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject startPlayButton;
    public GameObject startQuitButton;


    private void Start()
    {
        // wiring the main menu buttons 
        if (startPlayButton != null)
        {
            Button startPlay = startPlayButton.GetComponent<Button>();
            startPlay.onClick.AddListener(StartGame);
        }

        if (startQuitButton != null)
        {
            Button startQuit = startQuitButton.GetComponent<Button>();
            startQuit.onClick.AddListener(QuitGame);
        }
    }


    // methods to add functionality to menu buttons 
    private void StartGame()
    {
        SceneManager.LoadScene("MainRoom");
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
