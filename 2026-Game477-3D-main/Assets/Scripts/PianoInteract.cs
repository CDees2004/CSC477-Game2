using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Cinemachine;

public class PianoInteract : MonoBehaviour
{
    [Header("References")]
    public GameObject pianoUI;
    public Transform sitTarget;
    public Transform playerBody;
    public GameObject hud;

    [Header("Settings")]
    public float lerpDuration = 1.2f;

    private FirstPersonController fpsController;
    private PlayerInput playerInput;
    private Camera mainCam;
    private CinemachineBrain cinemachineBrain;

    private Vector3 originalCamPos;
    private Quaternion originalCamRot;
    private bool isSitting = false;

    private bool puzzleComplete = false;

    private void Awake()
    {
        fpsController = playerBody.GetComponent<FirstPersonController>();
        playerInput = playerBody.GetComponent<PlayerInput>();
        mainCam = Camera.main;
        cinemachineBrain = mainCam.GetComponent<CinemachineBrain>();
    }

    private void Grab()
    {
        if (isSitting || puzzleComplete) return;
        StartCoroutine(SitDown());
    }

    public void StandUp()
    {
        if (!isSitting) return;
        StartCoroutine(StandBack());
    }

    private IEnumerator SitDown()
    {
        isSitting = true;
        cinemachineBrain.enabled = false;
        hud.SetActive(false);

        // disable movement
        fpsController.enabled = false;
        playerInput.enabled = false;

        // store original camera transform
        originalCamPos = mainCam.transform.position;
        originalCamRot = mainCam.transform.rotation;

        // lerp camera to sit position
        float elapsed = 0f;
        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / lerpDuration);
            mainCam.transform.position = Vector3.Lerp(originalCamPos, sitTarget.position, t);
            mainCam.transform.rotation = Quaternion.Lerp(originalCamRot, sitTarget.rotation, t);
            yield return null;
        }

        mainCam.transform.position = sitTarget.position;
        mainCam.transform.rotation = sitTarget.rotation;

        // show UI
        pianoUI.SetActive(true);
        GameManager.Instance.overrideCursorLock = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator StandBack()
    {
        // hide UI
        pianoUI.SetActive(false);
        GameManager.Instance.overrideCursorLock = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // lerp camera back
        float elapsed = 0f;
        Vector3 currentPos = mainCam.transform.position;
        Quaternion currentRot = mainCam.transform.rotation;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / lerpDuration);
            mainCam.transform.position = Vector3.Lerp(currentPos, originalCamPos, t);
            mainCam.transform.rotation = Quaternion.Lerp(currentRot, originalCamRot, t);
            yield return null;
        }

        mainCam.transform.position = originalCamPos;
        mainCam.transform.rotation = originalCamRot;

        // re-enable movement
        fpsController.enabled = true;
        playerInput.enabled = true;

        isSitting = false;
        cinemachineBrain.enabled = true;
        hud.SetActive(true);
    }

    public void SetPuzzleComplete()
    {
        puzzleComplete = true;
    }
}