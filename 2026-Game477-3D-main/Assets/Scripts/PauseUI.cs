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

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.root.gameObject);
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
        if (GameManager.Instance.GameState == FsmGameState.Paused)
        {
            GameManager.Instance.ChangeState(FsmGameState.Playing);
            pauseUIPanel.SetActive(false);
        }
        // if they are playing, then pause 
        else if (GameManager.Instance.GameState == FsmGameState.Playing)
        {
            GameManager.Instance.ChangeState(FsmGameState.Paused);
            pauseUIPanel.SetActive(true);
        }
        // don't allow anything if they are not either playing or paused
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        input.Disable();
        input.UI.Disable();
    }
}
