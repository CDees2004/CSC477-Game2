using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    // assign in inspector
    public GameObject pauseUIPanel;

    private InputSystem_Actions input; 

    // making sure it is off by default 
    private void Awake()
    {
        Instance = this;
        input = new();

        input.Enable();
        input.UI.Enable();

        pauseUIPanel.SetActive(false);
    }

    private void Update()
    {
        // if they pressed the button then toggle pause
        if (input.UI.Pause.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        // if they are paused, then return to playing
        if(GameManager.Instance.GameState == FsmGameState.Paused)
        {
            GameManager.Instance.ChangeState(FsmGameState.Playing);
        }
        // if they are playing, then pause 
        else
        {
            GameManager.Instance.ChangeState(FsmGameState.Paused);
        }
    }
}
