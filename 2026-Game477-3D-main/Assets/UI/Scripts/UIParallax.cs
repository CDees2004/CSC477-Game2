using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // <-- Required for the New Input System!

public class UIParallax : MonoBehaviour
{
    private RawImage backgroundImage;

    [Header("Mouse Tracking")]
    public float mouseSensitivityX = 0.2f;
    public float mouseSensitivityY = 0.2f;
    public float smoothing = 5f;

    private Vector2 startPosition;
    private Vector2 currentPosition;

    void Start()
    {
        backgroundImage = GetComponent<RawImage>();
        
        if (backgroundImage == null)
        {
            Debug.LogError("Whoops! No RawImage found on " + gameObject.name);
            return;
        }

        startPosition = backgroundImage.uvRect.position; 
        currentPosition = startPosition;
    }

    void Update()
    {
        if (backgroundImage == null) return;

        // Safety check: Make sure a mouse is actually plugged in
        if (Mouse.current == null) return;

        // NEW INPUT SYSTEM: Reading the mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // 1. Find where the mouse is relative to the center of the screen
        float mouseX = (mousePos.x / Screen.width) * 2f - 1f;
        float mouseY = (mousePos.y / Screen.height) * 2f - 1f;

        mouseX = Mathf.Clamp(mouseX, -1f, 1f);
        mouseY = Mathf.Clamp(mouseY, -1f, 1f);

        // 2. Calculate target offset
        Vector2 targetPosition = startPosition + new Vector2(mouseX * mouseSensitivityX, mouseY * mouseSensitivityY);

        // 3. Smoothly slide to target
        currentPosition = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * smoothing);

        // 4. Apply back to the image
        Rect currentRect = backgroundImage.uvRect;
        currentRect.position = currentPosition;
        backgroundImage.uvRect = currentRect;
    }
}