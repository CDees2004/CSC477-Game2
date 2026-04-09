using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    public void Grab()
    {
        SceneManager.LoadScene("Escaped");
    }
}
