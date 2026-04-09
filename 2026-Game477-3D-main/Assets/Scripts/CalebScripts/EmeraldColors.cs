using UnityEngine;

public class EmeraldColors : MonoBehaviour
{
    private Renderer rend;
    private Color colorCycle;

    void Start()
    {
        rend = GetComponent<Renderer>();
        print(rend);
    }
    
    private void Update()
    {
        colorCycle = Color.Lerp(Color.white, Color.blue, Mathf.PingPong(Time.time, 1));
        rend.material.color = colorCycle;
    }
}