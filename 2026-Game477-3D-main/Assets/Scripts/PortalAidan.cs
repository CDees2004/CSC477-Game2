using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainRoom");
        }
    }
}