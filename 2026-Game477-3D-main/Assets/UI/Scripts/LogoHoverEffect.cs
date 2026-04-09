using UnityEngine;
using UnityEngine.UI;

public class LogoHoverEffect : MonoBehaviour
{
    [Header("Float")]
    public float floatAmount = 8f;
    public float floatSpeed = 1.2f;

    [Header("Glow")]
    public float glowMin = 0.85f;
    public float glowMax = 1.0f;
    public float glowSpeed = 1.5f;

    private RectTransform rect;
    private RawImage image;
    private Vector2 startPos;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<RawImage>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        // vertical float
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        rect.anchoredPosition = new Vector2(startPos.x, newY);

        // vbrightness pulse
        float glow = Mathf.Lerp(glowMin, glowMax, (Mathf.Sin(Time.time * glowSpeed) + 1) / 2);
        image.color = new Color(glow, glow, glow, 1f);
    }
}